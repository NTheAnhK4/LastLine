
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] private Transform actor;
    [SerializeField] private Transform enemyTarget;
   
    [SerializeField] private float bulletSpeed;
    
    public void Init(Transform actorTrf, Transform enemyTrf, float speed)
    {
        actor = actorTrf;
        bulletSpeed = speed;
        enemyTarget = enemyTrf;
    }

   
    private void Move()
    {
        Vector3 direction = (enemyTarget.position - actor.position).normalized;
        actor.Translate(direction * (bulletSpeed * Time.deltaTime), Space.World);
    }

    private void Update()
    {
        Move();
    }
}
