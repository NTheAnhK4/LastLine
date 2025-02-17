using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public MeleeEnemyData Data;
    public LevelData LevelData;

    public void SpawnWays()
    {
        StartCoroutine(SpawnEnemyFromId(0));
    }
    IEnumerator SpawnEnemyFromId(int wayId)
    {
        var level = LevelData.Levels[GameManager.Instance.Level];
        for (int i = wayId; i < level.Ways.Count; i++)
        {
            ObserverManager.Notify(EventId.SpawnWay,i + 1);
            SpawnWay(level.Ways[i]);
            yield return new WaitForSeconds(10);
        }
    }

    private void SpawnWay(WayParam wayParam)
    {
        foreach (var miniWay in wayParam.MiniWays)
        {
            StartCoroutine(SpawnMiniWay(miniWay));
        }
    }

    private IEnumerator SpawnMiniWay(MiniWayParam miniWayParam)
    {
        foreach (var enemyInfor in miniWayParam.EnemyInfors)
        {
            SpawnEnemy(miniWayParam.PathId, enemyInfor);
            yield return new WaitForSeconds(2);
        }
    }

    private void SpawnEnemy(int pathId, EnemyInfor enemyInfor)
    {
        if (enemyInfor.EnemyType != EnemyType.MeleeAttack) return;
        if (enemyInfor.EnemyId < 0 || enemyInfor.EnemyId >= Data.MeleeEnemys.Count) return;

        var enemyPrefab = Data.MeleeEnemys[enemyInfor.EnemyId].EnemyPrefab;
        if (enemyPrefab == null) return;

        var spawnPosition = LevelData.Levels[GameManager.Instance.Level].Paths[pathId].Positions[0];
        PoolingManager.Spawn(enemyPrefab, spawnPosition, default, transform);
    }
}