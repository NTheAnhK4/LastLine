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

    private void FaceToEnemy(Transform enemy)
    {
        Vector3 direction = enemy.position - actor.position;
        if(direction.x < 0) animHandler.RotateAnim(false);
        else animHandler.RotateAnim(true);
        
    }
    

    protected override void Attack(HealthHandler enemy)
    {
        base.Attack(enemy);
        FaceToEnemy(enemy.Actor);
        animHandler.SetAnim(AnimHandler.State.Attack);
       
        enemy.TakeDamage(damage, m_DamageType);
    }
}