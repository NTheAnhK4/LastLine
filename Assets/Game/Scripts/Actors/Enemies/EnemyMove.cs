
using System.Collections.Generic;
using UnityEngine;



public class EnemyMove : MoveHandler
{
    [SerializeField] private Enemy enemyCtrl;
    [Header("Move information")]
    private List<Vector3> m_PathList;
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int m_CurrentPathIndex;
   

    protected Vector3 direction;
    [Header("Target")]
    [SerializeField] private List<HealthHandler> enemyTargets = new List<HealthHandler>();

    [SerializeField] private float m_AttackRange;
    [SerializeField] private HealthHandler targetEnemy;
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

        m_CurrentPathIndex = GetCurrentIndex();
        targetPosition = m_PathList[m_CurrentPathIndex];

        targetEnemy = null;
        enemyTargets.Clear();
    }


    protected void MoveByPath()
    {
        Direction = (targetPosition - enemyCtrl.transform.position).normalized;
        enemyCtrl.transform.Translate(direction * (moveSpeed * Time.deltaTime));
        if (Vector3.Distance(enemyCtrl.transform.position, targetPosition) <= 0.4f)
        {
            if (m_CurrentPathIndex == m_PathList.Count - 1) enemyCtrl.enemyDead.OnDead(false);
            else SetNextPosition();
        }
    }
    

    private void SetNextPosition()
    {
        m_CurrentPathIndex++;
        targetPosition = m_PathList[m_CurrentPathIndex];
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
        actor.transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }
    private void Update()
    {
        
        if (animHandler.currentState == AnimHandler.State.Attack||
            animHandler.currentState == AnimHandler.State.Dead||
            animHandler.currentState == AnimHandler.State.DoSkill) return;
       
        if (targetEnemy == null || targetEnemy.IsDead) targetEnemy = GetEnemy();

        if (targetEnemy != null) MoveToTarget(targetEnemy.Actor.position);
        else
        {
            animHandler.SetAnim(AnimHandler.State.Move);
            if (m_CurrentPathIndex + 1 < m_PathList.Count)
            {
                if ((enemyCtrl.transform.position - m_PathList[m_CurrentPathIndex + 1]).sqrMagnitude <
                    (enemyCtrl.transform.position - m_PathList[m_CurrentPathIndex]).sqrMagnitude)
                {
                    SetNextPosition();
                }

            }
            MoveByPath();
        }
        
    }

    private int GetCurrentIndex()
    {
        int pathIndex = 0;
        float minDistance = float.MaxValue;
        for (int i = 0; i < m_PathList.Count; ++i)
        {
            float distance = Vector3.Distance(actor.position, m_PathList[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                pathIndex = i;
            }
        }

        return pathIndex;
    }
}
