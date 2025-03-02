using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MeleeEnemyMove : EnemyMove
{
    
    [SerializeField] private List<HealthHandler> enemyTarget;
   
    
    private static HashSet<HealthHandler> occupiedEnemies = new HashSet<HealthHandler>();
    private HealthHandler targetEnemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Radar")) return;
        
        if (other.tag.Equals("Solider"))
        {
            HealthHandler healthHandler = other.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) enemyTarget.Add(healthHandler);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag.Equals("Radar")) return;
        
        if (other.tag.Equals("Solider"))
        {
            HealthHandler healthHandler = other.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) enemyTarget.Remove(healthHandler);
        }
    }

    private HealthHandler GetEnemy()
    {
        enemyTarget.RemoveAll(enemy => enemy == null || enemy.IsDead || occupiedEnemies.Contains(enemy));

        if (enemyTarget.Count == 0) return null; 

        HealthHandler enemyClosest = null;
        float minDistance = float.MaxValue;
        foreach (var enemy in enemyTarget)
        {
            float distance = Vector3.Distance(actor.position, enemy.Actor.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemyClosest = enemy;
            }
        }

        if (enemyClosest != null)
        {
            occupiedEnemies.Add(enemyClosest);
            targetEnemy = enemyClosest;
        }
        return enemyClosest;
    }
    private void MoveToTarget(Vector3 target)
    {
        float distance = Vector3.Distance(target, actor.position);
        if (distance <= 1)
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

        if (targetEnemy == null || targetEnemy.IsDead)
        {
            occupiedEnemies.Remove(targetEnemy);
            targetEnemy = GetEnemy();
        }

        if (targetEnemy != null)
        {
            MoveToTarget(targetEnemy.Actor.position);
        }
        else
        {
            animHandler.SetAnim(AnimHandler.State.Move);
            MoveByPath();
        }
        
    }
}
