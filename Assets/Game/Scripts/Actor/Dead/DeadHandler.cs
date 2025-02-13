using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHandler : ComponentBehavior
{
    [SerializeField] protected Transform actor;

    public void Init(Transform actorTrf)
    {
        actor = actorTrf;
    }
    public virtual void OnDead()
    {
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
        if(CanDespawn()) OnDead();
    }
}
