
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private MeleeAttack enemyAttack;
    public MeleeEnemyData Data;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<MeleeAttack>();
        InitData();
    }

    protected override void InitData()
    {
        enemyMove.Init(Data.MeleeEnemyList[enemyId].MoveSpeed);
        enemyHealth.Init(Data.MeleeEnemyList[enemyId].HealthPoint);
        enemyAttack.Init(Data.MeleeEnemyList[enemyId].AttackRange,
            Data.MeleeEnemyList[enemyId].AttackSpeed,
            Data.MeleeEnemyList[enemyId].Damage);
        
    }
}
