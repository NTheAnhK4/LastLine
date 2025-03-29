
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public LevelData LevelData;
    
    public EnemySpawner enemySpawner;
    public TowerSpawner towerSpawner;
    public LevelSpawner levelSpawner;
    public LevelUI levelUI;
   
    private float healthPoint = 20;
    private int gold;
    
    private bool isGameOver;
    private int currentLevel;


    public List<Vector3> GetPath(int pathId)
    {
        if (LevelData == null || currentLevel >= LevelData.Levels.Count)
        {
            Debug.Log("param for level data is missing");
            return null;
        }
        return LevelData.Levels[currentLevel].Paths[pathId].Positions;
    }

    public int GetPathNum()
    {
        return LevelData.Levels[currentLevel].Paths.Count;
    }

    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public int Gold
    {
        get => gold;
        set
        {
            if (gold != value)
            {
                gold = value;
                ObserverManager<GameEventID>.Notify(GameEventID.UpdateGold, gold);
            }
        }
    }

    public float HealthPoint
    {
        get => healthPoint;
        set
        {
            if (Math.Abs(healthPoint - value) > 0.01)
            {
                healthPoint = value;
                if(isGameOver) return;
                if (HealthPoint <= 0)
                {
                    isGameOver = true;
                    ObserverManager<GameEventID>.Notify(GameEventID.Lose);
                }
                else ObserverManager<GameEventID>.Notify(GameEventID.AttackCastle, healthPoint);
            }
            
        }
    }

   
    
   
   


    private void Start()
    {
        currentLevel = GameManager.Instance.SelectedLevel;
        PlayLevel(this.currentLevel);
    }

    private void PlayLevel(int level)
    {
        
        GameManager.Instance.GameSpeed = 1;
        currentLevel = level;
        levelSpawner.Init(LevelData.Levels[level].LevelPrefab);
        isGameOver = false;


        HealthPoint = LevelData.Levels[currentLevel].TowerHealth;
        Gold = LevelData.Levels[level].InitialGold;
        
        enemySpawner.Init(LevelData.Levels[level]);
        towerSpawner.Init(LevelData.Levels[level]);
        towerSpawner.SpawnTower();
        levelUI.Init(LevelData.Levels[level],-1);
    }

    private void CompleteLevel(int stars)
    {
        LevelProgress progress = JsonSaveSystem.LoadData<LevelProgress>();
        progress.UpdateLevelState(currentLevel,stars);
        progress.UpdateLevelState(currentLevel + 1, 0);
       
        JsonSaveSystem.SaveData(progress);
    }

    private IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(2f);
        int star;
        if (healthPoint > 12) star = 3;
        else if (healthPoint > 6) star = 2;
        else star = 1;
      
        CompleteLevel(star);
        ObserverManager<GameEventID>.Notify(GameEventID.Win, star);
        
    }

    public void HandleEnemyDead(Enemy enemy)
    {
        if (enemySpawner != null)
        {
            bool isFinishGame = enemySpawner.IsFinishGame(enemy);
            if (isFinishGame) StartCoroutine(HandleWin());
        }
    }

    public void HandeEnemyCloneSpawn(Enemy enemy)
    {
        if (enemySpawner != null)
        {
            enemySpawner.activeEnemies.Add(enemy);
        }
    }
    
}
