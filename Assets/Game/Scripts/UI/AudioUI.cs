
using UnityEngine;

using UnityEngine.UI;

public class AudioUI : ComponentBehavior
{
    [Header("Sound")]
    [SerializeField] private Button m_SoundBtn;

    [SerializeField] private Sprite m_SoundImg;
    [SerializeField] private Sprite m_MuteSoundImg;
    private bool _IsSoundActive;
    private float _OldSoundValue;
   
    [Header("Music")]
    [SerializeField] private Button m_MusicBtn;

    [SerializeField] private Sprite m_MusicImg;
    [SerializeField] private Sprite m_MuteMusicImg;
    private bool _IsMusicActive;
    private float _OldMusicValue;
    
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
        
       
        m_SoundSlider.onValueChanged.AddListener(OnSoundValueChanged);
        m_MusicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        
        m_SoundSlider.value = AudioManager.Instance.SfxVolumeRate;
        m_MusicSlider.value = AudioManager.Instance.MusicVolumeRate;
        
        _IsSoundActive = m_SoundSlider.value > 0;
        _IsMusicActive = m_MusicSlider.value > 0;

        _OldMusicValue = m_MusicSlider.value;
        _OldSoundValue = m_SoundSlider.value;
        
        m_MusicBtn.onClick.AddListener(OnMusicBtnClick);
        m_SoundBtn.onClick.AddListener(OnSoundBtnClick);
    }

    private void OnMusicBtnClick()
    {
        _IsMusicActive = !_IsMusicActive;
        if(_IsMusicActive) m_MusicSlider.value = _OldMusicValue; 
        else
        {
            _OldMusicValue = Mathf.Max(m_MusicSlider.value, 0.1f);

            m_MusicSlider.value = 0; 
        }
    }

    private void OnSoundBtnClick()
    {
        _IsSoundActive = !_IsSoundActive;
        if (_IsSoundActive) m_SoundSlider.value = _OldSoundValue;
        else
        {
            _OldSoundValue = Mathf.Max(m_SoundSlider.value, 0.1f);

            m_SoundSlider.value = 0;
        }
    }
 
    private void OnSoundValueChanged(float value)
    {
        if (value == 0) m_SoundBtn.image.sprite = m_MuteSoundImg;
        else m_SoundBtn.image.sprite = m_SoundImg;
        AudioManager.Instance.SfxVolumeRate = value;
        _IsSoundActive = m_SoundSlider.value > 0;
    }

    private void OnMusicValueChanged(float value)
    {
        if (value == 0)   m_MusicBtn.image.sprite = m_MuteMusicImg;
        else  m_MusicBtn.image.sprite = m_MusicImg;
        AudioManager.Instance.MusicVolumeRate = value;
        _IsMusicActive = m_MusicSlider.value > 0;

    }
    private void OnDestroy()
    {
        m_SoundSlider.onValueChanged.RemoveListener(OnSoundValueChanged);
        m_MusicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
        m_MusicBtn.onClick.RemoveAllListeners();
        m_SoundBtn.onClick.RemoveAllListeners();
    }

}
