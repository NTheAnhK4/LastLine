
using System.IO;
using UnityEngine;

public static class GameFactory
{
    public static GameObject GetEnemyPrefab(EnemyType enemyType, int enemyId)
    {
        EnemyData enemyData = DataManager.Instance.GetData<EnemyData>();
        if (enemyData == null)
        {
            Debug.LogWarning("Enemy data is not loaded");
            return null;
        }
        GameObject enemyPrefab = null;
        switch (enemyType)
        {
            case EnemyType.MeleeEnemy:
                if (enemyId < 0 || enemyId >= enemyData.MeleeEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                
                else enemyPrefab = enemyData.MeleeEnemies[enemyId].EnemyPrefab;
                break;
            case EnemyType.RangedEnemy:
                if (enemyId < 0 || enemyId >= enemyData.RangedEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                else enemyPrefab = enemyData.RangedEnemies[enemyId].EnemyPrefab;
                break;
            case EnemyType.FlyEnemy:
                if (enemyId < 0 || enemyId >= enemyData.FlyEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                else enemyPrefab = enemyData.FlyEnemies[enemyId].EnemyPrefab;
                break;
        }

        return enemyPrefab;
    }

    public static float GetEnemyDifficultyWeight(EnemyType enemyType, int enemyID)
    {
        
        EnemyData enemyData = DataManager.Instance.GetData<EnemyData>();
        if (enemyData == null)
        {
            Debug.LogWarning("Enemy data is not loaded");
            return -1;
        }
        float difficultyWeight = 0;
        switch (enemyType)
        {
            case EnemyType.MeleeEnemy:
                if (enemyID < 0 || enemyID >= enemyData.MeleeEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                else difficultyWeight = enemyData.MeleeEnemies[enemyID].DifficultyWeight;
                break;
            case EnemyType.RangedEnemy:
                if (enemyID < 0 || enemyID >= enemyData.RangedEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                else difficultyWeight = enemyData.RangedEnemies[enemyID].DifficultyWeight;
                break;
            case EnemyType.FlyEnemy:
                if (enemyID < 0 || enemyID >= enemyData.FlyEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                else difficultyWeight = enemyData.FlyEnemies[enemyID].DifficultyWeight;
                break;
        }

        return difficultyWeight;
    }
    // used for parem for ISkill
    public static SkillParam GetSkillParam(SkillType skillType, int skillId)
    {
        SkillParam skillParam = null;
        switch (skillType)
        {
            case SkillType.Summon:
                if (skillId < 0 || skillId >= DataManager.Instance.GetData<SkillData>()?.SummonSkills.Count)
                    Debug.LogWarning("skill id is run out of index");
                else skillParam = DataManager.Instance.GetData<SkillData>()?.SummonSkills[skillId];
                break;
            case SkillType.Buff:
                if (skillId < 0 || skillId >= DataManager.Instance.GetData<SkillData>()?.BuffSkills.Count)
                    Debug.LogWarning("Skill id is run out of index");
                else skillParam = DataManager.Instance.GetData<SkillData>()?.BuffSkills[skillId];
                break;
        }

        return skillParam;
    }

    public static ISkill GetSkill(SkillType skillType, int skillId, GameObject actor)
    {
        ISkill skill = null;
        switch (skillType)
        {
            case SkillType.Summon:
                skill = new SummonSkill(GetSkillParam(skillType, skillId), actor);
                break;
            case SkillType.Buff:
                skill = new BuffSkill(GetSkillParam(skillType, skillId), actor);
                break;
            
        }

        return skill;
    }

    public static IBuff GetBuff(BuffType buffType, int buffId, GameObject target)
    {
        if (buffId < 0 || buffId >= DataManager.Instance.GetData<BuffData>().Buffs.Count) return null;
        IBuff buff = null;
        switch (buffType)
        {
            case BuffType.PhysicalDefense:
                buff = new PhysicalDefenseBuff(DataManager.Instance.GetData<BuffData>().Buffs[buffId], target);
                break;
        }

        return buff;
    }
    
}
