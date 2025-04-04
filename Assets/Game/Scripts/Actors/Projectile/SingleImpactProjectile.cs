
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SingleImpactProjectile : ComponentBehavior
{
    [SerializeField] protected SoundType m_SoundType;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] protected float damage;
    [SerializeField] protected DamageType m_DamageType;
    [SerializeField] private Transform m_EffectPrefab;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (projectile == null) projectile = transform.GetComponentInParent<Projectile>();
    }

    public void Init(Transform enemyTrf, float dmg, DamageType damageType)
    {
        enemyTarget = enemyTrf;
        damage = dmg;
        m_DamageType = damageType;
    }

    protected bool IsEnemy(Transform enemy)
    {
        if (enemy == null) return false;
        if (enemyTarget.tag.Equals("Enemy")) return enemy.tag.Equals("Enemy") || enemy.tag.Equals("FlyEnemy");
        return enemy.tag.Equals(enemyTarget.tag);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        HealthHandler healthHandler = other.transform.GetComponentInChildren<HealthHandler>();
        if(healthHandler == null) return;
        if (IsEnemy(other.transform))
        {
            PlaySound();
            healthHandler.TakeDamage(damage, m_DamageType);
            projectile.projectileDespawn.OnDead(false);
            if (m_EffectPrefab != null) PoolingManager.Spawn(m_EffectPrefab.gameObject, other.transform.position, default);
            AfterImpact();
        }
    }

    protected virtual void PlaySound()
    {
        AudioManager.PlaySFX(m_SoundType,0.3f);
    }

    protected virtual void AfterImpact()
    {
        
    }
   
}
