
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackHandler : MonoBehaviour
{
    [SerializeField] protected List<HealthHandler> detectedEnemies;
    protected HashSet<HealthHandler> priorityTargets = new HashSet<HealthHandler>();
    
    [SerializeField] protected Transform actor;
    [SerializeField] private CircleCollider2D collid;
    
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float coolDown;
    

    public void Init(Transform actorTrf, float aRange, float aSpeed)
    {
        actor = actorTrf;
        if (collid == null) collid = transform.GetComponentInChildren<CircleCollider2D>();
        attackRange = aRange;
        collid.radius = attackRange;
        attackSpeed = aSpeed;
        coolDown = 0;

    }
    private bool IsEnemy(Transform obj)
    {
        if (actor.tag.Equals("Tower")) return obj.tag.Equals("Enemy");
        else if(actor.tag.Equals("Enemy")) return obj.tag.Equals("Tower");
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        if (IsEnemy(other.transform))
        {
            HealthHandler healthHandler = other.transform.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) detectedEnemies.Add(healthHandler);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        if (IsEnemy(other.transform))
        {
            HealthHandler healthHandler = other.transform.GetComponentInChildren<HealthHandler>();
            if(healthHandler != null) detectedEnemies.Remove(healthHandler);
        }
    }

    private HealthHandler GetPriorityTarget()
    {
        priorityTargets.RemoveWhere(enemy => enemy == null || enemy.IsDead);
        detectedEnemies.RemoveAll(enemy => enemy == null || enemy.IsDead);
        foreach (var enemy in priorityTargets)
        {
            if (enemy != null && detectedEnemies.Contains(enemy)) return enemy;
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
            if(enemy != null) Attack(enemy);
            coolDown = 0;
        }
    }

    protected virtual void Attack(HealthHandler enemy)
    {
        priorityTargets.Add(enemy);
    }
}
