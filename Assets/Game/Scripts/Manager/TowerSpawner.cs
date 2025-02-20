using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : ComponentBehavior
{
    public TowerData TowerData;
    private LevelParam m_LevelParam;
    public void Init(LevelParam levelParam)
    {
        m_LevelParam = levelParam;
    }
    public void SpawnTower()
    {
        
        List<Vector3> towerParams = m_LevelParam.TowerPositions;
        for (int i = 0; i < towerParams.Count; ++i)
        {
            Vector3 position = towerParams[i];
            PoolingManager.Spawn(TowerData.Towers[0].TowerPrefab,position,default,transform);
        }
    }
}
