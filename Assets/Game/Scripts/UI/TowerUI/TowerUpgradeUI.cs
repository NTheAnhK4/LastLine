
using System;
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

   
    

    protected virtual void Start()
    {
        
        m_UpgradeTower.btn.onClick.AddListener(() =>
        {
            int cost = int.Parse(m_UpgradeTower.cost.text);
            if (cost <= InGameManager.Instance.Gold)
            {
                InGameManager.Instance.Gold -= cost;
                UpdateTower(0, 1.5f);
            }
            else
            {
                PoolingManager.Despawn(gameObject);
               
            }
        });
        m_SellTower.btn.onClick.AddListener(() =>
        {
            
            int cost = int.Parse(m_SellTower.cost.text);
            InGameManager.Instance.Gold += cost;
            UpdateTower(-1,1.5f);
        });
        
    }

   

    public override void CheckButtonsAvailable()
    {
        if (tower == null || tower.Data.Towers[tower.TowerId].TowerUpgradeList.Count == 0)
        {
            m_UpgradeTower.btn.interactable = false;
            m_UpgradeTower.cost.text = "--";
            m_UpgradeTower.cost.color = Color.white;
        }
        else
        {
            int towerUpgradeId = tower.Data.Towers[tower.TowerId].TowerUpgradeList[0].TowerId;
            m_UpgradeTower.cost.text = tower.Data.Towers[towerUpgradeId].Cost.ToString();
           
            int upgradeCost = int.Parse(m_UpgradeTower.cost.text);
            if (upgradeCost > InGameManager.Instance.Gold)
            {
                m_UpgradeTower.btn.interactable = false;
                m_UpgradeTower.cost.color = Color.red;
            }
            else
            {
                m_UpgradeTower.btn.interactable = true;
                m_UpgradeTower.cost.color = Color.white;
            }
            
            
        }

        if (tower == null || tower.TowerId >= tower.Data.Towers.Count)
        {
            m_SellTower.btn.interactable = false;
            m_SellTower.cost.text = "--";
        }
        else
        {
            int sellCost = tower.Data.Towers[tower.TowerId].Cost / 2;
            m_SellTower.cost.text = sellCost.ToString();
            m_SellTower.btn.interactable = true;
        }
    }
   
}
