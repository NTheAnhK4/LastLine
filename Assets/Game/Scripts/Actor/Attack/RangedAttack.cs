using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackHandler
{
    [SerializeField] private GameObject projectilePrefab;

    public void Init(Transform actorTrf, float aRange, float aSpeed, GameObject proPrefab)
    {
        base.Init(actorTrf,aRange,aSpeed);
        projectilePrefab = proPrefab;
    }
    protected override void Attack(HealthHandler enemy)
    {
        base.Attack(enemy);
        PoolingManager.Spawn(projectilePrefab, actor.position, default, transform);
    }
}
