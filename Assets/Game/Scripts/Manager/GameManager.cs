
public class GameManager : Singleton<GameManager>
{
    public EnemySpawner enemySpawner;
    public TowerSpawner towerSpawner;
    public int Level = 0;
    private void Start()
    {
        StartCoroutine(enemySpawner.SpawnEnemyFromId(0));
        towerSpawner.SpawnTower();
    }
}
