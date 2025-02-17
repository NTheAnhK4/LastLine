

using System;
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
        ObserverManager.Attach(EventId.AttackCastle, _ =>UpdateHealthUI());
        ObserverManager.Attach(EventId.RewardGold, _=>UpdateGoldUI());
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.SpawnWay, param => UpdateWayUI((int)param));
        ObserverManager.Detach(EventId.AttackCastle, _=>UpdateHealthUI());
        ObserverManager.Detach(EventId.RewardGold, _=>UpdateGoldUI());
    }

    private void UpdateWayUI(int wayId)
    {
        wayNumTxt.text = wayId.ToString() + "/" + GameManager.Instance.WayNum.ToString();
    }

    private void UpdateHealthUI()
    {
        healthTxt.text = GameManager.Instance.HealthPoint.ToString();
    }

    private void UpdateGoldUI()
    {
        goldTxt.text = GameManager.Instance.Gold.ToString();
    }

    private void Start()
    {
        var paths = LevelData.Levels[GameManager.Instance.Level].Paths;
        foreach (MiniWayParam miniWay in LevelData.Levels[GameManager.Instance.Level].Ways[0].MiniWays)
        {
            int pathId = miniWay.PathId;
            Transform signalWayTrf = PoolingManager.Spawn(SignalWayPrefab, paths[pathId].SignalPosition, default, transform).transform;

            RectTransform border = signalWayTrf.Find("SignalWay").Find("Boder 2").GetComponent<RectTransform>();
            border.rotation = Quaternion.Euler(new Vector3(0,0,paths[pathId].SignalAngle));
            WaySignal waySignal = signalWayTrf.GetComponentInChildren<WaySignal>();
            waySignal.Init(10,false);


        }
        
    }
}
