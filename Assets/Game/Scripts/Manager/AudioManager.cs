using System;


using UnityEngine;
using UnityEngine.Serialization;


public enum SoundType
{
    InGame,
    SelectLevel,
    LoadingScene,
    Arrow,
    SoilBreak,
    MagicImpact
}
public class AudioManager : Singleton<AudioManager>
{
    [Header("-----Audio Source-----")]
    [SerializeField] private AudioSource m_MusicSource;
    [SerializeField] private AudioSource m_SFXSource;

    [Header("-----Audio Clip-----")] [SerializeField]
    private SoundSource[] soundList;

  
  
    
    [Header("----------Data----------")] 
    [SerializeField] private float m_MusicVolumeRate = 1;
    [SerializeField] private float m_SFXVolumeRate = 1;

    public float MusicVolumeRate
    {
        get => m_MusicVolumeRate;
        set
        {
            // if (Math.Abs(m_SFXVolumeRate - value) > 0.0001f)
            // {
            //     Debug.Log(value);
            //     m_MusicSource.volume = value;
            //     m_MusicVolumeRate = value;
            // }
            m_MusicSource.volume = value;
            m_MusicVolumeRate = value;
            
        }
    }

    public float SfxVolumeRate
    {
        get => m_SFXVolumeRate;
        set => m_SFXVolumeRate = value;
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        m_MusicVolumeRate = PlayerPrefs.GetFloat("MusicVolumeData", 0.5f);
        m_SFXVolumeRate = PlayerPrefs.GetFloat("SFXVolumeData", 0.5f);
    }

 

    public static void PlaySFX(SoundType sound, float volume = 1)
    {
        
        volume = Mathf.Clamp(volume * Instance.m_SFXVolumeRate, 0, 1);
        Instance.m_SFXSource.PlayOneShot(Instance.soundList[(int)sound].Sound, volume);
    }

    public static void PlayBackGroundMusic(SoundType sound)
    {
       
        Instance.m_MusicSource.Stop();
        Instance.m_MusicSource.clip = Instance.soundList[(int)sound].Sound;

        Instance.m_MusicSource.volume = Instance.m_MusicVolumeRate;
        Instance.m_MusicSource.Play();
    }

    private void OnValidate()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
    
        if (soundList == null || soundList.Length != names.Length)
        {
            Array.Resize(ref soundList, names.Length);
        }

        for (int i = 0; i < names.Length; ++i)
        {
            if (soundList[i].name == null) 
            {
                soundList[i] = new SoundSource();
            }
            soundList[i].name = names[i];
        }
    }


}

[Serializable]
public struct SoundSource
{
    [HideInInspector] public string name;
    [SerializeField] private AudioClip sound;
    public AudioClip Sound
    {
        get => sound;
    }
}

