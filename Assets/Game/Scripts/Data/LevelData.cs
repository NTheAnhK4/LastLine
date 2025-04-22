using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public List<LevelParam> Levels;
}

[Serializable]
public class LevelParam
{
    public bool IsBossLevel = false;
    public int TowerHealth = 20;
    public int InitialGold;
    public GameObject LevelPrefab;
    public Vector2 MinLimitCamera;
    public Vector2 MaxLimitCamera;
    
    [Header("Path")]
    public List<RootParam> Roots;
    public List<NodePathParam> NodePaths;
    [Header("Tower")]
    public List<TowerInfor> TowerInfors;

    [Header("Way")] public List<EnemyLevelConfig> AllowedEnemyConfigs;
    public int MinTotalWay = 3;
    public int MaxTotalWay = 10;


}

[Serializable]
public class NodePathParam
{
    public Vector3 Point;
    public List<int> ChildID;
}

[Serializable]
public class RootParam : NodePathParam
{
    public Vector3 SignalPosition;
    public float SignalAngle;
}

[Serializable]
public class EnemyLevelConfig
{
    public int EnemyID;
    public EnemyType EnemyType;
    public int Min;
    public int Max;
    public int MinWayAllowed;
    public int MaxWaveAllowed;
}

[Serializable]
public class TowerInfor
{
    public Vector3 Towerposition;
    public Vector3 flagPosition;
    public int TowerId = 0;
}
public enum EnemyType
{
    MeleeEnemy,
    RangedEnemy,
    FlyEnemy
}