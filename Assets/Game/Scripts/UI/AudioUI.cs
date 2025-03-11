
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : ComponentBehavior
{
    [SerializeField] private Slider m_SoundUI;
    [SerializeField] private Slider m_MusicUI;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_SoundUI == null) m_SoundUI = transform.Find("Sound").GetComponentInChildren<Slider>();
        if (m_MusicUI == null) m_MusicUI = transform.Find("Music").GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        m_SoundUI.value = AudioManager.Instance.MusicVolumeRate;
        m_MusicUI.value = AudioManager.Instance.SfxVolumeRate;
        m_SoundUI.onValueChanged.AddListener(OnSoundValueChanged);
        m_MusicUI.onValueChanged.AddListener(OnMusicValueChanged);
    }

    private void OnSoundValueChanged(float value)
    {
        AudioManager.Instance.SfxVolumeRate = value;
    }

    private void OnMusicValueChanged(float value)
    {
        AudioManager.Instance.MusicVolumeRate = value;
    }
}
