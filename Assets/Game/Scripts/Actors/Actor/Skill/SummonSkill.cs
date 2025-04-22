
using UnityEngine;

public class SummonSkill : ISkill
{
    public SummonSkill(SkillParam skillParam, GameObject actor) : base(skillParam, actor)
    {
    }

    protected override void DoSkill()
    {
        
        if (m_SkillParam is not SummonSkillParam summonSkillParam)
        {
            Debug.LogWarning("Type of data for summon skill is wrong");
            return;
        }

        GameObject objectPrefab = GameFactory.GetEnemyPrefab(summonSkillParam.EnemyType, summonSkillParam.EnemyId);
        if (objectPrefab != null)
        {
            Enemy spawnedEnemy = PoolingManager.Spawn(objectPrefab, m_Actor.transform.position - Vector3.left * 0.5f, Quaternion.identity).GetComponent<Enemy>();
            Enemy enemy = m_Actor.GetComponent<Enemy>();
            if (enemy != null && spawnedEnemy != null)
            {
                if(EnemyHolder.Instance != null) EnemyHolder.Instance.HoldEnemy(enemy.gameObject, objectPrefab.name);
                
                spawnedEnemy.Init(summonSkillParam.EnemyId,enemy.GetCurrentNodePath());
            }
        }
       
       
    }

  
}