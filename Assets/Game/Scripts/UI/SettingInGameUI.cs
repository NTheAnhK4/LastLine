
using UnityEngine;

using UnityEngine.UI;

public class SettingInGameUI : CenterUI
{
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button quitBtn;
   
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Button");
        if (replayBtn == null) replayBtn = buttonHolder.Find("Replay").GetComponent<Button>();
        if (quitBtn == null) quitBtn = buttonHolder.Find("Quit").GetComponent<Button>();
        
        
    }

    private void OnEnable()
    {
        quitBtn.onClick.AddListener(() =>
        {
            HideUI(() =>
            {
                GameManager.Instance.GoToWorldMap();
            });
        });
        replayBtn.onClick.AddListener(() =>
        {
            HideUI(() =>
            {
                GameManager.Instance.GameSpeed = 1;
                GameManager.Instance.ReplayLevel();
            });
        });
    }

    private void OnDisable()
    {
        quitBtn.onClick.RemoveAllListeners();
        replayBtn.onClick.RemoveAllListeners();
    }

    protected override void SetInteractable(bool canInteractable)
    {
        base.SetInteractable(canInteractable);
        replayBtn.interactable = canInteractable;
        quitBtn.interactable = canInteractable;
    }
}
