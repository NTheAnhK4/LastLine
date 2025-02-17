
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
        
    }

    private void DeadByDamage()
    {
        StartCoroutine(DoAnim());
        ObserverManager.Notify(EventId.RewardGold,m_RewardGold);
    }

    private void ReachPlayerBase()
    {
        PoolingManager.Despawn(actor.gameObject);
        ObserverManager.Notify(EventId.AttackCastle,m_Damage);
    }
}
