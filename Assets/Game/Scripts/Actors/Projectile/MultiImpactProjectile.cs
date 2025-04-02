
using UnityEngine;

public class MultiImpactProjectile : SingleImpactProjectile
{
  
    protected override void AfterImpact()
    {
        base.AfterImpact();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.parent.parent.position, 1.5f);
       
        foreach (Collider2D enemy in hitEnemies)
        {
            if(!IsEnemy(enemy.transform)) continue;
            HealthHandler healthHandler = enemy.transform.GetComponentInChildren<HealthHandler>();
            if(healthHandler == null) continue;
            healthHandler.TakeDamage(damage * 2/3,m_DamageType);
        }

        
    }
}
