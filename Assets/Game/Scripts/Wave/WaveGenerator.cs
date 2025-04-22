using System;
using System.Collections.Generic;
using System.Threading;

using Cysharp.Threading.Tasks;
using UnityEngine;

public class WaveGenerator
{
    
    private LevelParam _LevelParam;
    private WaveDataGenerator _WaveDataGenerator;
    private int _CurrentWave;
    private List<MiniWaveData> _CurrentWaveData;
    private int _TotalWay;
    private CancellationTokenSource _cts = new();
    public int CurrentWave => _CurrentWave;
    
    public WaveGenerator(int level, int totalWay)
    {
       
        
        LevelData levelData = DataManager.Instance.GetData<LevelData>();
        if (levelData == null)
        {
            Debug.LogWarning("Level data has not been loaded.");
            return;
        }

        _TotalWay = totalWay;
        _LevelParam = levelData.Levels[level];
        _WaveDataGenerator = new WaveDataGenerator(_LevelParam,totalWay);
        _CurrentWave = 0;
        _CurrentWaveData = _WaveDataGenerator.GenerateWave(_CurrentWave);
        NotifySignal(true);
        ObserverManager<GameEventID>.Attach(GameEventID.SpawnWay, _=> SpawnWave());
    }

    private void NotifySignal(bool isFirstSignal = false)
    {
        foreach (MiniWaveData miniWaveData in _CurrentWaveData)
        {
            if(isFirstSignal) ObserverManager<GameEventID>.Notify(GameEventID.DisplayFirstGameSignal, miniWaveData.RootID);
            else
            {
                ObserverManager<GameEventID>.Notify(GameEventID.DisplaySignal, miniWaveData.RootID);
            }
        }
    }
    public async void SpawnWave()
    {
        
        try
        {
            var token = _cts.Token;
            var tasks = new List<UniTask>();
           
            foreach (MiniWaveData miniWaveData in _CurrentWaveData)
            {
                tasks.Add(SpawnMiniWave(miniWaveData, token));
            }

            await UniTask.WhenAll(tasks);
            if (!token.IsCancellationRequested)
            {
                if (_CurrentWave + 1 >= _TotalWay) await InGameManager.Instance.HandleWin();
                else
                {
                    _CurrentWave++;
                    _CurrentWaveData = _WaveDataGenerator.GenerateWave(_CurrentWave);
                    NotifySignal();
                }

            }
        }catch (OperationCanceledException)
        {
            //Wave spawning was cancelled
            return;
        }
        
    }

    private async UniTask SpawnMiniWave(MiniWaveData miniWaveData, CancellationToken token)
    {
        try
        {
            if (miniWaveData == null)
            {
                Debug.LogError("Wave data is null");
                return;
            }

            
            foreach (EnemyGroup enemyGroup in miniWaveData.EnemyGroups)
            {
                await SpawnEnemyGroup(enemyGroup, miniWaveData.RootID, token);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(miniWaveData.TimeUntilNextWay), cancellationToken: token);
            
        } catch (OperationCanceledException)
        {
            //MiniWave was cancelled
            return;
        }
       
    }

    private async UniTask SpawnEnemyGroup(EnemyGroup enemyGroup, int rootMiniWay, CancellationToken token)
    {
        try
        {
           
            for (int i = 0; i < enemyGroup.EnemyCount; ++i) {
                GameObject enemyPrefab = GameFactory.GetEnemyPrefab(enemyGroup.EnemyType, enemyGroup.EnemyID);
                if (enemyPrefab == null)
                {
                    Debug.LogWarning("Enemy prefab is null");
                    continue;
                }

                Vector3 spawnPosition = _LevelParam.Roots[rootMiniWay].Point;
                Enemy enemy = PoolingManager.Spawn(enemyPrefab, spawnPosition).GetComponent<Enemy>();
                if(EnemyHolder.Instance != null) EnemyHolder.Instance.HoldEnemy(enemy.gameObject, enemyPrefab.name);
                if(enemy != null) enemy.Init(enemyGroup.EnemyID, _LevelParam.Roots[rootMiniWay]);
                await UniTask.Delay(TimeSpan.FromSeconds(enemyGroup.SpawnInterval), cancellationToken: token);
            }
           
        }catch (OperationCanceledException)
        {
            //Enemy group is cancelled
            return;
        }
        
    }
    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    
   
    
}