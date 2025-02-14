
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
        enemyMove.Init(Data.meleeEnemyList[enemyId].MoveSpeed);
        enemyHealth.Init(Data.meleeEnemyList[enemyId].HealthPoint);
        enemyAttack.Init(Data.meleeEnemyList[enemyId].AttackRange,
            Data.meleeEnemyList[enemyId].AttackSpeed,
            Data.meleeEnemyList[enemyId].Damage);
        
    }
}
