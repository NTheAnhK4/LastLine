

public class EnemyDead : DeadHandler
{
    private int m_RewardGold;
    private float m_Damage;
    public void Init(int rewardGold, float damge)
    {
        m_RewardGold = rewardGold;
        m_Damage = damge;
    }
    public override void OnDead(bool hasAnim)
    {
        if(hasAnim) DeadByDamage();
        else ReachPlayerBase();
        InGameManager.Instance.HandleEnemyDead(actor.GetComponent<Enemy>());
    }

    private void DeadByDamage()
    {
        StartCoroutine(DoAnim());
        InGameManager.Instance.Gold += m_RewardGold;
        
    }

    private void ReachPlayerBase()
    {
        PoolingManager.Despawn(actor.gameObject);
        InGameManager.Instance.HealthPoint -= m_Damage;
        
    }
}
