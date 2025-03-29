using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : ISkill
{
   

    protected override void DoSkill()
    {
        if(m_SkillParam is not BuffSkillParam buffSkillParam) return;
        SkillHandler skillHandler = m_Actor.GetComponentInChildren<SkillHandler>();
       
        if(skillHandler == null) return;
        List<GameObject> allies = skillHandler.Allies;
        foreach (GameObject allie in allies)
        {
            BuffHandler buffHandler = allie.GetComponentInChildren<BuffHandler>();
            if(buffHandler == null) continue;
            foreach (BuffSkillInfor buffSkillInfor in buffSkillParam.BuffSkillInfors)
            {
                IBuff buff = GameFactory.GetBuff(buffSkillInfor.BuffType, buffSkillInfor.BuffID, allie);
                buffHandler.AddBuff(buff);
                
            }
        }
    }

    public BuffSkill(SkillParam skillParam, GameObject actor) : base(skillParam, actor)
    {
    }
}
