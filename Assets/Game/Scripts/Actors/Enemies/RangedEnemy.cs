
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public RangedEnemyParam EData { get; private set; }
    [SerializeField] private RangedAttack enemyAttack;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<RangedAttack>();
    }

    public void Init(RangedEnemyParam enemyData, List<Vector3> pathList,  int currentPathIndex = 1)
    {
        EData = enemyData;
        enemyMove.Init(pathList,enemyData.MoveSpeed, enemyData.AttackRange, currentPathIndex);
        enemyHealth.Init(enemyData.HealthPoint, enemyData.PhysicalDamageReduction, enemyData.MagicalDamageReduction);
        enemyDead.Init(enemyData.RewardGold, enemyData.DamageToTower);
        animHandler.SetAnim(AnimHandler.State.Move);
        enemyAttack.Init(enemyData.AttackRange,enemyData.AttackSpeed,enemyData.ProjectilePrefab);
    }
}
