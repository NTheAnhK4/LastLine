using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using UnityEditor.Search;
using UnityEngine;

public class DevelopScripts : MonoBehaviour
{
    public View PauseGame;
    public View FinishGame;

    private void Start()
    {
        StartCoroutine(WaitForWin());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))  Show(PauseGame,ViewAnimationType.PopZoom);
        if(Input.GetKeyDown(KeyCode.H)) Hide(ViewAnimationType.PopZoom);
        
    }
    

    IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("OK");
        Time.timeScale = 0;
        Show(FinishGame);
    }

    private async void Show(View target, ViewAnimationType viewAnimationType = ViewAnimationType.PopZoom)
    {
        await ViewAnimationController.PlayShowAnimation(viewAnimationType,target);
    }

    private async void Hide(ViewAnimationType viewAnimationType = ViewAnimationType.PopZoom)
    {
        await ViewAnimationController.PlayHideAnimation(viewAnimationType);
    }
}
