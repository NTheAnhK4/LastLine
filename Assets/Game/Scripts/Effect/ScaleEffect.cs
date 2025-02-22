using System;
using DG.Tweening;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    public float scaleFactor = 1.2f;
    public float duration = 0.5f;
    private Tween tween;
    private void Start()
    {
        LoopScale();
    }

    private void LoopScale()
    {
        Sequence scaleSequence = DOTween.Sequence();
        tween = scaleSequence.Append(transform.DOScale(scaleFactor, duration * 0.5f).SetEase(Ease.OutExpo)) 
            .Append(transform.DOScale(1f, duration).SetEase(Ease.InSine)) 
            .SetLoops(-1);

    }

    private void OnDestroy()
    {
        if(tween != null) tween.Kill();
    }
}
