
using UnityEngine;

public class ProjectileMove : ComponentBehavior
{
    [SerializeField] private Transform actor;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private Vector3 currentDirection;
    [SerializeField] private float bulletSpeed;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        actor = transform.parent.parent;
    }

    public void Init( Transform enemyTrf, float speed)
    {
       
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
