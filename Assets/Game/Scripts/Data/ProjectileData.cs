using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProjectileData", menuName = "Data/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public List<ProjectileParam> Projectiles;
}

[Serializable]
public class ProjectileParam
{
    public string Name;
    public float speed;
    public float damage;
    public DamageType DamageType;
}

public enum DamageType
{
    Physical, 
    Magical 
}


