
using System;
using System.Collections;

using UnityEngine;
using Random = UnityEngine.Random;


public class InGameManager : Singleton<InGameManager>
{

    #region Variables

    private WaveGenerator _WaveGenerator;
    public TowerSpawner towerSpawner;
    public LevelSpawner levelSpawner;
    public UIManager uiManager;
   
    private float healthPoint = 20;
    private int gold;
    
    private bool isGameOver;
    private int currentLevel;
    private int _TotalWay;

    private Action<object> onSpawnWay;
    #endregion

    


    #region Properties

    public int TotalWay => _TotalWay;
    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public int GetCurrentWay()
    {
        if (_WaveGenerator == null) return 0;
        else return _WaveGenerator.CurrentWave;
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

    #endregion




    #region LevelManager
    
    private void Start()
    {
        currentLevel = GameManager.Instance.SelectedLevel;
        PlayLevel(this.currentLevel);
    }

  
    private void PlayLevel(int level)
    {
        
        GameManager.Instance.GameSpeed = 1;
        currentLevel = level;
        if (InputManager.Instance != null) InputManager.Instance.SetLimitScene(currentLevel);
        LevelData levelData = DataManager.Instance.GetData<LevelData>();
        if (levelData != null)
        {
            _TotalWay = Random.Range(levelData.Levels[level].MinTotalWay, levelData.Levels[level].MaxTotalWay);
            levelSpawner.Init(levelData.Levels[level].LevelPrefab);
            isGameOver = false;


            HealthPoint = DataManager.Instance.GetData<LevelData>().Levels[currentLevel].TowerHealth;
            Gold = levelData.Levels[level].InitialGold;
        
           
            
            towerSpawner.Init(levelData.Levels[level]);
            towerSpawner.SpawnTower();
            uiManager.Init(levelData.Levels[level]);
            _WaveGenerator = new WaveGenerator(level, _TotalWay);
          
        }
        
    }
    private void CompleteLevel(int stars)
    {
        LevelProgress progress = JsonSaveSystem.LoadData<LevelProgress>();
        progress.UpdateLevelState(currentLevel,stars);
        progress.UpdateLevelState(currentLevel + 1, 0);
       
        JsonSaveSystem.SaveData(progress);
    }
    #endregion


    #region Game Result Handling

    public IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(2f);
        if (EnemyHolder.Instance == null) yield return null;
        yield return new WaitUntil(() => EnemyHolder.Instance.IsEnemyEmpty());
        int star;
        if (healthPoint > 12) star = 3;
        else if (healthPoint > 6) star = 2;
        else star = 1;
      
        CompleteLevel(star);
        ObserverManager<GameEventID>.Notify(GameEventID.Win, star);
        
    }

    
    #endregion

    private void OnDisable()
    {
        ObserverManager<GameEventID>.DetachAll();
        _WaveGenerator.Dispose();
        _WaveGenerator = null;
    }
}
