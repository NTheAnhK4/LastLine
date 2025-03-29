using System;

using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public int SelectedLevel;
    private float gameSpeed = 1f;
    private float preSpeed = -1;
    
    
    public float GameSpeed
    {
        get => gameSpeed;
        set
        {
            if (Math.Abs(gameSpeed - value) > 0.01)
            {
                preSpeed = gameSpeed;
                gameSpeed = value;
                Time.timeScale = value;
            }
            
        }
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void SelectLevel(int level)
    {
        SelectedLevel = level;
        SceneManager.LoadScene("InGame");
        AudioManager.PlayBackGroundMusic(SoundType.InGame);
    }

    public void SetPreSpeedGame()
    {
        if (Math.Abs(preSpeed - (-1)) < 0.01) GameSpeed = 1;
        else GameSpeed = preSpeed;
    }

    public void ReplayLevel()
    {
        SelectLevel(InGameManager.Instance.CurrentLevel);
    }

    public void GoToWorldMap()
    {
        GameSpeed = 1;
        SceneManager.LoadScene("WorldMap");
        AudioManager.PlayBackGroundMusic(SoundType.SelectLevel);
        
    }
    
}
