
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileImpact : ComponentBehavior
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private float damage;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (projectile == null) projectile = transform.GetComponentInParent<Projectile>();
    }

    public void Init(Transform enemyTrf, float dmg)
    {
        enemyTarget = enemyTrf;
        damage = dmg;
    }

    private bool IsEnemy(Transform enemy)
    {
        if (enemy == null) return false;
        return enemy.tag.Equals(enemyTarget.tag);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        HealthHandler healthHandler = other.transform.GetComponentInChildren<HealthHandler>();
        if(healthHandler == null) return;
        if (IsEnemy(other.transform))
        {
            healthHandler.TakeDamage(damage);
            projectile.projectileDespawn.OnDead();
        }
    }
}
