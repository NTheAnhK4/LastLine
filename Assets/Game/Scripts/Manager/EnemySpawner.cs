
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData Data;
    private LevelParam m_LevelParam;
    public int currentWay = -1;
    private System.Action<object> onSpawnWay;

    public readonly HashSet<Enemy> activeEnemies = new HashSet<Enemy>();

    public void Init(LevelParam levelParam)
    {
        m_LevelParam = levelParam;
       
    }
    private void OnEnable()
    {
        onSpawnWay = _ => SpawnWay();
        ObserverManager<GameEventID>.Attach(GameEventID.SpawnNextWay, onSpawnWay);
    }

    private void OnDisable()
    {
        ObserverManager<GameEventID>.Detach(GameEventID.SpawnNextWay, onSpawnWay);
        
    }


    private void SpawnWay()
    {
        if(this == null) return;
        var ways = m_LevelParam.Ways;
        currentWay++;
        if (currentWay == ways.Count) return;
        
        ObserverManager<GameEventID>.Notify(GameEventID.SpawnWay, currentWay + 1);

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

      
            
    }

    private IEnumerator SpawnMiniWay(MiniWayParam miniWayParam)
    {
        foreach (var enemyInfor in miniWayParam.EnemyInfors)
        {
            SpawnEnemy(miniWayParam.PathId, enemyInfor);
            yield return new WaitForSeconds(enemyInfor.SpawnDelay);
            if(this == null) yield break;
        }
    }


    private void SpawnEnemy(int pathId, EnemyInfor enemyInfor)
    {
        if (enemyInfor.EnemyType == EnemyType.MeleeAttack)
        {
            if (enemyInfor.EnemyId < 0 || enemyInfor.EnemyId >= Data.MeleeEnemies.Count) return;
            var enemyPrefab = Data.MeleeEnemies[enemyInfor.EnemyId].EnemyPrefab;
            if (enemyPrefab == null) return;
       
            var spawnPosition = m_LevelParam.Paths[pathId].Positions[0];
            MeleeEnemy meleeEnemy = PoolingManager.Spawn(enemyPrefab, spawnPosition, default, transform).GetComponent<MeleeEnemy>();

            if (meleeEnemy != null)
            {
                meleeEnemy.Init(Data.MeleeEnemies[enemyInfor.EnemyId],m_LevelParam.Paths[pathId].Positions);
                activeEnemies.Add(meleeEnemy);
            }
            
            
        }
        else
        {
            if(enemyInfor.EnemyId < 0 || enemyInfor.EnemyId >= Data.RangedEnemies.Count) return;
            var enemyPrefab = Data.RangedEnemies[enemyInfor.EnemyId].EnemyPrefab;
            if(enemyPrefab == null) return;

            var spawnPosition = m_LevelParam.Paths[pathId].Positions[0];
            RangedEnemy rangedEnemy = PoolingManager.Spawn(enemyPrefab, spawnPosition, default, transform).GetComponent<RangedEnemy>();
            if (rangedEnemy != null)
            {
                rangedEnemy.Init(Data.RangedEnemies[enemyInfor.EnemyId], m_LevelParam.Paths[pathId].Positions);
                activeEnemies.Add(rangedEnemy);
            }
            
        }
        
    }

    public bool IsFinishGame(Enemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            if (activeEnemies.Count == 0)
            {
                if (currentWay + 1 < m_LevelParam.Ways.Count)
                {
                    ObserverManager<GameEventID>.Notify(GameEventID.SpawnedEnemies, currentWay);
                    return false;
                }
                else return true;
            }
        }

        return false;
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    
}