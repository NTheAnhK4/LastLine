
public class GameManager : Singleton<GameManager>
{
    public LevelData LevelData;
    
    public EnemySpawner enemySpawner;
    public TowerSpawner towerSpawner;
    public int Level = 0;
    private float healthPoint = 20;
    private int gold;

    public int Gold
    {
        get => gold;
        set
        {
            if (gold != value)
            {
                gold = value;
                ObserverManager.Notify(EventId.UpdateGold, gold);
            }
        }
    }

    public float HealthPoint
    {
        get => healthPoint;
        set
        {
            if (healthPoint != value)
            {
                healthPoint = value;
                ObserverManager.Notify(EventId.AttackCastle, healthPoint);
            }
            
        }
    }

    public int WayNum => LevelData.Levels[Level].Ways.Count;
   
    private void Start()
    {
        HealthPoint = 20;
        Gold = LevelData.Levels[Level].InitialGold;
        towerSpawner.SpawnTower();
    }

    private void UpdateHealth(float damage)
    {
        HealthPoint -= damage;
        if(HealthPoint <= 0) ObserverManager.Notify(EventId.Lose);
    }
}
