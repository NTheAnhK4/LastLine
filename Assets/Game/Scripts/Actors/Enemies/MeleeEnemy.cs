
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public MeleeEnemyParam EData { get; private set; }
    [SerializeField] private MeleeAttack enemyAttack;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<MeleeAttack>();
        
    }

    public void Init(MeleeEnemyParam enemyData, List<Vector3> pathList, int currentPathIndex = 1)
    {
        EData = enemyData;
        enemyMove.Init(pathList,enemyData.MoveSpeed, enemyData.AttackRange,currentPathIndex);
        enemyHealth.Init(enemyData.HealthPoint, enemyData.PhysicalDamageReduction, enemyData.MagicalDamageReduction);
        enemyAttack.Init(enemyData.AttackRange, enemyData.AttackSpeed, enemyData.Damage, enemyData.DamageType);
        enemyDead.Init(enemyData.RewardGold, enemyData.DamageToTower);
        animHandler.SetAnim(AnimHandler.State.Move);
    }
    
}
