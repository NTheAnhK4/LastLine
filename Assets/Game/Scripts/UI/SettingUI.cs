using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : CenterUI
{
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button quitBtn;
   
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Button");
        if (replayBtn == null) replayBtn = buttonHolder.Find("Replay").GetComponent<Button>();
        if (quitBtn == null) quitBtn = buttonHolder.Find("Quit").GetComponent<Button>();
        
        
    }

    private void OnEnable()
    {
        quitBtn.onClick.AddListener(() =>
        {
            HideUI(() =>
            {
                GameManager.Instance.GameSpeed = 1;
                SceneManager.LoadScene("WorldMap");
            });
        });
    }

    private void OnDisable()
    {
        quitBtn.onClick.RemoveAllListeners();
    }
}
