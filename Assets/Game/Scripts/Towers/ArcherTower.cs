
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private RangedAttack archerAttack;
    [SerializeField] private GameObject projectilePrefab;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (archerAttack == null) archerAttack = transform.GetComponentInChildren<RangedAttack>();
        
        archerAttack.Init(transform,3,0.4f,projectilePrefab);
    }
}
