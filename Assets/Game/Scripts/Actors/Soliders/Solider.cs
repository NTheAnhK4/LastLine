
using System;
using UnityEngine;

public class Solider : ComponentBehavior
{
    [SerializeField] private SummonAttack m_Encampment;
    [SerializeField] private MeleeAttack m_Attack;
    [SerializeField] private SoliderMove m_Move;
    [SerializeField] private HealthHandler m_Health;
    [SerializeField] private AnimHandler m_Anim;

    public SoliderMove Move => m_Move;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_Attack == null) m_Attack = transform.GetComponentInChildren<MeleeAttack>();
        if (m_Move == null) m_Move = transform.GetComponentInChildren<SoliderMove>();
        if (m_Health == null) m_Health = transform.GetComponentInChildren<HealthHandler>();
        if (m_Anim == null) m_Anim = transform.GetComponentInChildren<AnimHandler>();

    }

    public void Init(SummonAttack encampment, Vector3 flagPosition, int encampMentLevel = 1)
    {
        
        m_Encampment = encampment;
        m_Anim.SetAnim(AnimHandler.State.Idle);
        m_Attack.Init(0.5f + 0.05f * encampMentLevel,1.5f - 0.05f * encampMentLevel,18f + 3 * encampMentLevel, DamageType.Physical);
        
        m_Move.Init(1.2f + 0.05f * encampMentLevel,flagPosition);
        m_Health.Init(600 + encampMentLevel * 50,0.1f + 0.03f * encampMentLevel,0.05f + 0.02f * encampMentLevel);
        m_Health.OnDead += () =>
        {
            m_Encampment.OnSoliderDead(flagPosition);
        };
    }

    private void OnDisable()
    {
        m_Anim.ResetAnim();
    }
}
