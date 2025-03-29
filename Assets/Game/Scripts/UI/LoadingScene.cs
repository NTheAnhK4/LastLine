using System;

using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : ComponentBehavior
{
    [SerializeField] private Slider m_LoadingSlider;
    [SerializeField] private Button m_LoadingBtn;
    [SerializeField] private Image m_GameName;
    private bool isFinishLoadingGame;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_LoadingSlider == null) m_LoadingSlider = transform.GetComponentInChildren<Slider>();
        if (m_LoadingBtn == null) m_LoadingBtn = transform.GetComponentInChildren<Button>();
        if (m_GameName == null) m_GameName = transform.Find("Top").Find("GameName").GetComponent<Image>();
        m_LoadingBtn.transform.localScale *= 0;

        var transform1 = m_GameName.transform;
        transform1.localScale *= 0;
        transform1.position = Vector3.zero;
        isFinishLoadingGame = false;
    }

    private void Start()
    {
        AudioManager.PlayBackGroundMusic(SoundType.LoadingScene);
        m_LoadingBtn.onClick.AddListener(() =>
        {
            m_LoadingBtn.interactable = false;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(m_LoadingBtn.transform.DOScale(0.75f, 0.2f))
                .Append(m_LoadingBtn.transform.DOScale(1.2f, 0.05f)).OnComplete(() =>
                {
                    SceneManager.LoadScene("WorldMap");
                    AudioManager.PlayBackGroundMusic(SoundType.SelectLevel);
                });
        });
    }

    private void Update()
    {
        if(isFinishLoadingGame) return;
       
        UpdateSlider();
        if (Math.Abs(m_LoadingSlider.value - 1) < 0.00001) OnLoadingFinish();
    }

    private void UpdateSlider()
    {
        float valueAdded;
        if (m_LoadingSlider.value < 0.7f) valueAdded = Time.deltaTime / 5;
        else if (m_LoadingSlider.value < 0.9f) valueAdded = Time.deltaTime / 10;
        else valueAdded = Time.deltaTime;
        m_LoadingSlider.value = Mathf.Min(1, m_LoadingSlider.value + valueAdded);
    }

    private void OnLoadingFinish()
    {
        isFinishLoadingGame = true;
        m_LoadingSlider.gameObject.SetActive(false);
       

        Sequence gameNameSequence = DOTween.Sequence();
        gameNameSequence.Append(m_GameName.transform.DOMove(new Vector3(0, 3, 0), 0.3f));
        gameNameSequence.Join(m_GameName.transform.DOScale(1, 0.3f));
        gameNameSequence.Play().OnComplete(() =>
        {
            m_LoadingBtn.transform.DOScale(1, 0.2f);
        });
    }
}

