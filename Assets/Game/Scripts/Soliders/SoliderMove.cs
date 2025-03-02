using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SoliderMove : MoveHandler
{
    [SerializeField] private float m_speed = 1;
    
    [SerializeField] private Vector3 m_FlagPosition;
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private List<HealthHandler> enemyTarget;
   
    
    private static HashSet<HealthHandler> occupiedEnemies = new HashSet<HealthHandler>();
    private HealthHandler targetEnemy;
    public void Init(float speed, Vector3 flagPosition)
    {
       
        m_speed = speed;
        m_FlagPosition = flagPosition;
       
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Radar")) return;
        if (other.tag.Equals("Enemy"))
        {
            HealthHandler healthHandler = other.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) enemyTarget.Add(healthHandler);
        }
    }

    private HealthHandler GetEnemy()
    {
        enemyTarget.RemoveAll(enemy => enemy == null || enemy.IsDead ||
                                       Vector3.Distance(m_FlagPosition, enemy.Actor.position) >= maxDistance ||
                                       occupiedEnemies.Contains(enemy));

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
        actor.transform.Translate(direction * m_speed * Time.deltaTime);
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
            MoveToTarget(m_FlagPosition);
        }
    }
}
