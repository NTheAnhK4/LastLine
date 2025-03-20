
using System;
using System.Collections;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
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
                ObserverManager.Notify(EventId.UpdateGold, gold);
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
                    ObserverManager.Notify(EventId.Lose);
                }
                else ObserverManager.Notify(EventId.AttackCastle, healthPoint);
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
        
        
        HealthPoint = 20;
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
        ObserverManager.Notify(EventId.Win);
        CompleteLevel(3);
    }

    public void HandleEnemyDead(Enemy enemy)
    {
        if (enemySpawner != null)
        {
            bool isFinishGame = enemySpawner.IsFinishGame(enemy);
            if (isFinishGame) StartCoroutine(HandleWin());
        }
    }
    
}
