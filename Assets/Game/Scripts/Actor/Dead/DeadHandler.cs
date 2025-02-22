

using System.Collections;
using UnityEngine;

public class DeadHandler : ActionHandler
{
   

    public virtual void OnDead(bool hasAnim)
    {
        if (hasAnim) StartCoroutine(DoAnim());
        else PoolingManager.Despawn(actor.gameObject);
    }

    protected IEnumerator DoAnim()
    {
        animHandler.SetAnim("Dead");
        yield return new WaitForSeconds(1.5f);
        PoolingManager.Despawn(actor.gameObject);
    }
    

    protected virtual bool CanDespawn()
    {
        return false;
    }

    protected virtual void UpdateLogic()
    {
        
    }
    private void Update()
    {
        UpdateLogic();
        if(CanDespawn()) OnDead(true);
    }
}
