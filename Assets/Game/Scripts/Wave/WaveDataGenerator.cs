
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGroup
{
    public int EnemyID;
    public EnemyType EnemyType;
    public int EnemyCount;
    public float SpawnInterval;
}

public class MiniWaveData
{
    public int RootID;
    public List<EnemyGroup> EnemyGroups;
    public float TimeUntilNextWay;
    public MiniWaveData()
    {
        EnemyGroups = new List<EnemyGroup>();
    }
}

public class EnemyUnit
{
    public int EnemyID;
    public EnemyType EnemyType;
    public int EnemyCount;
    public int MinWayAllowed;
    public int MaxWaveAllowed;
    public float DifficultyWeight;
}
public class WaveDataGenerator
{
    private LevelParam _levelParam;

   

    private float _TotalDifficulty;
    private List<float> _DifficultyRatios;
   
    private List<EnemyUnit> _EnemyUnits;
    private int _TotalWay;
    private MiniWaveDataGenerator _MiniWaveDataGenerator;
   
    #region Initialized

    public WaveDataGenerator(LevelParam levelParam, int totalWay)
    {

        _levelParam = levelParam;
        
       
        if (_levelParam == null)
        {
            Debug.LogWarning("Level Param is null");
            return;
        }

        _TotalWay = totalWay;
        InitEnemyUnit();
        InitDifficultyRatios();
        
    }

    private void InitEnemyUnit()
    {
        _EnemyUnits = new List<EnemyUnit>();
        _TotalDifficulty = 0;
        foreach (EnemyLevelConfig eConfig in _levelParam.AllowedEnemyConfigs)
        {
            float difficultyWeight = GameFactory.GetEnemyDifficultyWeight(eConfig.EnemyType, eConfig.EnemyID);
            int enemyCount = Random.Range(eConfig.Min, eConfig.Max + 1);
           
            _EnemyUnits.Add(new EnemyUnit()
            {
                EnemyID = eConfig.EnemyID,
                EnemyType = eConfig.EnemyType,
                EnemyCount = enemyCount,
                DifficultyWeight = difficultyWeight,
                MinWayAllowed = eConfig.MinWayAllowed,
                MaxWaveAllowed = eConfig.MaxWaveAllowed
            });
           
            _TotalDifficulty += difficultyWeight * enemyCount;
        }
        
        _MiniWaveDataGenerator = new MiniWaveDataGenerator(_EnemyUnits);
    }

    private void InitDifficultyRatios()
    {
        
        _DifficultyRatios = new List<float>();
        float totalDifficulty = 1.0f * _TotalWay * (_TotalWay + 1) / 2;
        for (int i = 0; i < _TotalWay; ++i)
        {
            _DifficultyRatios.Add((i + 1)/totalDifficulty);
        }
        
        
    }

  

    #endregion

   #region Support Generate Wave

   
    private bool TryGetDifficultyRatio(int waveIndex, out float difficultyRatio)
    {
        if (waveIndex >= _DifficultyRatios.Count)
        {
            Debug.LogWarning("Difficulty ratios is not calculated");
            difficultyRatio = 0;
            return false;
        }

        difficultyRatio = _DifficultyRatios[waveIndex] * _TotalDifficulty;
       
        return true;
    }

    private int GetNumberOfPaths(int waveID, float factor)
    {
        int maxPaths = _levelParam.Roots.Count;
        return Mathf.Clamp((int)(Mathf.Log(waveID + 1) * factor), 1, maxPaths);
    }

    private List<int> GetRandomPaths(int waveID, float factor)
    {
        int maxPaths = _levelParam.Roots.Count;
        int numberOfPaths = GetNumberOfPaths(waveID, factor);

        List<int> allPaths = Enumerable.Range(0, maxPaths).ToList();
        List<int> selectedPaths = new List<int>();
        
        for (int i = 0; i < numberOfPaths; ++i)
        {
            int index = Random.Range(0, allPaths.Count);
            selectedPaths.Add(allPaths[index]);
            allPaths.RemoveAt(index);
        }
        
        return selectedPaths;
    }
    
    private List<float> DistributeDifficulty(float totalDifficulty, int pathCount)
    {
        
        List<float> weights = new List<float>();
        int totalWeight = 0;

        for (int i = 0; i < pathCount; i++)
        {
            int weight = Random.Range(1, 6);
            weights.Add(weight);
            totalWeight += weight;
        }
        List<float> difficulties = weights
            .Select(w => totalDifficulty * w / totalWeight)
            .ToList();
        
        float diffSum = difficulties.Sum();
        if (diffSum < totalDifficulty)
            difficulties[Random.Range(0, pathCount)] += (totalDifficulty - diffSum);
       
        return difficulties;
    }

   
    #endregion

    private float GetEstimatedWaveTime(int miniWave, float increasePerWave = 6f, float timeBase = 10f)
    {
        return increasePerWave * miniWave + timeBase;
    }
    public List<MiniWaveData> GenerateWave(int waveID)
    {
        if (!TryGetDifficultyRatio(waveID, out float difficultyRatio)) return null;
        List<MiniWaveData> miniWaveDatas = new List<MiniWaveData>();
        List<int> roots = GetRandomPaths(waveID, 2.5f);
        List<float> distributeDifficulty = DistributeDifficulty(difficultyRatio, roots.Count);
      
        for (int i = 0; i < roots.Count; ++i)
        {
          
            MiniWaveData miniWaveData = _MiniWaveDataGenerator.GenerateMiniWave(waveID, distributeDifficulty[i], GetEstimatedWaveTime(waveID),waveID  + 1 == _TotalWay && i + 1 == roots.Count);
            miniWaveData.RootID = roots[i];
            
            miniWaveDatas.Add(miniWaveData);
        }

       
        return miniWaveDatas;
    }
   
    



        
        
}
