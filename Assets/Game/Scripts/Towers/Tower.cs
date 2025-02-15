
using System.Collections;
using UnityEngine;


public class Tower : ComponentBehavior
{
    public TowerData Data;
    public int towerId;

    [SerializeField] protected AnimHandler animHandler;
    
    [SerializeField] private TowerUI towerUI;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (animHandler == null) animHandler = transform.GetComponentInChildren<AnimHandler>();
    }

    public void ShowUI()
    {
        if(Data.TowerList[towerId].TowerUIPrefab == null) return;
        towerUI = PoolingManager.Spawn(Data.TowerList[towerId].TowerUIPrefab,transform.position).GetComponent<TowerUI>();
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
        int towerUpgradeId = Data.TowerList[towerId].TowerUpgradeList[upgradeId].TowerId;
        StartCoroutine(BuildNewTower(towerUpgradeId, timerBuild));
    }

    public virtual void Init(TowerParam data)
    {
        
    }
    IEnumerator BuildNewTower(int towerUpgradeId, float timeBuild = 1)
    {
        yield return new WaitForSeconds(timeBuild);
        Tower tower = PoolingManager.Spawn(Data.TowerList[towerUpgradeId].TowerPrefab,transform.position).GetComponent<Tower>();
        tower.Init(Data.TowerList[towerUpgradeId]);
        PoolingManager.Despawn(this.gameObject);
        
    }
    
}
