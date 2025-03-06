
using System;
using UnityEngine;


public class MeleeAttack : AttackHandler
{
    [SerializeField] private float damage;
    [SerializeField] private DamageType m_DamageType;
   
    public void Init(float aRange, float aSpeed, float aDamage, DamageType damageType)
    {
        base.Init(aRange,aSpeed);
        this.damage = aDamage;
        
    }
    

    

    protected override void Attack(HealthHandler enemy)
    {
        base.Attack(enemy);
        
        animHandler.SetAnim(AnimHandler.State.Attack);
        enemy.TakeDamage(damage, m_DamageType);
    }
}
