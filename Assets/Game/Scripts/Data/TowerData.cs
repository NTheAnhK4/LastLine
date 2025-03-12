
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/TowerData", fileName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public List<TowerParam> Towers;
}

[System.Serializable]
public class TowerParam
{
    public string TowerName;
    public int Cost;
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
    
    public int TowerId;
}

