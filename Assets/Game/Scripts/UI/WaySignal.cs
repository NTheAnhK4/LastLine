

using UnityEngine;

using UnityEngine.UI;


public class WaySignal : ComponentBehavior
{
    
    [SerializeField] private Transform roundTrf;
    [SerializeField] private Image roundImg;
    [SerializeField] private float m_TimeFill = 5f;
    [SerializeField] private float m_Timer = 0;
    private bool isRoundActive = true;

    public bool IsRoundActive
    {
        get => isRoundActive;
        set
        {
            if (isRoundActive != value)
            {
                isRoundActive = value;
                roundTrf.gameObject.SetActive(isRoundActive);
            }
            
        }
    }

    public void Init(float timeFill, bool isActive)
    {
        m_TimeFill = timeFill;
        IsRoundActive = isActive;
        m_Timer = 0;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (roundTrf == null) roundTrf = transform.Find("Round");
        roundImg ??= roundTrf.GetComponent<Image>();
    }

    private void Update()
    {
        if (!IsRoundActive || m_Timer > m_TimeFill) return;
        m_Timer += Time.deltaTime;
        roundImg.fillAmount = m_Timer / m_TimeFill;
    }
}
