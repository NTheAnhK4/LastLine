
using System.Collections;

using UnityEngine;

public class OldSkillHandler : ComponentBehavior
{
    protected AnimHandler m_AnimHander;
    [SerializeField] protected float m_CoolDown = 1;
   
    [SerializeField] protected bool m_Loop = true;
    
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
            if (m_AnimHander != null)
            {
                m_AnimHander.SetAnim(AnimHandler.State.DoSkill);
                StartCoroutine(OnDoSkill());
                m_AnimHander.RevertToPreviousAnim();
            }
        } while (m_Loop);
    }

    

    protected virtual IEnumerator OnDoSkill()
    {
        yield return null;
    }
    
}
