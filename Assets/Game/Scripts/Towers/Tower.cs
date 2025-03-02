
using System.Collections;
using UnityEngine;


public class Tower : ComponentBehavior
{
    public TowerData Data;
    protected int m_TowerId;

    [SerializeField] protected AnimHandler animHandler;
    [SerializeField] private TowerUI towerUI;

    protected Vector3 m_FlagPosition;
    private bool isUpgrade;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (animHandler == null) animHandler = transform.GetComponentInChildren<AnimHandler>();
        isUpgrade = false;
    }

    public void ShowUI()
    {
       
        if(Data.Towers[m_TowerId].TowerUIPrefab == null || isUpgrade) return;
        towerUI = PoolingManager.Spawn(Data.Towers[m_TowerId].TowerUIPrefab,transform.position).GetComponent<TowerUI>();
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
        animHandler.SetAnim(AnimHandler.State.Upgrade);
        int towerUpgradeId = Data.Towers[m_TowerId].TowerUpgradeList[upgradeId].TowerId;
        StartCoroutine(BuildNewTower(towerUpgradeId, timerBuild));
    }
    
    protected void OnEnable()
    {
        isUpgrade = false;
    }

    public virtual void Init(int towerId,Vector3 flagPosition)
    {
        m_TowerId = towerId;
        m_FlagPosition = flagPosition;
    }
    
    IEnumerator BuildNewTower(int towerUpgradeId, float timeBuild = 1)
    {
        isUpgrade = true;
        yield return new WaitForSeconds(timeBuild);
        Tower tower = PoolingManager.Spawn(Data.Towers[towerUpgradeId].TowerPrefab,transform.position)
            .GetComponent<Tower>();
        tower.Init(towerUpgradeId,m_FlagPosition);
        
        
        PoolingManager.Despawn(this.gameObject);
        
    }
    
}
