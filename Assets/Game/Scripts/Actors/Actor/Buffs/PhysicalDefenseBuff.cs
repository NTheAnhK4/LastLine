using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDefenseBuff : IBuff
{
    public PhysicalDefenseBuff(GameObject target, GameObject buffEffectPrefab) : base(target, buffEffectPrefab)
    {
    }

    public override void Apply()
    {
        base.Apply();
        HealthHandler healthHandler = m_Target.GetComponentInChildren<HealthHandler>();
        
    }
}
