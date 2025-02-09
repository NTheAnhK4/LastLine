using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;

    public void Init(float hp)
    {
        maxHealth = hp;
        curHealth = hp;
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Debug.Log("I'm dead");
        }
    }
}
