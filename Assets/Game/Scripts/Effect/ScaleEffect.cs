using System;
using DG.Tweening;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    public float scaleFactor = 0.8f;
    public float duration = 0.5f;

    private void Start()
    {
        LoopScale();
    }

    private void LoopScale()
    {
        transform.DOScale(scaleFactor, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
