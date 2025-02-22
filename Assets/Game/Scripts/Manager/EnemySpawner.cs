
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public MeleeEnemyData Data;
    private LevelParam m_LevelParam;
    public int currentWay = -1;
    private System.Action<object> onSpawnWay;
    public void Init(LevelParam levelParam)
    {
        m_LevelParam = levelParam;
    }
    private void OnEnable()
    {
        onSpawnWay = _ => SpawnWay();
        ObserverManager.Attach(EventId.SpawnNextWay, onSpawnWay);
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.SpawnNextWay, onSpawnWay);
        
    }


    private void SpawnWay()
    {
        if(this == null) return;
        var ways = m_LevelParam.Ways;
        currentWay++;
        if (currentWay == ways.Count) return;
        
        ObserverManager.Notify(EventId.SpawnWay, currentWay + 1);

        StartCoroutine(SpawnAllMiniWays(ways[currentWay].MiniWays));
    }

    private IEnumerator SpawnAllMiniWays(List<MiniWayParam> miniWays)
    {
        List<Coroutine> runningCoroutines = new List<Coroutine>();

        //Run all MiniWays in parallel
        foreach (var miniWay in miniWays)
        {
            runningCoroutines.Add(StartCoroutine(SpawnMiniWay(miniWay)));
        }

        // Wait for all MiniWays to complete
        foreach (var coroutine in runningCoroutines)
        {
            yield return coroutine;
        }

        // When all MiniWays have spawned, send an event
        if (currentWay + 1 < m_LevelParam.Ways.Count)
            ObserverManager.Notify(EventId.SpawnedEnemies, currentWay);
    }

    private IEnumerator SpawnMiniWay(MiniWayParam miniWayParam)
    {
        foreach (var enemyInfor in miniWayParam.EnemyInfors)
        {
            SpawnEnemy(miniWayParam.PathId, enemyInfor);
            yield return new WaitForSeconds(2);
            if(this == null) yield break;
        }
    }


    private void SpawnEnemy(int pathId, EnemyInfor enemyInfor)
    {
        if (enemyInfor.EnemyType != EnemyType.MeleeAttack) return;
        if (enemyInfor.EnemyId < 0 || enemyInfor.EnemyId >= Data.MeleeEnemys.Count) return;
        var enemyPrefab = Data.MeleeEnemys[enemyInfor.EnemyId].EnemyPrefab;
        if (enemyPrefab == null) return;
       
        var spawnPosition = m_LevelParam.Paths[pathId].Positions[0];
        MeleeEnemy meleeEnemy = PoolingManager.Spawn(enemyPrefab, spawnPosition, default, transform).GetComponent<MeleeEnemy>();
        
        meleeEnemy.Init(Data.MeleeEnemys[enemyInfor.EnemyId],m_LevelParam.Paths[pathId].Positions);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}