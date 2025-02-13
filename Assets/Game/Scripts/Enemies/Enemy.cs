
using System;
using UnityEngine;

public class Enemy : ComponentBehavior
{
    [SerializeField] private EnemyMove enemyMove;
    [SerializeField] private HealthHandler enemyHealth;
    [SerializeField] private AttackHandler enemyAttack;
    public DeadHandler enemyDead { get; private set; }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyMove == null) enemyMove = transform.GetComponentInChildren<EnemyMove>();
        if (enemyHealth == null) enemyHealth = transform.GetComponentInChildren<HealthHandler>();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<AttackHandler>();
        if (enemyDead == null) enemyDead = transform.GetComponentInChildren<DeadHandler>();
        InitData();
    }

    private void OnEnable()
    {
        InitData();
    }

    private void InitData()
    {
        enemyMove.Init(2f);
        enemyHealth.Init(5);
        
        enemyAttack.Init(2,3);
    }
}
