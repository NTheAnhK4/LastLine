


using TMPro;
using UnityEngine;


public class LevelUI : ComponentBehavior
{
    
    public GameObject SignalWayPrefab;
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI wayNumTxt;
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private Transform winUI;
    [SerializeField] private Transform loseUI;
    [SerializeField] private Transform panel;

    private LevelParam m_LevelParam;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform levelInfor = transform.Find("LevelInfor");

        if (levelInfor != null)
        {
            Transform wayInfor = levelInfor.Find("Way Infor");
            Transform hpAndCoin = levelInfor.Find("Hp and gold");

            if (wayNumTxt == null && wayInfor != null)
                wayNumTxt = wayInfor.Find("WayNum")?.GetComponent<TextMeshProUGUI>();

            if (hpAndCoin != null)
            {
                if (healthTxt == null)
                    healthTxt = hpAndCoin.Find("Hp")?.Find("HpTxt")?.GetComponent<TextMeshProUGUI>();

                if (goldTxt == null)
                    goldTxt = hpAndCoin.Find("Gold")?.Find("GoldTxt")?.GetComponent<TextMeshProUGUI>();
            }
        }

        Transform center = transform.Find("Center");
        if (winUI == null) winUI = center.Find("WinUI");
        if (loseUI == null) loseUI = center.Find("LoseUI");
        if (panel == null) panel = center.Find("Panel");
    }

    private void OnEnable()
    {
        ObserverManager.Attach(EventId.SpawnWay, param => UpdateWayUI((int)param));
        ObserverManager.Attach(EventId.AttackCastle, param  =>UpdateHealthUI((float)param));
        ObserverManager.Attach(EventId.UpdateGold, param=>UpdateGoldUI((int)param));
        ObserverManager.Attach(EventId.SpawnedEnemies,param=>SpawnSignalWay((int)param));
        ObserverManager.Attach(EventId.Win, _=>SetUICenterActive(winUI));
        ObserverManager.Attach(EventId.Lose, _=>SetUICenterActive(loseUI));
        
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.SpawnWay, param => UpdateWayUI((int)param));
        ObserverManager.Detach(EventId.AttackCastle, param =>UpdateHealthUI((float)param));
        ObserverManager.Detach(EventId.UpdateGold, param=>UpdateGoldUI((int)param));
        ObserverManager.Detach(EventId.SpawnedEnemies,param =>SpawnSignalWay((int)param));
        ObserverManager.Detach(EventId.Win, _=>SetUICenterActive(winUI));
        ObserverManager.Detach(EventId.Lose, _=>SetUICenterActive(loseUI));
    }
    
    private void UpdateWayUI(int wayId)
    {
        wayNumTxt.text = wayId.ToString() + "/" + m_LevelParam.Ways.Count.ToString();
    }

    private void UpdateHealthUI(float healthPoint)
    {
        healthTxt.text = healthPoint.ToString();
    }

    private void UpdateGoldUI(int gold)
    {
        goldTxt.text = gold.ToString();
    }

   

    public void Init(LevelParam levelParam, int preWay)
    {
        m_LevelParam = levelParam;
        if(preWay == -1) SpawnSignalWay(-1,false);
        else SpawnSignalWay(preWay);
        goldTxt.text = m_LevelParam.InitialGold.ToString();
        healthTxt.text = GameManager.Instance.HealthPoint.ToString();
        int wayNum = m_LevelParam.Ways.Count;
        wayNumTxt.text = "0/" + wayNum.ToString();

    }

    private void SpawnSignalWay(int wayId, bool isActive = true)
    {
        var paths = m_LevelParam.Paths;
        
        foreach (MiniWayParam miniWay in m_LevelParam.Ways[wayId + 1].MiniWays)
        {
            int pathId = miniWay.PathId;
            Transform signalWayTrf = PoolingManager.Spawn(SignalWayPrefab, paths[pathId].SignalPosition, default, transform).transform;

            RectTransform border = signalWayTrf.Find("SignalWay").Find("Boder 2").GetComponent<RectTransform>();
            border.rotation = Quaternion.Euler(new Vector3(0, 0, paths[pathId].SignalAngle));
            WaySignal waySignal = signalWayTrf.GetComponentInChildren<WaySignal>();
            waySignal.Init(10, isActive);

        }
    }

    private void SetUICenterActive(Transform centerUI)
    {
        centerUI.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        GameManager.Instance.GameSpeed = 0;
    }
    
}
