using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : ComponentBehavior
{
    public LevelData LevelData;
    public TowerData TowerData;
    public void SpawnTower()
    {
        for (int i = 0; i < LevelData.Levels[GameManager.Instance.Level].TowerPositions.Count; ++i)
        {
            Vector3 position = LevelData.Levels[GameManager.Instance.Level].TowerPositions[i];
            PoolingManager.Spawn(TowerData.Towers[0].TowerPrefab,position,default,transform);
        }
    }
}
