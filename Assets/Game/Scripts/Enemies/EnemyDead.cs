
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
        GameManager.Instance.Gold += m_RewardGold;
    }

    private void ReachPlayerBase()
    {
        PoolingManager.Despawn(actor.gameObject);
        GameManager.Instance.HealthPoint -= m_Damage;
        
    }
}
