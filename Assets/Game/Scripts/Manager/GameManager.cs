using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        if (preSpeed == 0) GameSpeed = 1;
        else GameSpeed = preSpeed;
    }

    public void ReplayLevel()
    {
        GameSpeed = 1;
        DOTween.KillAll();
        SelectLevel(InGameManager.Instance.CurrentLevel);
    }

    public void GoToWorldMap()
    {
       
        GameSpeed = 1;
       
        DOTween.KillAll();
        SceneManager.LoadScene("WorldMap");
        AudioManager.PlayBackGroundMusic(SoundType.SelectLevel);
    }

  
   
}
