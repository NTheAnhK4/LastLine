using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] private Transform actor;
    [SerializeField] private float bulletSpeed;

    public void Init(Transform actorTrf, float speed)
    {
        actor = actorTrf;
        bulletSpeed = speed;
    }

    private void Awake()
    {
        Init(transform.parent,5);
    }

    private void Move()
    {
        actor.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }
}
