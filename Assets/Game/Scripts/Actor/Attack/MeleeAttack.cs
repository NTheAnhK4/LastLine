
using UnityEngine;


public class MeleeAttack : AttackHandler
{
    [SerializeField] private float damage;
    public void Init(float aRange, float aSpeed, float aDamage)
    {
        base.Init(aRange,aSpeed);
        this.damage = aDamage;
    }
}
