
using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackHandler : MonoBehaviour
{
    [SerializeField] protected List<HealthHandler> detectedEnemies;
    [SerializeField] protected List<HealthHandler> escapedEnemies;
    [SerializeField] protected Transform actor;
    [SerializeField] private CircleCollider2D collid;
    [SerializeField] private float attackRange;
    public void Init(Transform actorTrf, float aRange)
    {
        actor = actorTrf;
        if (collid == null) collid = transform.GetComponentInChildren<CircleCollider2D>();
        attackRange = aRange;
        collid.radius = attackRange;
    }
    public virtual void Attack(Transform enemy){}

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
            Attack(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        if (IsEnemy(other.transform))
        {
            
        }
    }
}
