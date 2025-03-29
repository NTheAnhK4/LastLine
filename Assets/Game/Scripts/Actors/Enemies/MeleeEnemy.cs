

using UnityEngine;

public class MeleeEnemy : Enemy
{
   [SerializeField] private MeleeAttack enemyAttack;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyAttack == null) enemyAttack = transform.GetComponentInChildren<MeleeAttack>();
        
    }

    public override void Init(int enemyId, int pathId)
    {
        m_EnemyId = enemyId;
        m_PathId = pathId;
        if (DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies[enemyId].EnemySkills.Count > 0)
        {
            SkillHandler skillHandler = transform.GetComponentInChildren<SkillHandler>();
            if(skillHandler != null) skillHandler.Init(DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies[enemyId].EnemySkills);
        }
        MeleeEnemyParam enemyData = DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies[enemyId];
        if (enemyData != null)
        {
            enemyMove.Init(InGameManager.Instance.GetPath(m_PathId), enemyData.MoveSpeed, enemyData.AttackRange);
            enemyHealth.Init(enemyData.HealthPoint, enemyData.PhysicalDamageReduction, enemyData.MagicalDamageReduction);
            enemyAttack.Init(enemyData.AttackRange, enemyData.AttackSpeed, enemyData.Damage, enemyData.DamageType);
            enemyDead.Init(enemyData.RewardGold, enemyData.DamageToTower);
        }

        animHandler.SetAnim(AnimHandler.State.Move);
    }
    
}
