


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelUI : ComponentBehavior
{
    
    public GameObject SignalWayPrefab;
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI wayNumTxt;
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private WinUI winUI;
    [SerializeField] private CenterUI loseUI;
    [SerializeField] private PanelUI panel;

    private Dictionary<Vector3, GameObject> m_SignalWayChecker = new Dictionary<Vector3, GameObject>();

    private LevelParam m_LevelParam;
    private System.Action<object> onWinHandler;
    private System.Action<object> onSpawnWayHandler;
    private System.Action<object> onAttackCastleHandler;
    private System.Action<object> onUpdateGoldHandler;
    private System.Action<object> onSpawnedEnemiesHandler;
    private System.Action<object> onLoseHandler;
   
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
        winUI = center.Find("WinUI").GetComponent<WinUI>();
        loseUI = center.Find("LoseUI").GetComponent<CenterUI>();
        panel = center.Find("Panel").GetComponent<PanelUI>();
    }

    private void OnEnable()
    {
       
        onSpawnWayHandler = param => UpdateWayUI((int)param);
        onAttackCastleHandler = param => UpdateHealthUI((float)param);
        onUpdateGoldHandler = param => UpdateGoldUI((int)param);
        onSpawnedEnemiesHandler = param => SpawnSignalWay((int)param);
        onLoseHandler = _ => OnLose();
        onWinHandler = param => OnWin((int)param);
       
        ObserverManager<GameEventID>.Attach(GameEventID.SpawnWay, onSpawnWayHandler);
        ObserverManager<GameEventID>.Attach(GameEventID.AttackCastle, onAttackCastleHandler);
        ObserverManager<GameEventID>.Attach(GameEventID.UpdateGold, onUpdateGoldHandler);
        ObserverManager<GameEventID>.Attach(GameEventID.SpawnedEnemies, onSpawnedEnemiesHandler);
        ObserverManager<GameEventID>.Attach(GameEventID.Win, onWinHandler);
        ObserverManager<GameEventID>.Attach(GameEventID.Lose, onLoseHandler);
       
    }

    private void OnDisable()
    {
        ObserverManager<GameEventID>.Detach(GameEventID.SpawnWay, onSpawnWayHandler);
        ObserverManager<GameEventID>.Detach(GameEventID.AttackCastle, onAttackCastleHandler);
        ObserverManager<GameEventID>.Detach(GameEventID.UpdateGold, onUpdateGoldHandler);
        ObserverManager<GameEventID>.Detach(GameEventID.SpawnedEnemies, onSpawnedEnemiesHandler);
        ObserverManager<GameEventID>.Detach(GameEventID.Win, onWinHandler);
        ObserverManager<GameEventID>.Detach(GameEventID.Lose, onLoseHandler);
        
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
        healthTxt.text = InGameManager.Instance.HealthPoint.ToString();
        int wayNum = m_LevelParam.Ways.Count;
        wayNumTxt.text = "0/" + wayNum.ToString();

    }

    private void SpawnSignalWay(int wayId, bool isActive = true)
    {
        if(this == null) return;
        var paths = m_LevelParam.Roots;
        
        foreach (MiniWayParam miniWay in m_LevelParam.Ways[wayId + 1].MiniWays)
        {
            int pathId = miniWay.RootID;
            if(m_SignalWayChecker.ContainsKey(paths[pathId].SignalPosition)) continue;
            
            Transform signalWayTrf = PoolingManager.Spawn(SignalWayPrefab, paths[pathId].SignalPosition).transform;
            if (signalWayTrf != null)
            {
                signalWayTrf.localScale = new Vector3(1, 1, 1) * 2;
                m_SignalWayChecker[paths[pathId].SignalPosition] = signalWayTrf.gameObject;
                RectTransform border = signalWayTrf.Find("Signal").Find("SignalWay").Find("Boder 2").GetComponent<RectTransform>();
                border.rotation = Quaternion.Euler(new Vector3(0, 0, paths[pathId].SignalAngle));
                WaySignal waySignal = signalWayTrf.GetComponentInChildren<WaySignal>();
                waySignal.Init(10, isActive);
            }
        }
        m_SignalWayChecker.Clear();
    }

    private void SetUICenterActive(CenterUI centerUI)
    {
        if (centerUI == null)
        {
            Debug.LogWarning("centerUI has been destroyed or is missing.");
            return;
        }
        centerUI.ShowUI();
        panel.gameObject.SetActive(true);
        
    }
    private void OnWin(int starCount)
    {
        winUI.SetStars(starCount);
        SetUICenterActive(winUI);
    }

    private void OnLose()
    {
        SetUICenterActive(loseUI);
    }

   

}

