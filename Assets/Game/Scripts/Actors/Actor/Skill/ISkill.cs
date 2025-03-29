
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ISkill
{
    protected readonly SkillParam m_SkillParam;
    protected readonly GameObject m_Actor;
    public bool IsFinish;
    private float m_Timer;
    public bool IsDoingSkill;
    protected ISkill(SkillParam skillParam, GameObject actor)
    {
      
        m_SkillParam = skillParam;
        m_Actor = actor;
        
        IsFinish = false;
        m_Timer = 0;
        IsDoingSkill = false;
    }

    public void Update()
    {
        if(IsFinish) return;
        m_Timer += Time.deltaTime;
    }

    public bool CanDoSkill => m_Timer >= m_SkillParam.CoolDown;

    public async UniTask DoSkillAsync(AnimHandler animHandler, float animTime)
    {
        if(IsDoingSkill || animHandler.currentState == AnimHandler.State.Dead || animHandler.currentState == AnimHandler.State.Attack) return;
        animHandler.SetAnim(AnimHandler.State.DoSkill);
        IsDoingSkill = true;
        if (!m_SkillParam.IsLoop) IsFinish = true;
        m_Timer = 0;
        await UniTask.Delay(TimeSpan.FromSeconds(animTime));
        IsDoingSkill = false;
        animHandler.RevertToPreviousAnim();
        DoSkill();
    }

    protected abstract void DoSkill();
   

}

public enum SkillType
{
    Summon,
    Buff,
    Debuff
}