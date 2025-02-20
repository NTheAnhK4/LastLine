
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
    private bool isGameOver;
    [SerializeField] private int m_EnemyCount;
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

    public int WayNum => LevelData.Levels[Level].Ways.Count;

    public int EnemyCount
    {
        get => m_EnemyCount;
        set
        {
            if (m_EnemyCount != value)
            {
                m_EnemyCount = value;
                if(m_EnemyCount == 0) ObserverManager.Notify(EventId.Win);
            }
        }
    }

    private void Start()
    {
        isGameOver = false;
        m_EnemyCount = 0;
        foreach (WayParam wayParam in LevelData.Levels[Level].Ways)
        {
            foreach (MiniWayParam miniWayParam in wayParam.MiniWays)
            {
                m_EnemyCount += miniWayParam.EnemyInfors.Count;
            }
        }
        HealthPoint = 20;
        Gold = LevelData.Levels[Level].InitialGold;
        towerSpawner.SpawnTower();
        levelUI.Init(-1);
       
    }
}
