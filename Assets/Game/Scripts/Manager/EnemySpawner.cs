using System.Collections;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public MeleeEnemyData Data;
    public LevelData LevelData;

    public IEnumerator SpawnEnemyFromId(int wayId)
    {
        for (int i = wayId; i < LevelData.Levels[GameManager.Instance.Level].Ways.Count; ++i)
        {
           
            SpawnWay(LevelData.Levels[GameManager.Instance.Level].Ways[i]);
            yield return new WaitForSeconds(10);
        }
    }

    private void SpawnWay(WayParam wayParam)
    {
        for (int i = 0; i < wayParam.MiniWays.Count; ++i) StartCoroutine(SpawnMiniWay(wayParam.MiniWays[i]));
    }

    IEnumerator SpawnMiniWay(MiniWayParam miniWayParam)
    {
        
        for (int i = 0; i < miniWayParam.EnemyInfors.Count; ++i)
        {
            SpawnEnemy(miniWayParam.PathId, miniWayParam.EnemyInfors[i]);
            yield return new WaitForSeconds(2);
        }
    }

    private void SpawnEnemy(int pathId, EnemyInfor enemyInfor)
    {
        GameObject enemyPrefab = null;
        switch (enemyInfor.EnemyType)
        {
            case EnemyType.MeleeAttack:
                enemyPrefab = Data.MeleeEnemys[enemyInfor.EnemyId].EnemyPrefab;
                break;
        }
        if(enemyPrefab == null) return;
        PoolingManager.Spawn(enemyPrefab, LevelData.Levels[GameManager.Instance.Level].Paths[pathId].Positions[0],default,transform);
    }
}
