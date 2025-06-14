
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackHandler : ActionHandler
{
    [SerializeField] protected List<HealthHandler> detectedEnemies = new List<HealthHandler>();
    protected readonly HashSet<HealthHandler> priorityTargets = new HashSet<HealthHandler>();
   
    [SerializeField] private CircleCollider2D collid;
    
    [SerializeField] private float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] private float coolDown;
   
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (collid == null) collid = transform.GetComponentInChildren<CircleCollider2D>();
    }

    public void Init(float sRange, float aSpeed)
    {
        detectedEnemies.Clear();
        priorityTargets.Clear();
        attackRange = sRange;
        collid.radius = attackRange;
        attackSpeed = aSpeed;
        coolDown = 0;
        
        

    }

    private void OnDisable()
    {
        detectedEnemies.Clear();
        priorityTargets.Clear();
    }

    private bool IsEnemy(Transform obj)
    {
        if (actor.tag.Equals("Tower")) return obj.tag.Equals("Enemy") || obj.tag.Equals("FlyEnemy");
        if(actor.tag.Equals("Solider")) return obj.tag.Equals("Enemy");
        else if(actor.tag.Equals("Enemy")) return obj.tag.Equals("Solider");
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        HealthHandler healthHandler = other.transform.GetComponentInChildren<HealthHandler>();
        if(healthHandler == null) return;
        if (IsEnemy(other.transform)) detectedEnemies.Add(healthHandler);
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        HealthHandler healthHandler = other.transform.GetComponentInChildren<HealthHandler>();
        if(healthHandler == null) return;
        if (IsEnemy(other.transform)) detectedEnemies.Remove(healthHandler);
    }

    private HealthHandler GetPriorityTarget()
    {
        priorityTargets.RemoveWhere(enemy => enemy == null || enemy.IsDead || !enemy.gameObject.activeInHierarchy);
        detectedEnemies.RemoveAll(enemy => enemy == null || enemy.IsDead || !enemy.gameObject.activeInHierarchy);
        foreach (var enemy in priorityTargets)
        {
            if (enemy != null && detectedEnemies.Contains(enemy) && enemy.gameObject.activeInHierarchy) return enemy;
        }

        HealthHandler enemyClosest = null;
        float minDistance = float.MaxValue;
        foreach (HealthHandler enemy in detectedEnemies)
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

    private void Update()
    {
        
        coolDown += Time.deltaTime;
        if (coolDown >= attackSpeed)
        {
            HealthHandler enemy = GetPriorityTarget();
            if (enemy != null) Attack(enemy);
            else
            {
                if(animHandler.currentState == AnimHandler.State.Attack) animHandler.RevertToPreviousAnim();
            }
            coolDown = 0;
        }
        
    }
    

   
    protected virtual void Attack(HealthHandler enemy)
    {
        priorityTargets.Add(enemy);
       
    }

   
   
}
