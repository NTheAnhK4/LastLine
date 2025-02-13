
using UnityEngine;

public class RangedAttack : AttackHandler
{
    [SerializeField] private GameObject projectilePrefab;

    public void Init(float aRange, float aSpeed, GameObject proPrefab)
    {
        base.Init(aRange,aSpeed);
        projectilePrefab = proPrefab;
    }
    protected override void Attack(HealthHandler enemy)
    {
        base.Attack(enemy);
        
        Vector3 enemyPosition = enemy.Actor.position;
        var position = actor.position;
        Vector3 direction = (enemyPosition - position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Projectile projectile = PoolingManager.Spawn(projectilePrefab, position, Quaternion.Euler(new Vector3(0,0,angle)), transform)
            .GetComponent<Projectile>();
        if(projectile != null) projectile.Init(enemy.Actor);
    }
}
