

using System.Collections;
using UnityEngine;

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
        else StartCoroutine(ReachPlayerBase());
      
    }

    private void DeadByDamage()
    {
        StartCoroutine(DoAnim());
        InGameManager.Instance.Gold += m_RewardGold;
        
    }

    private IEnumerator ReachPlayerBase()
    {
        InGameManager.Instance.HealthPoint -= m_Damage;
        yield return new WaitForSeconds(0.2f);
        PoolingManager.Despawn(actor.gameObject);
    }
}
