
using UnityEngine;

public class PhysicalDefenseBuff : IBuff
{
    private HealthHandler _healthHandler;
    public PhysicalDefenseBuff(BuffParam buffParam, GameObject target) : base(buffParam, target)
    {
        _healthHandler = m_Target.GetComponentInChildren<HealthHandler>();
    }

    public override void Apply()
    {
        base.Apply();
        if (_healthHandler != null)
        {
            if (m_BuffParam.IsMultiplier) _healthHandler.PhysicalDamageReduction *= m_BuffParam.Value;
            else _healthHandler.PhysicalDamageReduction += m_BuffParam.Value;
        }
    }

    public override void Remove()
    {
        base.Remove();
        if (_healthHandler != null)
        {
            if (m_BuffParam.IsMultiplier) _healthHandler.PhysicalDamageReduction /= m_BuffParam.Value;
            else _healthHandler.PhysicalDamageReduction -= m_BuffParam.Value;
        }
      
    }
}
