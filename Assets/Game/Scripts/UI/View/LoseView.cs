using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : UICenterView
{
    [SerializeField] private Button rePlayBtn;
    [SerializeField] private Button quitBtn;
   

    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Buttons");
        if (rePlayBtn == null) rePlayBtn = buttonHolder.Find("RePlay").GetComponent<Button>();
        if (quitBtn == null) quitBtn = buttonHolder.Find("Quit").GetComponent<Button>();
    }

    private void OnEnable()
    {
        quitBtn.onClick.AddListener(OnQuitBtnClick);
     
        rePlayBtn.onClick.AddListener(OnReplayBtnClick);
    }

    
   

    private async void OnQuitBtnClick()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
        GameManager.Instance.GameSpeed = 1;
        GameManager.Instance.GoToWorldMap();
    }

    private async void OnReplayBtnClick()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
        GameManager.Instance.GameSpeed = 1;
        GameManager.Instance.ReplayLevel();
    }

    private void OnDisable()
    {
        quitBtn.onClick.RemoveAllListeners();
        rePlayBtn.onClick.RemoveAllListeners();
    }
}
