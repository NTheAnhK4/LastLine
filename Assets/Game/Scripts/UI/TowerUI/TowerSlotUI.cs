
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

    protected  void Start()
    {
        archer.btn.onClick.AddListener(() =>
        {
            
            int archerCost = int.Parse(archer.cost.text);
            if (archerCost <= LevelManager.Instance.Gold)
            {
                LevelManager.Instance.Gold -= archerCost;
                UpdateTower(0,1);
            }
            else PoolingManager.Despawn(gameObject);
           
        });
        guardian.btn.onClick.AddListener(() =>
        {
            
            int guardianCost = int.Parse(guardian.cost.text);
            if (guardianCost <= LevelManager.Instance.Gold)
            {
                LevelManager.Instance.Gold -= guardianCost;
                UpdateTower(1,1);
            }
            else PoolingManager.Despawn(gameObject);
        });
        mage.btn.onClick.AddListener(() =>
        {
            
            int mageCost = int.Parse(mage.cost.text);
            if (mageCost <= LevelManager.Instance.Gold)
            {
                LevelManager.Instance.Gold -= mageCost;
                UpdateTower(2,1);
            }
            else PoolingManager.Despawn(gameObject);
        });
        catapult.btn.onClick.AddListener(() =>
        {
            
            int catapultCost = int.Parse(catapult.cost.text);
            if (catapultCost <= LevelManager.Instance.Gold)
            {
                LevelManager.Instance.Gold -= catapultCost;
                UpdateTower(3,1);
            }
            else PoolingManager.Despawn(gameObject);
        });
    }

    
}
