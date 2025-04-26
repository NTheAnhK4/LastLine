using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniWaveDataGenerator
{
    private List<EnemyUnit> _EnemyUnits;

    public MiniWaveDataGenerator(List<EnemyUnit> enemyUnits)
    {
        _EnemyUnits = enemyUnits;
        
    }
    private List<EnemyUnit> GetEnemyAvailable(int waveID, float difficultyRatio, bool isFinalMiniWave)
    {
        _EnemyUnits.RemoveAll(enemyUnit => enemyUnit.EnemyCount == 0);
        if (isFinalMiniWave) return _EnemyUnits;
        
        return _EnemyUnits.Where(enemyUnit =>
            enemyUnit.MinWayAllowed <= waveID &&
            enemyUnit.MaxWaveAllowed >= waveID &&
            enemyUnit.DifficultyWeight <= difficultyRatio
        ).ToList();
    }
    private MiniWaveData GenerateEnemyGroups(List<EnemyUnit> enemyAvailable, float difficultyRatio, bool isFinalMiniWay)
    {
       
        MiniWaveData miniWaveData = new MiniWaveData();
        
        float currentDifficulty = 0;
        if (isFinalMiniWay)
        {
            foreach (EnemyUnit chosen in enemyAvailable)
            {
                miniWaveData.EnemyGroups.Add(new EnemyGroup()
                {
                    EnemyID = chosen.EnemyID,
                    EnemyType = chosen.EnemyType,
                    EnemyCount = chosen.EnemyCount,
                    SpawnInterval = chosen.DifficultyWeight
                });
              
            }

            return miniWaveData;
        }
        while (currentDifficulty <= difficultyRatio && enemyAvailable.Count > 0)
        {
            int enemyUnitId = Random.Range(0, enemyAvailable.Count);
            EnemyUnit chosen = enemyAvailable[enemyUnitId];

            float difficultyGap = difficultyRatio - currentDifficulty;
            int enemyCount = (int)Math.Floor(difficultyGap / chosen.DifficultyWeight);
            enemyCount = Math.Min(enemyCount, chosen.EnemyCount);
            if (enemyCount == 0) break;

            miniWaveData.EnemyGroups.Add(new EnemyGroup()
            {
                EnemyID = chosen.EnemyID,
                EnemyType = chosen.EnemyType,
                EnemyCount = enemyCount,
                SpawnInterval = chosen.DifficultyWeight // temp
            });
          

            currentDifficulty += enemyCount * chosen.DifficultyWeight;

            chosen.EnemyCount -= enemyCount;
            enemyAvailable.RemoveAll(enemy => enemy.EnemyCount == 0);
        }

        return miniWaveData;
    }
    private void UpdateSpawnIntervals(int waveID,MiniWaveData miniWaveData,  float estimatedTime)
    {
        List<float> difficultyRatios = new List<float>();
        float totalDifficulty = 0;
        for (int i = 0; i < miniWaveData.EnemyGroups.Count; ++i)
        {
            difficultyRatios.Add(miniWaveData.EnemyGroups[i].SpawnInterval);
            totalDifficulty += miniWaveData.EnemyGroups[i].SpawnInterval;
        }

        for (int i = 0; i < difficultyRatios.Count; ++i)
        {
            float totalEnemyGroupTime = estimatedTime * difficultyRatios[i] / totalDifficulty;
            float spawnInterval = totalEnemyGroupTime / miniWaveData.EnemyGroups[i].EnemyCount;
            miniWaveData.EnemyGroups[i].SpawnInterval = CaculateSpawnInterval(waveID, spawnInterval);
        }
       
    }

    private float CaculateSpawnInterval(int waveID, float originalSpawnInterval)
    {
        return Mathf.Clamp(originalSpawnInterval - 0.05f * waveID, GetMinSpawnInterval(waveID), GetMaxSpawnInterval(waveID));
    }
    private float GetMinSpawnInterval(int waveID)
    {
        return Mathf.Max(0.2f, 0.8f - 0.05f * waveID);
    }

    private float GetMaxSpawnInterval(int waveID)
    {
        return Mathf.Max(1, 2 - 0.1f * waveID);
    }
   
    public MiniWaveData GenerateMiniWave(int waveID ,float difficultyRatio, float estimatedWaveTime, bool isFinalMiniWave)
    {
        List<EnemyUnit> enemyAvailable = GetEnemyAvailable(waveID, difficultyRatio, isFinalMiniWave);
        
        MiniWaveData miniWaveData = GenerateEnemyGroups(enemyAvailable, difficultyRatio, isFinalMiniWave);
        
        
        UpdateSpawnIntervals(waveID,miniWaveData,  estimatedWaveTime);

        miniWaveData.TimeUntilNextWay = miniWaveData.EnemyGroups.Sum(g => g.SpawnInterval) + 3f;

        return miniWaveData;
    }
    
}
