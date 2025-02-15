
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public List<LevelParam> Levels;
}

[System.Serializable]
public class LevelParam
{
    public List<WayParam> Ways;
    public List<PathParam> Paths;
    public List<Vector3> TowerPositions;
}

[System.Serializable]
public class PathParam
{
    public List<Vector3> Positions;
}

[System.Serializable]
public class MiniWayParam
{
    public int PathId;
    public List<EnemyInfor> EnemyInfors;
}

[System.Serializable]
public class EnemyInfor
{
    public int EnemyId;
    public EnemyType EnemyType;
}
[System.Serializable]
public class WayParam
{
    public List<MiniWayParam> MiniWays;
}

public enum EnemyType
{
    MeleeAttack,
    RangedAttack
}