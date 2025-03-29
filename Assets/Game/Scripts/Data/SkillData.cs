using System;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Skill", fileName = "Skill Data")]
public class SkillData : ScriptableObject
{
    public List<SummonSkillParam> SummonSkills;
    public List<BuffSkillParam> BuffSkills;
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

[Serializable]
public class BuffSkillParam : SkillParam
{
    public List<BuffSkillInfor> BuffSkillInfors;
}
[Serializable]
public class BuffSkillInfor
{
    public int BuffID;
    public BuffType BuffType;
}

public enum BuffType
{
    PhysicalDefense
}


