using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : ComponentBehavior
{
    [SerializeField] protected Transform actor;
    [SerializeField] protected AnimHandler animHandler;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (animHandler == null) animHandler = transform.parent.parent.GetComponentInChildren<AnimHandler>();
        actor = transform.parent.parent;
    }

    
}
