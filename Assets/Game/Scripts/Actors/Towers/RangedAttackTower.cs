
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class RangedAttackTower : Tower
{
    [SerializeField] private RangedAttack rangedAttack;
    
    [SerializeField] private GameObject m_AttackRangeIndicatorPrefab;
    [SerializeField] private Transform m_AttackRangeIndicator;
    
    private float m_AttackRange;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (rangedAttack == null) rangedAttack = transform.GetComponentInChildren<RangedAttack>();
       
    }

    public override void ShowUI()
    {
        base.ShowUI();
        if(m_AttackRangeIndicatorPrefab == null) return;
        
        if(m_AttackRangeIndicator == null) m_AttackRangeIndicator = PoolingManager.Spawn(m_AttackRangeIndicatorPrefab, transform.position).transform;

        m_AttackRangeIndicator.transform.DOScale(2 * m_AttackRange, 0.3f);

    }

    public override void HideUI()
    {
        base.HideUI();
        if (m_AttackRangeIndicator != null)
        {
            m_AttackRangeIndicator.transform.DOScale(0, 0.2f).OnComplete(() =>
            {
                PoolingManager.Despawn(m_AttackRangeIndicator.gameObject);
                m_AttackRangeIndicator = null;
            });

        } 
    }

    public override void Init(int towerId, Vector3 flagPosition, int towerLevel = 1)
    {
        base.Init(towerId, flagPosition, towerLevel);
        m_AttackRange = m_TowerData.Towers[towerId].AttackRange;
        rangedAttack.Init(m_TowerData.Towers[towerId].AttackRange, m_TowerData.Towers[towerId].AttackSpeed, m_TowerData.Towers[towerId].UnitPrefab, towerLevel);
    }

    
}
