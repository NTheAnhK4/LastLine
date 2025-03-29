
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
   [SerializeField] private MeleeAttack enemyAttack;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<MeleeAttack>();
        
    }

    public void Init(MeleeEnemyParam enemyData, List<Vector3> pathList)
    {
        EData = enemyData;
        enemyMove.Init(pathList,enemyData.MoveSpeed, enemyData.AttackRange);
        enemyHealth.Init(enemyData.HealthPoint, enemyData.PhysicalDamageReduction, enemyData.MagicalDamageReduction);
        enemyAttack.Init(enemyData.AttackRange, enemyData.AttackSpeed, enemyData.Damage, enemyData.DamageType);
        enemyDead.Init(enemyData.RewardGold, enemyData.DamageToTower);
        animHandler.SetAnim(AnimHandler.State.Move);
    }
    
}
