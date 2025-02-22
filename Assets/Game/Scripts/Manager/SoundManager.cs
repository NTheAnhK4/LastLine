using System;


using UnityEngine;


public enum SoundType
{
    BackGround,
    Arrow
}
public class SoundManager : Singleton<SoundManager>
{
    [Header("-----Audio Source-----")]
    [SerializeField] private AudioSource m_MusicSource;
    [SerializeField] private AudioSource m_SFXSource;

    [Header("-----Audio Clip-----")] [SerializeField]
    private SoundSource[] soundList;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_MusicSource.clip = soundList[(int)SoundType.BackGround].Sound;
        m_MusicSource.Play();
    }

    public static void PlaySFX(SoundType sound, float volume = 1)
    {
        Instance.m_SFXSource.PlayOneShot(Instance.soundList[(int)sound].Sound, volume);
    }

    public static void PlayBackGroundMusic(SoundType sound, float volume = 1)
    {
        Instance.m_MusicSource.Stop();
        Instance.m_MusicSource.clip = Instance.soundList[(int)sound].Sound;
        Instance.m_MusicSource.Play();
    }

    private void OnValidate()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList,names.Length);
        for (int i = 0; i < names.Length; ++i)
        {
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

