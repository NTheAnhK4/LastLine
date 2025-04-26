using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDead : MonoBehaviour
{
    public void HandleDead()
    {
        
        AnimHandler anim = transform.GetComponent<AnimHandler>();
        if(anim != null) anim.ResetAnim();
        Transform transform1;
        (transform1 = transform).parent.GetComponentInChildren<DeadHandler>()?.AfterAnimDead();
       
       
        PoolingManager.Despawn(transform1.parent.gameObject);
    }
}
