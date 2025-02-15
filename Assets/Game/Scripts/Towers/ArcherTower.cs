
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private RangedAttack archerAttack;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (archerAttack == null) archerAttack = transform.GetComponentInChildren<RangedAttack>();
    }

    public override void Init(TowerParam data)
    {
        base.Init(data);
        archerAttack.Init(data.AttackRange,data.AttackSpeed,data.UnitPrefab);
    }
}
