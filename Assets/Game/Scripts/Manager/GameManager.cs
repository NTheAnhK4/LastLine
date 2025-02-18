
using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LevelData LevelData;
    
    public EnemySpawner enemySpawner;
    public TowerSpawner towerSpawner;
    public LevelUI levelUI;
    public int Level = 0;
    private float healthPoint = 20;
    private int gold;
    private float gameSpeed = 1f;

    public float GameSpeed
    {
        get => gameSpeed;
        set
        {
            if (Math.Abs(gameSpeed - value) > 0.01)
            {
                gameSpeed = value;
                Time.timeScale = value;
            }
            
        }
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
                if(HealthPoint <= 0) ObserverManager.Notify(EventId.Lose);
                else ObserverManager.Notify(EventId.AttackCastle, healthPoint);
            }
            
        }
    }

    public int WayNum => LevelData.Levels[Level].Ways.Count;
   
    private void Start()
    {
        HealthPoint = 20;
        Gold = LevelData.Levels[Level].InitialGold;
        towerSpawner.SpawnTower();
        levelUI.Init(-1);
    }

    
}
