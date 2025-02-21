using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CenterUI : ComponentBehavior
{
    [SerializeField] private Vector3 originalScale;
    private Tween tween;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (originalScale == Vector3.zero)
        {
            var transform1 = transform;
            originalScale = transform1.localScale;
            transform1.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        tween = transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            GameManager.Instance.GameSpeed = 0;
            
        });
        
    }
    public void HideUI()
    {
        GameManager.Instance.SetPreSpeedGame();
        tween = transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        
    }

    private void OnDestroy()
    {
        if(tween != null) tween.Kill();
    }
}
