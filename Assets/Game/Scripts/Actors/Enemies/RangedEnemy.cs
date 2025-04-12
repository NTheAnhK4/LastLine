
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private RangedAttack enemyAttack;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<RangedAttack>();
    }

    public override void Init(int enemyId, NodePathParam nodePathParam)
    {
        m_EnemyId = enemyId;
       
        if (DataManager.Instance.GetData<EnemyData>()?.RangedEnemies[enemyId].EnemySkills.Count > 0)
        {
            SkillHandler skillHandler = transform.GetComponentInChildren<SkillHandler>();
            if(skillHandler != null) skillHandler.Init(DataManager.Instance.GetData<EnemyData>()?.RangedEnemies[enemyId].EnemySkills);
        }
        RangedEnemyParam enemyData = DataManager.Instance.GetData<EnemyData>()?.RangedEnemies[enemyId];
        enemyMove.Init(nodePathParam,enemyData.MoveSpeed, enemyData.AttackRange);
        enemyHealth.Init(enemyData.HealthPoint, enemyData.PhysicalDamageReduction, enemyData.MagicalDamageReduction);
        enemyDead.Init(enemyData.RewardGold, enemyData.DamageToTower);
        animHandler.SetAnim(AnimHandler.State.Move);
        enemyAttack.Init(enemyData.AttackRange,enemyData.AttackSpeed,enemyData.ProjectilePrefab);
    }
}
