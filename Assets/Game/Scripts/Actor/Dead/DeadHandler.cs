using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHandler : MonoBehaviour
{
    [SerializeField] private Transform actor;

    public void Init(Transform actorTrf)
    {
        actor = actorTrf;
    }
    public virtual void OnDead()
    {
        PoolingManager.Despawn(actor.gameObject);
    }
}
