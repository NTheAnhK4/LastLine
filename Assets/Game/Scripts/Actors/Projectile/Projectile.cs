using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : ComponentBehavior
{
    [Header("----------Data----------")]
    [SerializeField] private int m_ProjectileId;
    [Header("----------Component----------")]
    [SerializeField] private ProjectileMove projectileMove;
    [SerializeField] private SingleImpactProjectile m_ProjectileImpact;
    public DeadByDistance projectileDespawn { get; private set; }
    [SerializeField] private Transform enemyTarget;
    [SerializeField] protected int m_ProjectileLevel;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (projectileMove == null) projectileMove = transform.GetComponentInChildren<ProjectileMove>();
        if (projectileDespawn == null) projectileDespawn = transform.GetComponentInChildren<DeadByDistance>();
        if (m_ProjectileImpact == null) m_ProjectileImpact = transform.GetComponentInChildren<SingleImpactProjectile>();
    }

    public void Init(Transform enemyTrf, int projectileLevel = 1)
    {
        ProjectileData m_ProjectileData = DataManager.Instance.GetData<ProjectileData>();
        if (m_ProjectileData != null)
        {
            enemyTarget = enemyTrf;
            m_ProjectileLevel = projectileLevel;
            projectileMove.Init(enemyTarget, m_ProjectileData.Projectiles[m_ProjectileId].speed + m_ProjectileLevel);
       
            m_ProjectileImpact.Init(enemyTarget,
                m_ProjectileData.Projectiles[m_ProjectileId].damage + (m_ProjectileLevel - 1) * 0.5f, 
                m_ProjectileData.Projectiles[m_ProjectileId].DamageType);
        }
        
    }
}
