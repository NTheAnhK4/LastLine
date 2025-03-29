using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOldSkill : OldSkillHandler
{
    [SerializeField] protected GameObject m_ObjectCreated;
    [SerializeField] protected int m_SpawnCount;
    protected virtual Vector3 GetPosition()
    {
        return Vector3.zero;
    }

    protected virtual GameObject SpawnObject(Vector3 position)
    {
        if (m_ObjectCreated != null) return PoolingManager.Spawn(m_ObjectCreated, position);
        return null;
    }
    protected override IEnumerator OnDoSkill()
    {
        
        yield return new WaitForSeconds(1.5f);
        Vector3 position = GetPosition();
        SpawnObject(position);
        m_AnimHander.RevertToPreviousAnim();
    }

    protected override IEnumerator DoSkill()
    {
        do
        { 
            yield return new WaitForSeconds(m_CoolDown);
            for (int i = 0; i < m_SpawnCount; ++i)
            {
                if (m_AnimHander != null)
                {
                    m_AnimHander.SetAnim(AnimHandler.State.DoSkill);
                    StartCoroutine(OnDoSkill());
                }
                
            }
        } while (m_Loop);
    }
}
