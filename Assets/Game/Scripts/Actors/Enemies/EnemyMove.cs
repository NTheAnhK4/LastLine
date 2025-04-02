
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemyMove : MoveHandler
{
    [SerializeField] private Enemy enemyCtrl;
    [Header("Move information")] 
    private List<NodePathParam> m_NodePaths;

    private NodePathParam m_CurrentNodePath;
    [SerializeField] private Vector3 targetPosition;
   
   

    protected Vector3 direction;

    public NodePathParam CurrentNodePath => m_CurrentNodePath;
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

    public void Init(NodePathParam currentNodePath, float eMoveSpeed, float aRange)
    {
        m_NodePaths = DataManager.Instance.GetData<LevelData>().Levels[InGameManager.Instance.CurrentLevel].NodePaths;
        m_CurrentNodePath = currentNodePath;
        m_AttackRange = aRange;
        
        moveSpeed = eMoveSpeed;
        if(animHandler != null) animHandler.SetAnim(AnimHandler.State.Move);

        SetNextPosition();

        targetEnemy = null;
        enemyTargets.Clear();
    }


    protected void MoveByPath()
    {
        Direction = (targetPosition - enemyCtrl.transform.position).normalized;
        enemyCtrl.transform.Translate(direction * (moveSpeed * Time.deltaTime));
        if (Vector3.Distance(enemyCtrl.transform.position, targetPosition) <= 0.4f)
        {
            if (IsLeaf()) enemyCtrl.enemyDead.OnDead(false);
            else SetNextPosition();
        }
    }
    

    private void SetNextPosition()
    {
        
        int childID = Random.Range(0, m_CurrentNodePath.ChildID.Count());
        int nodeID = m_CurrentNodePath.ChildID[childID];
       
        m_CurrentNodePath = m_NodePaths[nodeID];
        targetPosition = m_CurrentNodePath.Point;
    }

    private bool IsLeaf()
    {
        return m_CurrentNodePath.ChildID == null || !m_CurrentNodePath.ChildID.Any();
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
        if(actor.tag.Equals("FlyEnemy")) MoveByPath();
        else
        {
            if (targetEnemy == null || targetEnemy.IsDead || !targetEnemy.gameObject.activeInHierarchy) targetEnemy = GetEnemy();

            if (targetEnemy != null && targetEnemy.gameObject.activeInHierarchy) MoveToTarget(targetEnemy.Actor.position);
            else
            {
                animHandler.SetAnim(AnimHandler.State.Move);
                MoveByPath();
            }
        }
        
        
    }

   
}
