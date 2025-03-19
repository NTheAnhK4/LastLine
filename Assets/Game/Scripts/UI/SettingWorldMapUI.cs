using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class SettingWorldMapUI : CenterUI
{
    [SerializeField] private Button quitBtn;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Button");
        if (quitBtn == null) quitBtn = buttonHolder.Find("Quit").GetComponent<Button>();
    }

    protected override void SetInteractable(bool canInteractable)
    {
        base.SetInteractable(canInteractable);
        quitBtn.interactable = canInteractable;
    }

    private void OnEnable()
    {
        quitBtn.onClick.AddListener(() =>
        {
            HideUI(Exit);
        });
    }

    private void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
    private void OnDisable()
    {
        quitBtn.onClick.RemoveAllListeners();
    }
}
