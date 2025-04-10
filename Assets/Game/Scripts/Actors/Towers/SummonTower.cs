
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
        if(animHandler != null) animHandler.SetAnim(AnimHandler.State.Idle);
        guardianAttack.Init(m_TowerData.Towers[m_TowerId].UnitPrefab,m_FlagPosition,m_TowerData.Towers[m_TowerId].AttackSpeed, m_TowerLevel);
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
