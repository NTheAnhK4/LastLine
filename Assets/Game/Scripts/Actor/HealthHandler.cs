using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public Transform Actor { get; set; }
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;
    public bool IsDead;
    public void Init(Transform actorTrf, float hp)
    {
        maxHealth = hp;
        curHealth = hp;
        Actor = actorTrf;
        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            IsDead = true;
        }
    }
}
