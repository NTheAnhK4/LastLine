
using UnityEngine;

public class RangedAttack : AttackHandler
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector3 projectilePosition;

    [SerializeField] private int m_ActorLevel;

    private float m_Damage;
    private float angle;
    private HealthHandler enemyTarget;
   
    public void Init(float aRange, float aSpeed, float damage, GameObject proPrefab, int actorLevel = 1)
    {
        base.Init(aRange,aSpeed);
        projectilePrefab = proPrefab;
        m_ActorLevel = actorLevel;
        m_Damage = damage;
    }

    
    protected override void Attack(HealthHandler enemy)
    {
        base.Attack(enemy);
        enemyTarget = enemy;
        Vector3 enemyPosition = enemy.Actor.position;
        var position = actor.position;
        Vector3 direction = (enemyPosition - position);

       angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        SetDirection();
       
       
        animHandler.SetAnim(AnimHandler.State.Attack);

       
    }

    public void Shoot()
    {
        Projectile projectile = PoolingManager.Spawn(projectilePrefab, 
                actor.transform.TransformPoint(projectilePosition), 
                Quaternion.Euler(new Vector3(0,0,angle)), 
                transform)
            .GetComponent<Projectile>();
        if(projectile != null) projectile.Init(enemyTarget.Actor,m_Damage, m_ActorLevel);
    }
    

    private void SetDirection()
    {
        if(angle is < 45 and >= 0) animHandler.SetInt("direction",1);
        else if(angle is < 90 and >= 45) animHandler.SetInt("direction",0);
        
        else if(angle is >= -45 and < 0) animHandler.SetInt("direction",2);
        else if(angle is >= -90 and < -45) animHandler.SetInt("direction",3);
        
        else if(angle is >= -135 and < -90) animHandler.SetInt("direction",4);
        else if(angle is >= -180 and < -135) animHandler.SetInt("direction",5);
        
        else if(angle is >= 90 and < 135) animHandler.SetInt("direction",7);
        else animHandler.SetInt("direction",6);
    }
}