using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTower : Tower
{
    [SerializeField] private SummonAttack guardianAttack;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (guardianAttack == null) guardianAttack = transform.GetComponentInChildren<SummonAttack>();
        
    }

    public override void Init(int towerId, Vector3 flagPosition)
    {
        base.Init(towerId, flagPosition);
        guardianAttack.Init(m_FlagPosition,Data.Towers[m_TowerId].AttackSpeed);
    }

   
}
