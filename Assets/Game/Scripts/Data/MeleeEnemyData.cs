
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Melee enemy",fileName = "MeleeEnemyData")]
public class MeleeEnemyData : ScriptableObject
{
    public List<MeleeEnemyParam> meleeEnemyList;
}
[System.Serializable]
public class MeleeEnemyParam
{
    public string Name;
    public float HealthPoint;

    public float MoveSpeed;

    public float Damage;
    public float AttackRange;
    public float AttackSpeed;
    public GameObject EnemyPrefab;
}
