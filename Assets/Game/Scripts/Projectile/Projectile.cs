using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ComponentBehavior
{
    [SerializeField] private ProjectileMove projectileMove;
    [SerializeField] private ProjectileImpact projectileImpact;
    public DeadByDistance projectileDespawn { get; private set; }
    [SerializeField] private Transform enemyTarget;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (projectileMove == null) projectileMove = transform.GetComponentInChildren<ProjectileMove>();
        if (projectileDespawn == null) projectileDespawn = transform.GetComponentInChildren<DeadByDistance>();
        if (projectileImpact == null) projectileImpact = transform.GetComponentInChildren<ProjectileImpact>();
    }

    public void Init(Transform enemyTrf)
    {
        enemyTarget = enemyTrf;
        projectileMove.Init(enemyTarget,5f);
       
        projectileImpact.Init(enemyTarget,1);
    }
}
