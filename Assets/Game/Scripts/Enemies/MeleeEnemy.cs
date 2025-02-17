
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
        var enemyData = Data.MeleeEnemys[enemyId];

        enemyMove.Init(enemyData.MoveSpeed);
        enemyHealth.Init(enemyData.HealthPoint);
        enemyAttack.Init(enemyData.AttackRange, enemyData.AttackSpeed, enemyData.Damage);
        enemyDead.Init(enemyData.RewardGold, enemyData.Damage);

        
    }
}
