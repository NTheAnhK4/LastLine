
using UnityEngine;

public class CloneSkill : SummonSkill
{
    private Enemy m_Enemy;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_Enemy == null) m_Enemy = transform.GetComponentInParent<Enemy>();
        m_ObjectCreated = m_Enemy.gameObject;
        m_Loop = true;
        m_SpawnCount = 1;
        m_CoolDown = 10f;
    }

    protected override Vector3 GetPosition()
    {
        return m_Enemy.transform.TransformPoint(Vector3.left * 0.3f);
    }

    protected override GameObject SpawnObject(Vector3 position)
    {
        GameObject objectPrefab = base.SpawnObject(position);
        if (objectPrefab == null) return null;
        if (m_Enemy is MeleeEnemy meleeEnemy)
        {
            MeleeEnemy enemyPrefab = objectPrefab.GetComponent<MeleeEnemy>();
            if (enemyPrefab != null)
            {
                if (meleeEnemy.EData is MeleeEnemyParam meleeEnemyParam)
                {
                    enemyPrefab.Init(meleeEnemyParam,meleeEnemy.EnemyMove.PathList);
                    InGameManager.Instance.HandeEnemyCloneSpawn(enemyPrefab);
                }
                
            }
        }
        else if (m_Enemy is RangedEnemy rangedEnemy)
        {
            RangedEnemy enemyPrefab = objectPrefab.GetComponent<RangedEnemy>();
            if (enemyPrefab != null)
            {
                if (rangedEnemy.EData is RangedEnemyParam rangedEnemyParam)
                {
                    enemyPrefab.Init(rangedEnemyParam,rangedEnemy.EnemyMove.PathList);
                    InGameManager.Instance.HandeEnemyCloneSpawn(enemyPrefab);
                }
                
            }
               
        }
        return objectPrefab;
    }
}
