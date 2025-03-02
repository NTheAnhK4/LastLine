
using System;
using UnityEngine;


public class MeleeAttack : AttackHandler
{
    [SerializeField] private float damage;
   
    public void Init(float aRange, float aSpeed, float aDamage)
    {
        base.Init(aRange,aSpeed);
        this.damage = aDamage;
        
    }
    

    

    protected override void Attack(HealthHandler enemy)
    {
        base.Attack(enemy);
        
        animHandler.SetAnim(AnimHandler.State.Attack);
        enemy.TakeDamage(damage);
    }
}
