using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : ComponentBehavior
{
    [SerializeField] private Button replayBtn;
    [SerializeField] private Vector3 originalScale;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (replayBtn == null) replayBtn = transform.Find("Button").Find("Replay").GetComponent<Button>();
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
        transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            GameManager.Instance.GameSpeed = 0;
            
        });
        
    }

    public void HideUI()
    {
        GameManager.Instance.GameSpeed = 1;
        transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        
    }
    
}
