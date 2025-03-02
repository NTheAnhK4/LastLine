
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
        enemyMove.Init(pathList,enemyData.MoveSpeed);
        enemyHealth.Init(enemyData.HealthPoint);
        enemyAttack.Init(enemyData.AttackRange, enemyData.AttackSpeed, enemyData.Damage);
        enemyDead.Init(enemyData.RewardGold, enemyData.Damage);
        animHandler.SetAnim(AnimHandler.State.Move);
    }
    
}
