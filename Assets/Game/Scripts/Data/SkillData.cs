using System;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Skill", fileName = "Skill Data")]
public class SkillData : ScriptableObject
{
    public List<SummonSkillParam> SummonSkills;
}

[Serializable]
public class SkillParam
{
    public float CoolDown;
    public bool IsLoop;
}
[Serializable]
public class SummonSkillParam : SkillParam
{
    public EnemyType EnemyType;
    public int EnemyId;
}


