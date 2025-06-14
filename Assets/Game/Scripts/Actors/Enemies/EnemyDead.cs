

using System.Collections;
using UnityEngine;

public class EnemyDead : DeadHandler
{
    private int m_RewardGold;
    private float m_Damage;
    private bool _isOnDeadCalled = false;
    public void Init(int rewardGold, float damge)
    {
        _isOnDeadCalled = false;
        m_RewardGold = rewardGold;
        m_Damage = damge;
    }
    public override void OnDead(bool hasAnim)
    {
        if(_isOnDeadCalled) return;
        _isOnDeadCalled = true;
        if (hasAnim)
        {
            animHandler.SetAnim(AnimHandler.State.Dead);
          
        }
        else StartCoroutine(ReachPlayerBase());
      
    }

    public override void AfterAnimDead()
    {
        base.AfterAnimDead();
        InGameManager.Instance.Gold += m_RewardGold;
    }
    
    private IEnumerator ReachPlayerBase()
    {
        InGameManager.Instance.HealthPoint -= m_Damage;
        yield return new WaitForSeconds(0.2f);
        PoolingManager.Despawn(actor.gameObject);
    }
}
