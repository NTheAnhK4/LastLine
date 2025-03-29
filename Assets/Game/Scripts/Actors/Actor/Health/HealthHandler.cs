
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthHandler : ComponentBehavior
{
    public Transform Actor { get; set; }
    [SerializeField] private DeadHandler deadHandler;
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;
    [SerializeField] private float m_PhysicalDamageReduction;
    [SerializeField] private float m_MagicalDamageReduction;
    public Action OnDead = null;

    public float PhysicalDamageReduction
    {
        get => m_PhysicalDamageReduction;
        set => m_PhysicalDamageReduction = value;
    }
    public float CurHealth
    {
        get => curHealth;
        set
        {
            if (Math.Abs(value - curHealth) > 0.01f)
            {
                curHealth = value;
                if(healthUI != null) healthUI.OnHpChange(curHealth/maxHealth);
            }
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (deadHandler == null) deadHandler = transform.parent.parent.GetComponentInChildren<DeadHandler>();
        if(healthUI == null) healthUI = transform.parent.parent.GetComponentInChildren<HealthUI>();
        Actor = transform.parent.parent;
    }

    public bool IsDead;
    public void Init(float hp, float physicalDamageReduction, float magicalDamageReduction)
    {
        OnDead = null;
        maxHealth = hp;
        CurHealth = hp;
        IsDead = false;
        m_PhysicalDamageReduction = physicalDamageReduction;
        m_MagicalDamageReduction = magicalDamageReduction;
    }

    public void TakeDamage(float damage, DamageType damageType)
    {

        if (IsDead) return;
        switch (damageType)
        {
            case DamageType.Physical:
                damage *= m_PhysicalDamageReduction;
                break;
            case DamageType.Magical:
                damage *= m_MagicalDamageReduction;
                break;
        }
        CurHealth = Mathf.Max(curHealth - damage, 0);
        if (curHealth <= 0)
        {
            IsDead = true;
            OnDead?.Invoke();
            deadHandler.OnDead(true);
        }
    }
}
