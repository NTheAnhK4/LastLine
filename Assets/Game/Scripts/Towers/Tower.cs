
using System.Collections;
using UnityEngine;


public class Tower : ComponentBehavior
{
    public TowerData Data;
    public int towerId;

    [SerializeField] protected AnimHandler animHandler;
    
    [SerializeField] private TowerUI towerUI;
    private bool isUpgrade;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (animHandler == null) animHandler = transform.GetComponentInChildren<AnimHandler>();
        isUpgrade = false;
    }

    public void ShowUI()
    {
       
        if(Data.Towers[towerId].TowerUIPrefab == null || isUpgrade) return;
        towerUI = PoolingManager.Spawn(Data.Towers[towerId].TowerUIPrefab,transform.position).GetComponent<TowerUI>();
        towerUI.tower = this;
    }

    public void HideUI()
    {
        if(towerUI == null) return;
        PoolingManager.Despawn(towerUI.gameObject);
    }

    
    public void UpdateTower(int upgradeId, float timerBuild = 1)
    {
        animHandler.SetInt("upgradeId",upgradeId);
        animHandler.SetAnim("Upgrade");
        int towerUpgradeId = Data.Towers[towerId].TowerUpgradeList[upgradeId].TowerId;
        StartCoroutine(BuildNewTower(towerUpgradeId, timerBuild));
    }

    protected void OnEnable()
    {
        isUpgrade = false;
        ApplyData();
    }

    protected virtual void ApplyData()
    {
        
    }
    IEnumerator BuildNewTower(int towerUpgradeId, float timeBuild = 1)
    {
        isUpgrade = true;
        yield return new WaitForSeconds(timeBuild);
        PoolingManager.Spawn(Data.Towers[towerUpgradeId].TowerPrefab,transform.position);
        
        PoolingManager.Despawn(this.gameObject);
        
    }
    
}
