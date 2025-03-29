
using UnityEngine;

public abstract class ISkill
{
    protected float m_CoolDown;
    protected bool m_IsLoop;
    protected GameObject m_Actor;
    public bool IsFinish;
    private float m_Timer;
    public ISkill(ISkillData data)
    {
        m_CoolDown = data.CoolDown;
        m_IsLoop = data.IsLoop;
        m_Actor = data.Actor;
        IsFinish = false;
        m_Timer = 0;
    }

    public void Update()
    {
        if(!m_IsLoop && IsFinish) return;
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_CoolDown)
        {
            IsFinish = true;
            m_Timer = 0;
            DoSkill();
        }
    }

    protected abstract void DoSkill();
}