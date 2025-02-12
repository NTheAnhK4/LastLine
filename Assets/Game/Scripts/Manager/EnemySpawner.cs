using System.Collections;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //changelate
    public GameObject enemyPrefab;
    public Vector3 spawnPos;

    public IEnumerator SpawnEnemyFromId(int wayId)
    {
        int numberOfWay = 5;
        for (int i = wayId; i < numberOfWay; ++i)
        {
            SpawnWay(i);
            yield return new WaitForSeconds(10);
        }
    }

    private void SpawnWay(int wayId)
    {
        int numberOfMiniWay = 1;
        for (int i = 0; i < numberOfMiniWay; ++i) StartCoroutine(SpawnMiniWay(i));
    }

    IEnumerator SpawnMiniWay(int miniWayId)
    {
        int miniWayNumber = 3;
        for (int i = 0; i < miniWayNumber; ++i)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2);
        }
    }

    private void SpawnEnemy()
    {
        PoolingManager.Spawn(enemyPrefab, spawnPos,default,transform);
    }
}
