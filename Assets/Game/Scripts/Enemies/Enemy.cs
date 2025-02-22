

using UnityEngine;

public class Enemy : ComponentBehavior
{
    [SerializeField] protected EnemyMove enemyMove;
    [SerializeField] protected HealthHandler enemyHealth;
    
    public EnemyDead enemyDead { get; private set; }
  
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyMove == null) enemyMove = transform.GetComponentInChildren<EnemyMove>();
        if (enemyHealth == null) enemyHealth = transform.GetComponentInChildren<HealthHandler>();
        
        if (enemyDead == null) enemyDead = transform.GetComponentInChildren<EnemyDead>();
       
    }
    
    
}
