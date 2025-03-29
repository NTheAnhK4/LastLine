using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data/Buff", menuName = "Buff Data")]
public class BuffData : ScriptableObject
{
    public List<BuffParam> Buffs;
}

[Serializable]
public class BuffParam
{
    public string Name;
    public bool IsMultiplier;
    public float Duration;
    public bool IsStackable;
    public int MaxStacks;
    public float TickInterval;
    public float Value;
    public GameObject BuffEffectPrefab;
    
    
}
