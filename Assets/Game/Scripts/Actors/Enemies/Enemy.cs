

using UnityEngine;

public class Enemy : ComponentBehavior
{
    [SerializeField] protected EnemyMove enemyMove;
    [SerializeField] protected HealthHandler enemyHealth;
    [SerializeField] protected AnimHandler animHandler;
    
    public EnemyDead enemyDead { get; private set; }

    public EnemyMove EnemyMove => enemyMove;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyMove == null) enemyMove = transform.GetComponentInChildren<EnemyMove>();
        if (enemyHealth == null) enemyHealth = transform.GetComponentInChildren<HealthHandler>();
        
        if (enemyDead == null) enemyDead = transform.GetComponentInChildren<EnemyDead>();
        if (animHandler == null) animHandler = transform.GetComponentInChildren<AnimHandler>();

    }
    
    
}
