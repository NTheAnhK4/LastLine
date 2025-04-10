using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : ComponentBehavior
{
    [System.Serializable]
    protected class UpdateTowerInfor
    {
        public Button btn;
        public TextMeshProUGUI cost;
    }

    public Tower tower { get; set; }

    [SerializeField] protected Transform optionsHolder;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if(optionsHolder == null) optionsHolder = transform.Find("Canvas").Find("UIImg");
    }

    protected void UpdateTower(int towerUpgradeId)
    {
        PoolingManager.Despawn(gameObject);
        tower.UpdateTower(towerUpgradeId);
        
    }

    public virtual void CheckButtonsAvailable()
    {
        
    }
    
}
