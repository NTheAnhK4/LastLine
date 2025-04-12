using System.Collections;
using System.Collections.Generic;
using Core.UI;
using DG.Tweening;
using UnityEngine;

public abstract class ViewAnimation : ScriptableObject
{
    protected Sequence _animation;
    public abstract Sequence PlayShowAnimation(View view);
    public abstract Sequence PlayHideAnimation(View view);
    //public abstract Sequence PlayTransition(View from, View to);
}
