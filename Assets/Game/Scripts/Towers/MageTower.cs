using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [SerializeField] private RangedAttack mageAttack; 
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (mageAttack == null) mageAttack = transform.GetComponentInChildren<RangedAttack>();
       
    }

    public override void Init(int towerId, Vector3 flagPosition)
    {
        base.Init(towerId, flagPosition);
        mageAttack.Init(Data.Towers[towerId].AttackRange, Data.Towers[towerId].AttackSpeed, Data.Towers[towerId].UnitPrefab);
    }
}
