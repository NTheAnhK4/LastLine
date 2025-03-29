using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : ComponentBehavior
{
    public GameObject buffPrefab;
    private List<IBuff> m_Buffs;

    private void Start()
    {
        m_Buffs = new List<IBuff>();
        AddBuff(new PhysicalDefenseBuff(transform.parent.parent.gameObject, buffPrefab));
    }

    public void AddBuff(IBuff buff)
    {
        if(buff == null) return;
        m_Buffs.Add(buff);
        buff.Apply();
    }
}
