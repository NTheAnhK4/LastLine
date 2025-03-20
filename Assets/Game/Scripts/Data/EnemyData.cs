
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/Enemy Data",fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [FormerlySerializedAs("MeleeEnemys")] public List<MeleeEnemyParam> MeleeEnemies;
    public List<RangedEnemyParam> RangedEnemies;
}

[Serializable]
public class EnemyParam
{
    public string Name;
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
}
[Serializable]
public class MeleeEnemyParam : EnemyParam
{
    public float Damage;
}
[Serializable]
public class RangedEnemyParam : EnemyParam
{
    public GameObject ProjectilePrefab;
}
