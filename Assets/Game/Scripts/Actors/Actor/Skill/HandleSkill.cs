
using System.Collections;

using UnityEngine;

public class HandleSkill : ComponentBehavior
{
    private AnimHandler m_AnimHander;
    [SerializeField] protected float m_CoolDown = 1;
    [SerializeField] protected GameObject m_ObjectCreated;
    [SerializeField] protected bool m_Loop = true;
    [SerializeField] protected int m_SpawnCount;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_AnimHander == null) m_AnimHander = transform.parent.parent.GetComponentInChildren<AnimHandler>();
    }

    private void Start()
    {
        StartCoroutine(DoSkill());
    }

    protected virtual IEnumerator DoSkill()
    {
        do
        { 
            yield return new WaitForSeconds(m_CoolDown);
            for (int i = 0; i < m_SpawnCount; ++i)
            {
                if (m_AnimHander != null)
                {
                    m_AnimHander.SetAnim(AnimHandler.State.DoSkill);
                    yield return new WaitForSeconds(1.5f);
                    Vector3 position = GetPosition();
                    SpawnObject(position);
                    m_AnimHander.RevertToPreviousAnim();
                }
                
            }
        } while (m_Loop);
    }

    protected virtual Vector3 GetPosition()
    {
        return Vector3.zero;
    }

    protected virtual GameObject SpawnObject(Vector3 position)
    {
        if (m_ObjectCreated != null) return PoolingManager.Spawn(m_ObjectCreated, position);
        return null;
    }
    
}
