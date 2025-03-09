using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUI : CenterUI
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
        quitBtn.onClick.AddListener(() =>
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
        quitBtn.onClick.RemoveAllListeners();
        rePlayBtn.onClick.RemoveAllListeners();
    }
}
