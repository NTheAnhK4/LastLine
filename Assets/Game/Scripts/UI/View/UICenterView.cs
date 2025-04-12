using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using UnityEngine;

public class UICenterView : View
{
    [SerializeField] private PanelUI _PanelUI;
    protected virtual void LoadComponent()
    {
        if (_PanelUI == null) _PanelUI = transform.parent.GetComponentInChildren<PanelUI>();
        if (Container == null) Container = transform;
    }

    protected override void Awake()
    {
        base.Awake();
        LoadComponent();
    }

    private void Reset()
    {
        LoadComponent();
    }

    public override void Show()
    {
        if(!_PanelUI.gameObject.activeInHierarchy) _PanelUI.gameObject.SetActive(true);
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        _PanelUI.HideUI();
    }
}
