
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private MeleeAttack enemyAttack;
    public MeleeEnemyData Data;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<MeleeAttack>();
        ApplyData();
    }

    protected override void ApplyData()
    {
        enemyMove.Init(Data.MeleeEnemys[enemyId].MoveSpeed);
        enemyHealth.Init(Data.MeleeEnemys[enemyId].HealthPoint);
        enemyAttack.Init(Data.MeleeEnemys[enemyId].AttackRange,
            Data.MeleeEnemys[enemyId].AttackSpeed,
            Data.MeleeEnemys[enemyId].Damage);
        
    }
}
