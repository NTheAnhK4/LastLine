

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CircleCollider2D))]
public class SkillHandler : ComponentBehavior
{
    [SerializeField] private AnimHandler m_AnimHandler;
    private List<ISkill> m_Skills = new List<ISkill>();
    public List<GameObject> Allies;
    public List<GameObject> Enemies;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_AnimHandler == null) m_AnimHandler = transform.parent.parent.GetComponentInChildren<AnimHandler>();
    }

    public void Init(List<EnemySkillParam> enemySkills)
    {
        m_Skills = new List<ISkill>();
        Allies = new List<GameObject>();
        Enemies = new List<GameObject>();
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

    private bool IsEnemy(GameObject other)
    {
        if (transform.parent.parent.tag.Equals("Enemy")) return other.tag.Equals("Solider");
        if (transform.parent.parent.tag.Equals("Solider")) return other.tag.Equals("Enemy");
        return false;
    }

    private bool IsAlly(GameObject other)
    {
        
        if (transform.parent.parent.tag.Equals("Enemy")) return other.tag.Equals("Enemy");
        if (transform.parent.parent.tag.Equals("Solider")) return other.tag.Equals("Solider");
        return false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.transform.tag.Equals("Radar")) return;
        if(IsEnemy(other.gameObject)) Enemies.Add(other.gameObject);
        else if(IsAlly(other.gameObject)) Allies.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag.Equals("Radar")) return;
        if(IsEnemy(other.gameObject)) Enemies.Remove(other.gameObject);
        else if(IsAlly(other.gameObject)) Allies.Remove(other.gameObject);
    }
}
