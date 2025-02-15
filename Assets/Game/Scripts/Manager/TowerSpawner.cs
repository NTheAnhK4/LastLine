using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : ComponentBehavior
{
    public LevelData LevelData;
    public TowerData TowerData;
    public void SpawnTower()
    {
        
        List<Vector3> towerParams = LevelData.Levels[GameManager.Instance.Level].TowerPositions;
        for (int i = 0; i < towerParams.Count; ++i)
        {
            Vector3 position = towerParams[i];
            PoolingManager.Spawn(TowerData.Towers[0].TowerPrefab,position,default,transform);
        }
    }
}
