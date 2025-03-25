

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSlotUI : TowerUI
{
    [SerializeField] private UpdateTowerInfor archer;
    [SerializeField] private UpdateTowerInfor guardian;
    [SerializeField] private UpdateTowerInfor mage;
    [SerializeField] private UpdateTowerInfor catapult;
    private Transform uIImage;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        archer = new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Archer").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Archer").GetComponentInChildren<TextMeshProUGUI>()
        };
        guardian = new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Guardian").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Guardian").GetComponentInChildren<TextMeshProUGUI>()
        };
        mage = new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Mage").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Mage").GetComponentInChildren<TextMeshProUGUI>()
        };
        catapult = new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Catapult").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Catapult").GetComponentInChildren<TextMeshProUGUI>()
        };
        
    }

    void Start()
    {
        AddBuyListener(archer, 0);
        AddBuyListener(guardian, 1);
        AddBuyListener(mage, 2);
        AddBuyListener(catapult, 3);
       
    }

  

    void AddBuyListener(UpdateTowerInfor towerInfor, int towerType)
    {
        towerInfor.btn.onClick.AddListener(() =>
        {
            int cost = int.Parse(towerInfor.cost.text);
            if (cost <= LevelManager.Instance.Gold)
            {
                LevelManager.Instance.Gold -= cost;
                UpdateTower(towerType, 1);
            }
            else
            {
                PoolingManager.Despawn(gameObject);
                
            }
        });
    }

    public override void CheckButtonsAvailable()
    {
        CheckButtonAvailable(archer);
        CheckButtonAvailable(guardian);
        CheckButtonAvailable(mage);
        CheckButtonAvailable(catapult);
    }

    private void CheckButtonAvailable(UpdateTowerInfor towerInfor)
    {
        int towerCost = int.Parse(towerInfor.cost.text);
        if (towerCost > LevelManager.Instance.Gold)
        {
            towerInfor.btn.interactable = false;
            towerInfor.cost.color = Color.red;
        }
        else
        {
            towerInfor.btn.interactable = true;
            towerInfor.cost.color = Color.white;
        }
    }
    


    
}
