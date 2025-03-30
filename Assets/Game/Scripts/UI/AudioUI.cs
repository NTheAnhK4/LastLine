
using UnityEngine;

using UnityEngine.UI;

public class AudioUI : ComponentBehavior
{
    [Header("Sound")]
    [SerializeField] private Button m_SoundBtn;

    [SerializeField] private Sprite m_SoundImg;
    [SerializeField] private Sprite m_MuteSoundImg;
    private BindableValue<bool> m_IsSoundActive;
    [Header("Music")]
    [SerializeField] private Button m_MusicBtn;

    [SerializeField] private Sprite m_MusicImg;
    [SerializeField] private Sprite m_MuteMusicImg;
    private BindableValue<bool> m_IsMusicActive;

    [Header("Slider")]
    [SerializeField] private Slider m_SoundSlider;
    [SerializeField] private Slider m_MusicSlider;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        
        if (m_SoundSlider == null) m_SoundSlider = transform.Find("Sound").GetComponentInChildren<Slider>();
        if (m_SoundBtn == null) m_SoundBtn = transform.Find("Sound").GetComponentInChildren<Button>();
        if (m_MusicSlider == null) m_MusicSlider = transform.Find("Music").GetComponentInChildren<Slider>();
        if (m_MusicBtn == null) m_MusicBtn = transform.Find("Music").GetComponentInChildren<Button>();
    }

    private void Start()
    {
        
        m_SoundSlider.value = AudioManager.Instance.SfxVolumeRate;
        m_MusicSlider.value = AudioManager.Instance.MusicVolumeRate;
        m_SoundSlider.onValueChanged.AddListener(OnSoundValueChanged);
        m_MusicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        HandleMusicButonClick();
        HandleSoundButtonClick();
    }

    private void HandleMusicButonClick()
    {
        m_IsMusicActive = new BindableValue<bool>();
        m_IsMusicActive.Value = true;
        m_IsMusicActive.OnValueChanged += (oldValue, newValue) =>
        {
            if (newValue) m_MusicBtn.image.sprite = m_MusicImg;
            else
            {
                m_MusicBtn.image.sprite = m_MuteMusicImg;
                m_MusicSlider.value = 0;
            }
        };
        m_MusicBtn.onClick.AddListener(() =>
        {
            m_IsMusicActive.Value = !m_IsMusicActive.Value;
        });
    }

    private void HandleSoundButtonClick()
    {
        m_IsSoundActive = new BindableValue<bool>();
        m_IsSoundActive.Value = true;
        m_IsSoundActive.OnValueChanged += (oldValue, newValue) =>
        {
            if (newValue) m_SoundBtn.image.sprite = m_SoundImg;
            else
            {
                m_SoundBtn.image.sprite = m_MuteSoundImg;
                m_SoundSlider.value = 0;
            }
        };
        m_SoundBtn.onClick.AddListener(() =>
        {
            m_IsSoundActive.Value = !m_IsSoundActive.Value;
        });
    }
    private void OnSoundValueChanged(float value)
    {
        AudioManager.Instance.SfxVolumeRate = value;
        if (value == 0) m_IsSoundActive.Value = false;
        else m_IsSoundActive.Value = true;
    }

    private void OnMusicValueChanged(float value)
    {
       
        AudioManager.Instance.MusicVolumeRate = value;
        if (value == 0) m_IsMusicActive.Value = false;
        else m_IsMusicActive.Value = true;
    }
    private void OnDestroy()
    {
        m_SoundSlider.onValueChanged.RemoveListener(OnSoundValueChanged);
        m_MusicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
    }

}
