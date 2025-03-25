using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ComponentBehavior
{
    [Header("----------Data----------")]
    [SerializeField] private ProjectileData m_ProjectileData;
    [SerializeField] private int m_ProjectileId;
    [Header("----------Component----------")]
    [SerializeField] private ProjectileMove projectileMove;
    [SerializeField] private ProjectileImpact projectileImpact;
    public DeadByDistance projectileDespawn { get; private set; }
    [SerializeField] private Transform enemyTarget;
    [SerializeField] protected int m_ProjectileLevel;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (projectileMove == null) projectileMove = transform.GetComponentInChildren<ProjectileMove>();
        if (projectileDespawn == null) projectileDespawn = transform.GetComponentInChildren<DeadByDistance>();
        if (projectileImpact == null) projectileImpact = transform.GetComponentInChildren<ProjectileImpact>();
    }

    public void Init(Transform enemyTrf, int projectileLevel = 1)
    {
        enemyTarget = enemyTrf;
        m_ProjectileLevel = projectileLevel;
        projectileMove.Init(enemyTarget, m_ProjectileData.Projectiles[m_ProjectileId].speed + m_ProjectileLevel);
       
        projectileImpact.Init(enemyTarget,
            m_ProjectileData.Projectiles[m_ProjectileId].damage * m_ProjectileLevel, 
            m_ProjectileData.Projectiles[m_ProjectileId].DamageType);
    }
}
