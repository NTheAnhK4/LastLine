
using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour
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
    public bool IsDead;
    public void Init(Transform actorTrf, float hp)
    {
        maxHealth = hp;
        CurHealth = hp;
        Actor = actorTrf;
        IsDead = false;
        if (Actor != null)
        {
            if(deadHandler == null) deadHandler = Actor.GetComponentInChildren<DeadHandler>();
            if (healthUI == null) healthUI = Actor.GetComponentInChildren<HealthUI>();
        }
        
    }

    public void TakeDamage(float damage)
    {
        CurHealth = Mathf.Max(curHealth - damage, 0);
        if (curHealth <= 0)
        {
            IsDead = true;
            deadHandler.OnDead();
        }
    }
}
