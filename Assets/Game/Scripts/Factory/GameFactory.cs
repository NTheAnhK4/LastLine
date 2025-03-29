
using System.IO;
using UnityEngine;

public static class GameFactory
{
    public static GameObject GetEnemyPrefab(EnemyType enemyType, int enemyId)
    {
        GameObject enemyPrefab = null;
        switch (enemyType)
        {
            case EnemyType.MeleeAttack:
                if (enemyId < 0 || enemyId >= DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                
                else enemyPrefab = DataManager.Instance.GetData<EnemyData>()?.MeleeEnemies[enemyId].EnemyPrefab;
                break;
            case EnemyType.RangedAttack:
                if (enemyId < 0 || enemyId >= DataManager.Instance.GetData<EnemyData>()?.RangedEnemies.Count)
                    Debug.LogWarning("Enemy id is run out of index");
                else enemyPrefab = DataManager.Instance.GetData<EnemyData>()?.RangedEnemies[enemyId].EnemyPrefab;
                break;
        }

        return enemyPrefab;
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
