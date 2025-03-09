
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileImpact : ComponentBehavior
{
    [SerializeField] protected SoundType m_SoundType;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private float damage;
    [SerializeField] private DamageType m_DamageType;
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
            PlaySound();
            healthHandler.TakeDamage(damage, m_DamageType);
            projectile.projectileDespawn.OnDead(false);
            if (m_EffectPrefab != null) PoolingManager.Spawn(m_EffectPrefab.gameObject, other.transform.position, default);
        }
    }

    protected virtual void PlaySound()
    {
        AudioManager.PlaySFX(m_SoundType,0.3f);
    }
}
