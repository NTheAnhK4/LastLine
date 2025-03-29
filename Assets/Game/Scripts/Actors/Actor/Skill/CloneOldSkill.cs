
using UnityEngine;

public class CloneOldSkill : SummonOldSkill
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
        Enemy enemy = objectPrefab.GetComponent<Enemy>();
        enemy.Init(m_Enemy.EnemyId, m_Enemy.PathId);
       
        return objectPrefab;
    }
}
