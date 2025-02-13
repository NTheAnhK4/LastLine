
using System;
using UnityEngine;

public class HealthHandler : ComponentBehavior
{
    public Transform Actor { get; set; }
    [SerializeField] private DeadHandler deadHandler;
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;

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
    public void Init(float hp)
    {
        maxHealth = hp;
        CurHealth = hp;
        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        CurHealth = Mathf.Max(curHealth - damage, 0);
        if (curHealth <= 0)
        {
            IsDead = true;
            deadHandler.OnDead(true);
        }
    }
}
