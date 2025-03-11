
using UnityEngine;

public class RangedAttackTower : Tower
{
    [SerializeField] private RangedAttack archerAttack; 
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (archerAttack == null) archerAttack = transform.GetComponentInChildren<RangedAttack>();
       
    }

    public override void Init(int towerId, Vector3 flagPosition)
    {
        base.Init(towerId, flagPosition);
        archerAttack.Init(Data.Towers[towerId].AttackRange, Data.Towers[towerId].AttackSpeed, Data.Towers[towerId].UnitPrefab);
    }

    
}
