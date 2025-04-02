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

   

    private void OnEnable()
    {
        quitBtn.onClick.AddListener(() =>
        {
            HideUI(Exit);
        });
    }

    private void Exit()
    {
        PlayerPrefs.SetFloat("MusicVolumeData",AudioManager.Instance.MusicVolumeRate);
        PlayerPrefs.SetFloat("SFXVolumeData",AudioManager.Instance.SfxVolumeRate);
        PlayerPrefs.Save();
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
