
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class Tower : ComponentBehavior
{
    public TowerData m_TowerData;
    protected int m_TowerId;
    [SerializeField] protected int m_TowerLevel;
    public int TowerId
    {
        get => m_TowerId;
        set => m_TowerId = value;
    }
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

    public virtual void ShowUI()
    {
       
        if(m_TowerData.Towers[m_TowerId].TowerUIPrefab == null || isUpgrade) return;
        towerUI = PoolingManager.Spawn(m_TowerData.Towers[m_TowerId].TowerUIPrefab,transform.position).GetComponent<TowerUI>();
        towerUI.gameObject.SetActive(false);
        if (towerUI != null)
        {
            towerUI.tower = this;
            towerUI.CheckButtonsAvailable();
            towerUI.gameObject.SetActive(true);
        }
    }

    public virtual void HideUI()
    {
        if(towerUI != null) PoolingManager.Despawn(towerUI.gameObject);
    }

    
    public void UpdateTower(int upgradeId, float timerBuild = 1)
    {
        HideUI();
        animHandler.SetInt("upgradeId",upgradeId);
        animHandler.SetAnim(AnimHandler.State.Upgrade);
        int towerUpgradeId;
        if (upgradeId >= 0) towerUpgradeId = m_TowerData.Towers[m_TowerId].TowerUpgradeList[upgradeId].TowerId;
        else towerUpgradeId = 0;
        StartCoroutine(BuildNewTower(towerUpgradeId, timerBuild));
    }
    
    protected void OnEnable()
    {
        isUpgrade = false;
    }

    public virtual void Init(int towerId,Vector3 flagPosition, int towerLevel = 0)
    {
        m_TowerId = towerId;
        m_FlagPosition = flagPosition;
        m_TowerLevel = towerLevel;
        m_TowerData = DataManager.Instance.GetData<TowerData>();
    }
    
    IEnumerator BuildNewTower(int towerUpgradeId, float timeBuild = 1)
    {
        isUpgrade = true;
        yield return new WaitForSeconds(timeBuild);
        Tower tower = PoolingManager.Spawn(m_TowerData.Towers[towerUpgradeId].TowerPrefab,transform.position)
            .GetComponent<Tower>();
        tower.Init(towerUpgradeId,m_FlagPosition, m_TowerLevel + 1);
        
        
        PoolingManager.Despawn(this.gameObject);
        
    }
    
}
