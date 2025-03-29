using UnityEngine;

public abstract class IBuff
{
    protected readonly GameObject m_Target;
   
    protected GameObject m_BuffEffect;
    
    protected readonly BuffParam m_BuffParam;
    private float m_Timer;
    private float m_RemainingDuration; 
    public bool IsFinish => m_RemainingDuration <= 0;
    public bool IsStackable => m_BuffParam.IsStackable;
    public int MaxStacks => m_BuffParam.MaxStacks;
    public IBuff(BuffParam buffParam, GameObject target)
    {
        m_BuffParam = buffParam;
        m_Target = target;
        m_Timer = 0;
        m_RemainingDuration = buffParam.Duration;
    }

    public virtual void Apply()
    {
        if (m_BuffParam.BuffEffectPrefab != null) m_BuffEffect = PoolingManager.Spawn(m_BuffParam.BuffEffectPrefab, m_Target.transform.position, Quaternion.identity, m_Target.transform);
    }

    public virtual void Update()
    {
        if(IsFinish) return;
        m_RemainingDuration -= Time.deltaTime;
        if(m_BuffParam.TickInterval <= 0) return;
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_BuffParam.TickInterval)
        {
            m_Timer = 0;
            DoSkillInterval();
        }
    }

    protected virtual void DoSkillInterval()
    {
        
    }

    
    public virtual void Remove()
    {
        if (m_BuffEffect != null) PoolingManager.Despawn(m_BuffEffect);
    }
}