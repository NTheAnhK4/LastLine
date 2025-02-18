


using TMPro;
using UnityEngine;


public class LevelUI : ComponentBehavior
{
    public LevelData LevelData;
    public GameObject SignalWayPrefab;
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI wayNumTxt;
    [SerializeField] private TextMeshProUGUI goldTxt;

    
    
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
        
        
    }

    private void OnEnable()
    {
        ObserverManager.Attach(EventId.SpawnWay, param => UpdateWayUI((int)param));
        ObserverManager.Attach(EventId.AttackCastle, param  =>UpdateHealthUI((float)param));
        ObserverManager.Attach(EventId.UpdateGold, param=>UpdateGoldUI((int)param));
        ObserverManager.Attach(EventId.SpawnedEnemies,param=>SpawnSignalWay((int)param));
        
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.SpawnWay, param => UpdateWayUI((int)param));
        ObserverManager.Detach(EventId.AttackCastle, param =>UpdateHealthUI((float)param));
        ObserverManager.Detach(EventId.UpdateGold, param=>UpdateGoldUI((int)param));
        ObserverManager.Detach(EventId.SpawnedEnemies,param =>SpawnSignalWay((int)param));
        
    }
    
    private void UpdateWayUI(int wayId)
    {
        wayNumTxt.text = wayId.ToString() + "/" + GameManager.Instance.WayNum.ToString();
    }

    private void UpdateHealthUI(float healthPoint)
    {
        healthTxt.text = healthPoint.ToString();
    }

    private void UpdateGoldUI(int gold)
    {
        goldTxt.text = gold.ToString();
    }

   

    public void Init(int preWay)
    {
        if(preWay == -1) SpawnSignalWay(-1,false);
        else SpawnSignalWay(preWay,true);
        goldTxt.text = LevelData.Levels[GameManager.Instance.Level].InitialGold.ToString();
        int wayNum = LevelData.Levels[GameManager.Instance.Level].Ways.Count;
        wayNumTxt.text = "0/" + wayNum.ToString();

    }

    private void SpawnSignalWay(int wayId, bool isActive = true)
    {
        var paths = LevelData.Levels[GameManager.Instance.Level].Paths;
        
        foreach (MiniWayParam miniWay in LevelData.Levels[GameManager.Instance.Level].Ways[wayId + 1].MiniWays)
        {
            int pathId = miniWay.PathId;
            Transform signalWayTrf = PoolingManager.Spawn(SignalWayPrefab, paths[pathId].SignalPosition, default, transform).transform;

            RectTransform border = signalWayTrf.Find("SignalWay").Find("Boder 2").GetComponent<RectTransform>();
            border.rotation = Quaternion.Euler(new Vector3(0, 0, paths[pathId].SignalAngle));
            WaySignal waySignal = signalWayTrf.GetComponentInChildren<WaySignal>();
            waySignal.Init(10, isActive);

        }
    }
}
