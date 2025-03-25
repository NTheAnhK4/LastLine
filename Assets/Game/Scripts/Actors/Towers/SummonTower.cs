
using UnityEngine;

public class SummonTower : Tower
{
    [SerializeField] private SummonAttack guardianAttack;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (guardianAttack == null) guardianAttack = transform.GetComponentInChildren<SummonAttack>();
        
    }

    public override void Init(int towerId, Vector3 flagPosition, int towerLevel)
    {
        base.Init(towerId, flagPosition, towerLevel);
        
        guardianAttack.Init(Data.Towers[m_TowerId].UnitPrefab,m_FlagPosition,Data.Towers[m_TowerId].AttackSpeed, m_TowerLevel);
    }

    private void OnDisable()
    {
        guardianAttack.Disband();
    }

    public void SetNewFlag(Vector3 newFlagPosition)
    {
        m_FlagPosition = newFlagPosition;
        if(guardianAttack != null) guardianAttack.SetNewFlagPosition(newFlagPosition);
    }
}
