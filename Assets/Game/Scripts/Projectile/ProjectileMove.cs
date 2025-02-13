
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] private Transform actor;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private Vector3 currentDirection;
    [SerializeField] private float bulletSpeed;
    
    public void Init(Transform actorTrf, Transform enemyTrf, float speed)
    {
        actor = actorTrf;
        bulletSpeed = speed;
        enemyTarget = enemyTrf;
    }
    private void SetDirection()
    {
       
        currentDirection = (enemyTarget.position - actor.position).normalized;
    }
    private void Move()
    {
        SetDirection();
        actor.Translate(currentDirection* (bulletSpeed * Time.deltaTime), Space.World);
    }

    private void Update()
    {
        Move();
    }
}
