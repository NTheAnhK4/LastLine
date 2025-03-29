
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : ComponentBehavior
{
    
    private List<IBuff> m_Buffs;
    private Dictionary<IBuff, int> activeBuffs = new Dictionary<IBuff, int>();

    public void Init()
    {
        m_Buffs = new List<IBuff>();
        activeBuffs = new Dictionary<IBuff, int>();
    }

    private void OnEnable()
    {
        Init();
    }

    public void AddBuff(IBuff buff)
    {
        
        if (buff.IsStackable)
        {
            if (!activeBuffs.ContainsKey(buff))
            {
                activeBuffs[buff] = 1;
                m_Buffs.Add(buff);
                buff.Apply();
            }
            else if (activeBuffs[buff] < buff.MaxStacks)
            {
                activeBuffs[buff]++;
                m_Buffs.Add(buff);
                buff.Apply();
            }
        }
        else
        {
            if (!activeBuffs.ContainsKey(buff))
            {
                activeBuffs[buff] = 1;
                m_Buffs.Add(buff);
                buff.Apply();
            }
        }
    }

    private void Update()
    {
        List<IBuff> buffToRemove = new List<IBuff>();
        foreach (IBuff buff in m_Buffs)
        {
            buff.Update();
            if(buff.IsFinish) buffToRemove.Add(buff);
        }

        foreach (IBuff buff in buffToRemove)
        {
            m_Buffs.Remove(buff);
            if (activeBuffs[buff] > 1) activeBuffs[buff]--;
            else activeBuffs.Remove(buff);
        }
    }
}
