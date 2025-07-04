

using UnityEngine;

using UnityEngine.UI;


public class WaySignal : ComponentBehavior
{
    
    [SerializeField] private Transform roundTrf;
    [SerializeField] private Image roundImg;
    [SerializeField] private Button signalBtn;
    [SerializeField] private float m_TimeFill = 5f;
    [SerializeField] private float m_Timer = 0;
    private bool isRoundActive = true;
    private System.Action<object> onSpawnWayHandler;

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

    private void OnEnable()
    {
        signalBtn.onClick.AddListener(() =>
        {
            if (IsRoundActive)
            {
                int goldNum = Mathf.RoundToInt(m_TimeFill - m_Timer);
                InGameManager.Instance.Gold += goldNum;
            }
            ObserverManager<GameEventID>.Notify(GameEventID.SpawnWay);
            Disapperance();
        });

        ObserverManager<GameEventID>.Attach(GameEventID.SpawnWay, _ => Disapperance());
    }

    private void OnDisable()
    {
        signalBtn.onClick.RemoveAllListeners();
    }

    private void Disapperance()
    {
        if (this == null || gameObject == null || transform.parent == null || transform.parent.parent == null) return;
        transform.parent.parent.gameObject.SetActive(false);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (roundTrf == null) roundTrf = transform.Find("Round");
        roundImg ??= roundTrf.GetComponent<Image>();
        signalBtn ??= transform.GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (!IsRoundActive ) return;
        if(m_Timer >= m_TimeFill){
            ObserverManager<GameEventID>.Notify(GameEventID.SpawnWay);
            PoolingManager.Despawn(transform.parent.parent.gameObject);
        }
        m_Timer += Time.deltaTime;
        roundImg.fillAmount = m_Timer / m_TimeFill;
    }
}
