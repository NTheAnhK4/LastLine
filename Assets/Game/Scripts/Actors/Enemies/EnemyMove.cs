
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MoveHandler
{
    [SerializeField] private Enemy enemyCtrl;
    [Header("Move information")]
    private List<Vector3> m_PathList;
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int currentPathIndex;
    [SerializeField] protected float moveSpeed;

    protected Vector3 direction;
    [Header("Target")]
    [SerializeField] private List<HealthHandler> enemyTargets;

    [SerializeField] private float m_AttackRange;
    private HealthHandler targetEnemy;
    public Vector3 Direction
    {
        get => direction;
        set
        {
            if(direction.x < 0) animHandler.RotateAnim(false);
            else animHandler.RotateAnim(true);
            direction = value;
        }
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyCtrl == null) enemyCtrl = transform.GetComponentInParent<Enemy>();
    }

    public void Init(List<Vector3> pathList, float eMoveSpeed, float aRange)
    {
        m_AttackRange = aRange;
        m_PathList = pathList;
        moveSpeed = eMoveSpeed;
        if(animHandler != null) animHandler.SetAnim(AnimHandler.State.Move);
        currentPathIndex = 1;
        targetPosition = m_PathList[currentPathIndex];
    }


    protected void MoveByPath()
    {
        Direction = (targetPosition - enemyCtrl.transform.position).normalized;
        enemyCtrl.transform.Translate(direction * (moveSpeed * Time.deltaTime));
        if (Vector3.Distance(enemyCtrl.transform.position, targetPosition) <= 0.4f)
        {
            if (currentPathIndex == m_PathList.Count - 1) enemyCtrl.enemyDead.OnDead(false);
            else SetNextPosition();
        }
    }
    

    private void SetNextPosition()
    {
        currentPathIndex++;
        targetPosition = m_PathList[currentPathIndex];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Radar")) return;
        
        if (other.tag.Equals("Solider"))
        {
            HealthHandler healthHandler = other.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) enemyTargets.Add(healthHandler);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag.Equals("Radar")) return;
        
        if (other.tag.Equals("Solider"))
        {
            HealthHandler healthHandler = other.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) enemyTargets.Remove(healthHandler);
        }
    }

    private HealthHandler GetEnemy()
    {
        enemyTargets.RemoveAll(enemy => enemy.IsDead || enemy == null);
        if (enemyTargets.Count == 0) return null;
        HealthHandler enemyClosest = null;
        float minDistance = float.MaxValue;
        foreach (var enemy in enemyTargets)
        {
            float distance = Vector3.Distance(actor.position, enemy.Actor.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemyClosest = enemy;
            }
        }

        return enemyClosest;
    }
    private void MoveToTarget(Vector3 target)
    {
        float distance = Vector3.Distance(target, actor.position);
        if (distance <= m_AttackRange)
        {
            animHandler.SetAnim(AnimHandler.State.Idle);
            return;
        }
        animHandler.SetAnim(AnimHandler.State.Move);
        Vector3 direction = (target - actor.position).normalized;
        actor.transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    private void Update()
    {
        
        if (animHandler.currentState == AnimHandler.State.Attack) return;
        if(animHandler.currentState == AnimHandler.State.Dead) return;

        if (targetEnemy == null || targetEnemy.IsDead) targetEnemy = GetEnemy();

        if (targetEnemy != null) MoveToTarget(targetEnemy.Actor.position);
        else
        {
            animHandler.SetAnim(AnimHandler.State.Move);
            if (currentPathIndex + 1 < m_PathList.Count)
            {
                if ((enemyCtrl.transform.position - m_PathList[currentPathIndex + 1]).sqrMagnitude <
                    (enemyCtrl.transform.position - m_PathList[currentPathIndex]).sqrMagnitude)
                {
                    SetNextPosition();
                }

            }
            MoveByPath();
        }
        
    }
}
