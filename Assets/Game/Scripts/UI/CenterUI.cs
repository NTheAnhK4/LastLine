using System;

using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class CenterUI : ComponentBehavior
{
    [SerializeField] private Vector3 originalScale;
    protected CanvasGroup CanvasGroup;
    private Tween tween;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (CanvasGroup == null) CanvasGroup = transform.GetComponent<CanvasGroup>();
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
        CanvasGroup.interactable = false;
        transform.localScale = Vector3.zero;
        
        tween = transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            CanvasGroup.interactable = true;
            GameManager.Instance.GameSpeed = 0;
           
        });

    }

    public void HideUI()
    {
        CanvasGroup.interactable = false;
        GameManager.Instance.SetPreSpeedGame();
        tween = transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
           
            gameObject.SetActive(false);
        });
        
    }

    protected void HideUI(Action actionAfterHide)
    {
        CanvasGroup.interactable = false;
        GameManager.Instance.SetPreSpeedGame();
        tween = transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            
            gameObject.SetActive(false);
            actionAfterHide?.Invoke();
        });
    }

    private void OnDestroy()
    {
        if(tween != null) tween.Kill();
    }
}