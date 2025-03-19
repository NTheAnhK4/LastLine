
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class RangedAttackTower : Tower
{
    [SerializeField] private RangedAttack archerAttack;
    
    [SerializeField] private GameObject m_AttackRangeIndicatorPrefab;
    [SerializeField] private Transform m_AttackRangeIndicator;
    
    private float m_AttackRange;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (archerAttack == null) archerAttack = transform.GetComponentInChildren<RangedAttack>();
       
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

    public override void Init(int towerId, Vector3 flagPosition)
    {
        base.Init(towerId, flagPosition);
        m_AttackRange = Data.Towers[towerId].AttackRange;
        archerAttack.Init(Data.Towers[towerId].AttackRange, Data.Towers[towerId].AttackSpeed, Data.Towers[towerId].UnitPrefab);
    }

    
}
