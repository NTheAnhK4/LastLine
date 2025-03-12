using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : TowerUI
{
    [SerializeField] private UpdateTowerInfor m_UpgradeTower;
    [SerializeField] private UpdateTowerInfor m_SellTower;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        m_UpgradeTower = new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Upgrade").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Upgrade").GetComponentInChildren<TextMeshProUGUI>()
        };
       
        m_SellTower = new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Sell").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Sell").GetComponentInChildren<TextMeshProUGUI>()
        };
        
    }

    private void OnEnable()
    {
        // if(tower == null) return;
        // int towerUpgradeId = tower.Data.Towers[tower.TowerId].TowerUpgradeList[0].TowerId;
        // m_UpgradeTower.cost.text = tower.Data.Towers[towerUpgradeId].Cost.ToString();
        //
        // m_SellTower.cost.text = (tower.Data.Towers[tower.TowerId].Cost / 2).ToString();
    }

    private void Start()
    {
        m_UpgradeTower.btn.onClick.AddListener(() =>
        {
            int cost = int.Parse(m_UpgradeTower.cost.text);
            if (cost <= LevelManager.Instance.Gold)
            {
                LevelManager.Instance.Gold -= cost;
                UpdateTower(0, 1);
            }
            else
            {
                PoolingManager.Despawn(gameObject);
                Debug.Log("Not enough money");
            }
        });
        m_SellTower.btn.onClick.AddListener(() =>
        {
            
            int cost = int.Parse(m_SellTower.cost.text);
            LevelManager.Instance.Gold += cost;
            //UpdateTower(-1,1);
        });
    }

   
}
