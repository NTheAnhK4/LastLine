
public class FlyEnemy : Enemy
{
    public override void Init(int enemyId, NodePathParam nodePathParam)
    {
        m_EnemyId = enemyId;
      
        if (DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies[enemyId].EnemySkills.Count > 0)
        {
            SkillHandler skillHandler = transform.GetComponentInChildren<SkillHandler>();
            if(skillHandler != null) skillHandler.Init(DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies[enemyId].EnemySkills);
        }
        EnemyParam enemyData = DataManager.Instance.GetData<EnemyData>()?.FlyEnemies[enemyId];
        if (enemyData != null)
        {
            enemyMove.Init(nodePathParam, enemyData.MoveSpeed, enemyData.AttackRange);
            enemyHealth.Init(enemyData.HealthPoint, enemyData.PhysicalDamageReduction, enemyData.MagicalDamageReduction);
            enemyDead.Init(enemyData.RewardGold, enemyData.DamageToTower);
        }

        animHandler.SetAnim(AnimHandler.State.Move);
    }
}
