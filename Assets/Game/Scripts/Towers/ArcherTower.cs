
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private RangedAttack archerAttack;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (archerAttack == null) archerAttack = transform.GetComponentInChildren<RangedAttack>();
        
        archerAttack.Init(transform,3);
    }
}
