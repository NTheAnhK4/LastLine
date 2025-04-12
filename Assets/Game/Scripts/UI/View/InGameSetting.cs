using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InGameSetting : UICenterView
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
        quitBtn.onClick.AddListener(OnQuitBtnClick);
        replayBtn.onClick.AddListener(OnReplayBtnClick);
    }

    private async void OnQuitBtnClick()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
        
        GameManager.Instance.GoToWorldMap();
    }

   

    private async void OnReplayBtnClick()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
        
        GameManager.Instance.ReplayLevel();
    }
    private void OnDisable()
    {
        quitBtn.onClick.RemoveAllListeners();
        replayBtn.onClick.RemoveAllListeners();
    }

   

    public async void OnHideUI()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
    }

}
