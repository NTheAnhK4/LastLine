using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapSetting : UICenterView
{
    [SerializeField] private Button quitBtn;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Button");
        if (quitBtn == null) quitBtn = buttonHolder.Find("Quit").GetComponent<Button>();
    }

   

    private async void OnEnable()
    {
        quitBtn.onClick.AddListener(QuitBtnClick);
        
        await ViewAnimationController.PlayShowAnimation(ViewAnimationType.PopZoom, this);
    }
   
    private async void QuitBtnClick()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
        PlayerPrefs.SetFloat("MusicVolumeData",AudioManager.Instance.MusicVolumeRate);
        PlayerPrefs.SetFloat("SFXVolumeData",AudioManager.Instance.SfxVolumeRate);
        PlayerPrefs.Save();
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }

    public async void HideUI()
    {
        await ViewAnimationController.PlayHideAnimation(ViewAnimationType.PopZoom);
    }
   
    private void OnDisable()
    {
        quitBtn.onClick.RemoveAllListeners();
    }
}
