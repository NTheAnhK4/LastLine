
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/TowerData", fileName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public List<TowerParam> TowerList;
}

[System.Serializable]
public class TowerParam
{
    public string TowerName;
    public float AttackRange;
    public float AttackSpeed;
    public GameObject UnitPrefab;
    public GameObject TowerPrefab;
    public GameObject TowerUIPrefab;
    public List<TowerUpgradeParam> TowerUpgradeList;
}

[System.Serializable]
public class TowerUpgradeParam
{
    public int Cost;
    public int TowerId;
}

