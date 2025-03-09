using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : ComponentBehavior
{
    [SerializeField] private Image m_PanelImg;
    private Tween m_Tween;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_PanelImg == null) m_PanelImg = transform.GetComponent<Image>();
    }

    

    private void OnEnable()
    {
        ShowUI();
    }

    public void ShowUI()
    {
        m_PanelImg.DOFade(0.95f, 0.6f);
    }

    public void HideUI()
    {
        m_PanelImg.DOFade(0f, 0.3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }



}
