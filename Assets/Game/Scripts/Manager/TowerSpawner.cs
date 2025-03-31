using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : ComponentBehavior
{
    private TowerData m_TowerData;
    private LevelParam m_LevelParam;
    public void Init(LevelParam levelParam)
    {
        m_LevelParam = levelParam;
        m_TowerData = DataManager.Instance.GetData<TowerData>();
    }
    public void SpawnTower()
    {
        
        for (int i = 0; i < m_LevelParam.TowerInfors.Count; ++i)
        {
            Vector3 position = m_LevelParam.TowerInfors[i].Towerposition;
            int towerId = m_LevelParam.TowerInfors[i].TowerId;
            Tower tower = PoolingManager.Spawn(m_TowerData.Towers[towerId].TowerPrefab,position,default,transform).GetComponent<Tower>();
            tower.Init(towerId,m_LevelParam.TowerInfors[i].flagPosition);
        }
    }
}
