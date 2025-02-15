
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private RangedAttack archerAttack;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (archerAttack == null) archerAttack = transform.GetComponentInChildren<RangedAttack>();
        ApplyData();
    }

    protected override void ApplyData()
    {
        base.ApplyData();
        archerAttack.Init(Data.Towers[towerId].AttackRange, Data.Towers[towerId].AttackSpeed, Data.Towers[towerId].UnitPrefab);
    }
}
