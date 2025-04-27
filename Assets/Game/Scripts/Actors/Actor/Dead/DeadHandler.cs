
using System;

public class DeadHandler : ActionHandler
{

   

  

    public virtual void OnDead(bool hasAnim)
    {
      
        if (hasAnim) return;
        PoolingManager.Despawn(actor.gameObject);
    }


    public virtual void AfterAnimDead()
    {
        
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
