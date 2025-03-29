

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class SkillHandler : ComponentBehavior
{
    [SerializeField] private AnimHandler m_AnimHandler;
    private List<ISkill> m_Skills = new List<ISkill>();
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_AnimHandler == null) m_AnimHandler = transform.parent.parent.GetComponentInChildren<AnimHandler>();
    }

    public void Init(List<EnemySkillParam> enemySkills)
    {
        m_Skills = new List<ISkill>();
        foreach (EnemySkillParam enemySkill in enemySkills)
        {
            ISkill skill = GameFactory.GetSkill(enemySkill.SkillType, enemySkill.SkillId, transform.parent.parent.gameObject);
            m_Skills.Add(skill);
        }

       
    }
    private void Update()
    {
       
        foreach (ISkill skill in m_Skills)
        {
            skill.Update();
            if (skill.CanDoSkill) skill.DoSkillAsync(m_AnimHandler, 1.5f).Forget();
        }

        m_Skills.RemoveAll(skill => skill.IsFinish);
    }

    
    
}
