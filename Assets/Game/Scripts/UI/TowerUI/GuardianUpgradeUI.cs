using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GuardianUpgradeUI : TowerUpgradeUI
{
    [SerializeField] private Button m_FlagButton;

    [SerializeField] private GameObject m_GuardianPlaceablePrefab;
    [SerializeField] private GameObject m_GuardianPlaceable;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_FlagButton == null) m_FlagButton = optionsHolder.Find("Flag").GetComponentInChildren<Button>();
    }

    protected override void Start()
    {
        base.Start();
        m_FlagButton.onClick.AddListener(OnFlagBtnClick);
    }

    private void OnFlagBtnClick()
    {
        if(m_GuardianPlaceable != null) PoolingManager.Despawn(m_GuardianPlaceable);
        if (m_GuardianPlaceablePrefab != null)
        {
            var transform1 = transform;
            m_GuardianPlaceable = PoolingManager.Spawn(m_GuardianPlaceablePrefab, transform1.position, Quaternion.identity, transform1);
        }
    }

    private void OnDisable()
    {
        if (m_GuardianPlaceable != null)
        {
            PoolingManager.Despawn(m_GuardianPlaceable);
           
            m_GuardianPlaceable = null;
        }
    }
}
