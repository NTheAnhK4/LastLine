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
    public int TowerHealth = 20;
    public int InitialGold;
    public GameObject LevelPrefab;
    public Vector2 MinLimitCamera;
    public Vector2 MaxLimitCamera;
    public List<WayParam> Ways;
    [Header("Path")]
    public List<RootParam> Roots;
    public List<NodePathParam> NodePaths;
    [Header("Tower")]
    public List<TowerInfor> TowerInfors;
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
public class MiniWayParam
{
    public int RootID;
   
    public List<EnemyInfor> EnemyInfors;
}

[Serializable]
public class EnemyInfor
{
    [Header("Enemy Id")]
    public int EnemyId;
    [Header("Spawn Delay")]
    public float SpawnDelay = 2;
    public EnemyType EnemyType;
}
[Serializable]
public class WayParam
{
    public List<MiniWayParam> MiniWays;
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