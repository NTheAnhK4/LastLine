
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ComponentBehavior
{
    
    [SerializeField] protected EnemyMove enemyMove;
    [SerializeField] protected HealthHandler enemyHealth;
    [SerializeField] protected AnimHandler animHandler;
  
    public EnemyDead enemyDead { get; private set; }

    public EnemyMove EnemyMove => enemyMove;
    protected int m_EnemyId;
    protected int m_PathId;

    public int EnemyId => m_EnemyId;

    public int PathId => m_PathId;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyMove == null) enemyMove = transform.GetComponentInChildren<EnemyMove>();
        if (enemyHealth == null) enemyHealth = transform.GetComponentInChildren<HealthHandler>();
        
        if (enemyDead == null) enemyDead = transform.GetComponentInChildren<EnemyDead>();
        if (animHandler == null) animHandler = transform.GetComponentInChildren<AnimHandler>();

    }

    public virtual void Init(int enemyId, int pathId)
    {
        
    }

   
}
