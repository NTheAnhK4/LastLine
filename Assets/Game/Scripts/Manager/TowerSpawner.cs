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
        
        for (int i = 0; i < m_LevelParam.TowerInfors.Count; ++i)
        {
            Vector3 position = m_LevelParam.TowerInfors[i].Towerposition;
            Tower tower = PoolingManager.Spawn(TowerData.Towers[0].TowerPrefab,position,default,transform).GetComponent<Tower>();
            tower.Init(0,m_LevelParam.TowerInfors[i].flagPosition);
        }
    }
}
