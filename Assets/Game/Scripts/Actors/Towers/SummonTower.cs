
using UnityEngine;

public class SummonTower : Tower
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
        
        guardianAttack.Init(Data.Towers[m_TowerId].UnitPrefab,m_FlagPosition,Data.Towers[m_TowerId].AttackSpeed);
    }

    private void OnDisable()
    {
        guardianAttack.Disband();
    }
}
