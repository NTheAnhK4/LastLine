using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WinView : UICenterView
{
    [Header("Button")]
    [SerializeField] private Button rePlayBtn;
    [SerializeField] private Button continueBtn;
    [Header("Stars")] 
    [SerializeField] private List<Image> m_Stars;

    [SerializeField] private Sprite m_GoldStar;
    [SerializeField] private Sprite m_EmptyStar;
    public int StarCount = 0;
   

    protected override  void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Buttons");
        if (rePlayBtn == null) rePlayBtn = buttonHolder.Find("Replay").GetComponent<Button>();
        if (continueBtn == null) continueBtn = buttonHolder.Find("Continue").GetComponent<Button>();
        Transform stars = transform.Find("Stars");

        if (stars != null)
        {
            foreach (Transform star in stars)
            {
                m_Stars.Add(star.GetComponent<Image>());
            }
        }
    }

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(OnContinueBtnClick);
        rePlayBtn.onClick.AddListener(OnReplayBtnClick);
    }

    private async void OnContinueBtnClick()
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
        continueBtn.onClick.RemoveAllListeners();
        rePlayBtn.onClick.RemoveAllListeners();
    }

    

    public void SetStars()
    {
        for (int i = 0; i <= StarCount - 1; ++i)
        {
            if(i >= m_Stars.Count) return;
            m_Stars[i].sprite = m_GoldStar;
        }

        for (int i = StarCount; i < 3; ++i) m_Stars[i].sprite = m_EmptyStar;
    }

    protected override UniTask OnShow()
    {
        GameManager.Instance.GameSpeed = 0;
        SetStars();
        return base.OnShow();
    }

    
}
