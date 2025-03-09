using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : CenterUI
{
    [SerializeField] private Button rePlayBtn;
    [SerializeField] private Button continueBtn;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Buttons");
        if (rePlayBtn == null) rePlayBtn = buttonHolder.Find("Replay").GetComponent<Button>();
        if (continueBtn == null) continueBtn = buttonHolder.Find("Continue").GetComponent<Button>();
    }

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(() =>
        {
            HideUI(() =>
            {
                GameManager.Instance.GameSpeed = 1;
                SceneManager.LoadScene("WorldMap");
                AudioManager.PlayBackGroundMusic(SoundType.SelectLevel);
            });
            
        });
        rePlayBtn.onClick.AddListener(() =>
        {
            HideUI(() =>
            {
                GameManager.Instance.GameSpeed = 1;
                GameManager.Instance.ReplayLevel();
            });
        });
    }

    private void OnDisable()
    {
        continueBtn.onClick.RemoveAllListeners();
        rePlayBtn.onClick.RemoveAllListeners();
    }
}
