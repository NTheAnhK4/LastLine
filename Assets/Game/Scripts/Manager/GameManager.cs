
public class GameManager : Singleton<GameManager>
{
    public EnemySpawner enemySpawner;

    private void Start()
    {
        StartCoroutine(enemySpawner.SpawnEnemyFromId(0));
    }
}
