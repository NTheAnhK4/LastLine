
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WinUI : CenterUI
{
    [Header("Button")]
    [SerializeField] private Button rePlayBtn;
    [SerializeField] private Button continueBtn;
    [Header("Stars")] 
    [SerializeField] private List<Image> m_Stars;

    [SerializeField] private Sprite m_GoldStar;
    [SerializeField] private Sprite m_EmptyStar;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform buttonHolder = transform.Find("Buttons");
        if (rePlayBtn == null) rePlayBtn = buttonHolder.Find("Replay").GetComponent<Button>();
        if (continueBtn == null) continueBtn = buttonHolder.Find("Continue").GetComponent<Button>();
        Transform stars = transform.Find("Stars");

        if (stars != null)
        {
            foreach (Transform star in stars)
            {
                m_Stars.Add(star.GetComponent<Image>());
            }
        }
    }

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(() =>
        {
            HideUI(() =>
            {
                GameManager.Instance.GoToWorldMap();
            });
            
        });
        rePlayBtn.onClick.AddListener(() =>
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
        continueBtn.onClick.RemoveAllListeners();
        rePlayBtn.onClick.RemoveAllListeners();
    }

    protected override void SetInteractable(bool canInteractable)
    {
        base.SetInteractable(canInteractable);
        rePlayBtn.interactable = canInteractable;
        continueBtn.interactable = canInteractable;
    }

    public void SetStars(int starCount)
    {
        for (int i = 0; i <= starCount - 1; ++i)
        {
            if(i >= m_Stars.Count) return;
            m_Stars[i].sprite = m_GoldStar;
        }

        for (int i = starCount; i < 3; ++i) m_Stars[i].sprite = m_EmptyStar;
    }
}
