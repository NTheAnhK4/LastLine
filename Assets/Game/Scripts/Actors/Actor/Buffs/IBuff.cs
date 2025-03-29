using UnityEngine;

public abstract class IBuff
{
    protected readonly GameObject m_Target;
    protected readonly GameObject m_BuffEffectPrefab;
    protected GameObject m_BuffEffect;
    protected float m_TimeApply;
    public bool IsFinish => m_TimeApply <= 0;

    protected IBuff(GameObject target, GameObject buffEffectPrefab)
    {
        m_Target = target;
        m_BuffEffectPrefab = buffEffectPrefab;
        m_TimeApply = 10;
    }

    public virtual void Apply()
    {
        if (m_BuffEffectPrefab != null) m_BuffEffect = PoolingManager.Spawn(m_BuffEffectPrefab, m_Target.transform.position, Quaternion.identity, m_Target.transform);
    }

    public virtual void Update()
    {
        m_TimeApply -= Time.deltaTime;
    }
    
    public virtual void Remove()
    {
        if (m_BuffEffect != null) PoolingManager.Despawn(m_BuffEffect);
    }
}