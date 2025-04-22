
using System;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(menuName = "Data/Enemy Data",fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
   
    public List<MeleeEnemyParam> MeleeEnemies;
    public List<RangedEnemyParam> RangedEnemies;
    public List<EnemyParam> FlyEnemies;
}

[Serializable]
public class EnemyParam
{
    public int EnemyID;
    public string Name;
    public float DifficultyWeight;
   
    public float HealthPoint;
    [Range(0,1)] public float PhysicalDamageReduction;
    [Range(0,1)] public float MagicalDamageReduction;

    public float MoveSpeed;
    public DamageType DamageType;
    public float AttackRange;
    public float AttackSpeed;
    public float DamageToTower = 1;
    public int RewardGold;
    public GameObject EnemyPrefab;

    public List<EnemySkillParam> EnemySkills;
}
[Serializable]
public class MeleeEnemyParam : EnemyParam
{
    public float Damage;
}
[Serializable]
public class RangedEnemyParam : EnemyParam
{
    public float Damage;
    public GameObject ProjectilePrefab;
}

[Serializable]
public class EnemySkillParam
{
    public SkillType SkillType;
    public int SkillId;

    
}
