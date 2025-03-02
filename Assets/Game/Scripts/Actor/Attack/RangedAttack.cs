
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
        if(angle is >= 45 and < 135) animHandler.SetInt("direction",0);
        else if(angle is >= 0 and < 45 or < 0 and >= -45) animHandler.SetInt("direction",1);
        else if(angle is >= -135 and < -45) animHandler.SetInt("direction", 2);
        else animHandler.SetInt("direction", 3);
        animHandler.SetAnim(AnimHandler.State.Attack);
        Projectile projectile = PoolingManager.Spawn(projectilePrefab, position, Quaternion.Euler(new Vector3(0,0,angle)), transform)
            .GetComponent<Projectile>();
        if(projectile != null) projectile.Init(enemy.Actor);
    }
}
