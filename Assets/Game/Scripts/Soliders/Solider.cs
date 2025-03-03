using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : ComponentBehavior
{
    [SerializeField] private SummonAttack m_Encampment;
    [SerializeField] private MeleeAttack m_Attack;
    [SerializeField] private SoliderMove m_Move;
    [SerializeField] private HealthHandler m_Health;
   
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_Attack == null) m_Attack = transform.GetComponentInChildren<MeleeAttack>();
        if (m_Move == null) m_Move = transform.GetComponentInChildren<SoliderMove>();
        if (m_Health == null) m_Health = transform.GetComponentInChildren<HealthHandler>();


    }

    public void Init(SummonAttack encampment, Vector3 flagPosition)
    {
        m_Encampment = encampment;
        m_Attack.Init(1,2,0.5f);
        
        m_Move.Init(1,flagPosition);
        m_Health.Init(20);
        m_Health.OnDead += () =>
        {
            m_Encampment.SoliderNum--;
        };
    }
    
}
