
public class GameManager : Singleton<GameManager>
{
    public LevelData LevelData;
    
    public EnemySpawner enemySpawner;
    public TowerSpawner towerSpawner;
    public int Level = 0;
    
    public float HealthPoint = 20;
    public int Gold = 0;
    public int WayNum => LevelData.Levels[Level].Ways.Count;
    private void OnEnable()
    {
        ObserverManager.Attach(EventId.RewardGold, param=>UpdateGold((int)param));
        ObserverManager.Attach(EventId.AttackCastle, param=>UpdateHealth((float)param));
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.RewardGold, param=>UpdateGold((int)param));
        ObserverManager.Detach(EventId.AttackCastle, param=>UpdateHealth((float)param));
    }
    private void Start()
    {
        HealthPoint = 20;
        Gold = 0;
        //enemySpawner.SpawnWays();
        towerSpawner.SpawnTower();
    }
    private void UpdateGold(int goldNum)
    {
        Gold += goldNum;
    }

    private void UpdateHealth(float damage)
    {
        HealthPoint -= damage;
        if(HealthPoint <= 0) ObserverManager.Notify(EventId.Lose);
    }
}
