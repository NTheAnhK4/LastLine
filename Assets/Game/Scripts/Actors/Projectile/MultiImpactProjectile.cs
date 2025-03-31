
using UnityEngine;

public class MultiImpactProjectile : SingleImpactProjectile
{
    protected override void AfterImpact()
    {
        base.AfterImpact();
        Collider[] hitEnemies = Physics.OverlapSphere(transform.parent.parent.position, 2.5f);
        foreach (Collider enemy in hitEnemies)
        {
            if(!IsEnemy(enemy.transform)) continue;
            HealthHandler healthHandler = enemy.transform.GetComponentInChildren<HealthHandler>();
            if(healthHandler == null) continue;
            healthHandler.TakeDamage(damage * 2/3,m_DamageType);
        }
    }
}
