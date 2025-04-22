
using System.Collections.Generic;
using Core.UI;
using TMPro;
using UnityEngine;

public class UIManager : ComponentBehavior
{
    public GameObject SignalWayPrefab;
    
   
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI wayNumTxt;
    [SerializeField] private TextMeshProUGUI goldTxt;
    
    [SerializeField] private  WinView winView;
    [SerializeField] private LoseView loseView;
    [SerializeField] private InGameSetting inGameSetting;
   
    
    
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
        if (winView == null) winView = transform.GetComponentInChildren<WinView>();
        if (loseView == null) loseView = transform.GetComponentInChildren<LoseView>();
        if (inGameSetting == null) inGameSetting = transform.GetComponentInChildren<InGameSetting>();
      
    }

    private void OnEnable()
    {
        
        ObserverManager<GameEventID>.Attach(GameEventID.SpawnWay, _ => UpdateWayUI());
        ObserverManager<GameEventID>.Attach(GameEventID.AttackCastle, param => UpdateHealthUI((float)param));
        ObserverManager<GameEventID>.Attach(GameEventID.UpdateGold, param => UpdateGoldUI((int)param));
        ObserverManager<GameEventID>.Attach(GameEventID.DisplaySignal, param => SpawnSignalWay((int)param));
        ObserverManager<GameEventID>.Attach(GameEventID.DisplayFirstGameSignal, param => SpawnSignalWay((int)param, false));
        ObserverManager<GameEventID>.Attach(GameEventID.Win, param => OnWin((int)param));
        ObserverManager<GameEventID>.Attach(GameEventID.Lose, _ => OnLose());
       
    }

    
    private void UpdateWayUI()
    {
        wayNumTxt.text = (InGameManager.Instance.GetCurrentWay() + 1).ToString() + "/" + InGameManager.Instance.TotalWay.ToString();
    }

    private void UpdateHealthUI(float healthPoint)
    {
        healthTxt.text = healthPoint.ToString();
    }

    private void UpdateGoldUI(int gold)
    {
        goldTxt.text = gold.ToString();
    }

   

    public void Init(LevelParam levelParam)
    {
        m_LevelParam = levelParam;
       
        goldTxt.text = m_LevelParam.InitialGold.ToString();
        healthTxt.text = InGameManager.Instance.HealthPoint.ToString();
        int wayNum = InGameManager.Instance.TotalWay;
        wayNumTxt.text = "0/" + wayNum.ToString();

    }

    private void SpawnSignalWay(int miniWayId, bool isActive = true)
    {
        if(this == null) return;
        var paths = m_LevelParam.Roots;
      
        Transform signalWayTrf = PoolingManager.Spawn(SignalWayPrefab, paths[miniWayId].SignalPosition).transform;
        if (signalWayTrf != null)
        {
            signalWayTrf.localScale = new Vector3(1, 1, 1) * 2;
            
            RectTransform border = signalWayTrf.Find("Signal").Find("SignalWay").Find("Boder 2").GetComponent<RectTransform>();
            border.rotation = Quaternion.Euler(new Vector3(0, 0, paths[miniWayId].SignalAngle));
            WaySignal waySignal = signalWayTrf.GetComponentInChildren<WaySignal>();
            waySignal.Init(10, isActive);
        }
        
    }

   
    private async void OnWin(int starCount)
    {
        winView.StarCount = starCount;
        await ViewAnimationController.PlayForceShowAnimation(ViewAnimationType.PopZoom, winView);
    }

    private async void OnLose()
    {
        
        await ViewAnimationController.PlayForceShowAnimation(ViewAnimationType.PopZoom, loseView);
    }

    public async void OnSetting()
    {
        GameManager.Instance.GameSpeed = 0;
        await ViewAnimationController.PlayShowAnimation(ViewAnimationType.PopZoom, inGameSetting);
    }
    
}
