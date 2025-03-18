

using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class SummonAttack : ComponentBehavior
{
    [SerializeField] private Vector3 m_SpawnPosition;

    [SerializeField] private GameObject m_Unit;
    [SerializeField] private int summonLimit;
    [SerializeField] private float m_SummonCooldown;
    
    private bool isSummoning;
    private bool[][] summonedUnits = new bool[3][];
    private List<Vector2Int> randomPositions;
    public List<Solider> Soliders;

    
    public void Init(GameObject unit, Vector3 spawnPosition, float summonedCooldown)
    {
        Soliders = new List<Solider>();
        m_Unit = unit;
        summonLimit = 3;
        
        m_SummonCooldown = summonedCooldown;
        m_SpawnPosition = spawnPosition;
        isSummoning = false;
        for(int i = 0; i < summonLimit; ++i) OnSummon();


    }

    protected override void Awake()
    {
        base.Awake();
        randomPositions = new List<Vector2Int>()
        {
            new Vector2Int(-1,1), new Vector2Int(0,0), new Vector2Int(1,1),
            new Vector2Int(-1,-1), new Vector2Int(1,-1), new Vector2Int(-1,0),
            new Vector2Int(0,1), new Vector2Int(1,0), new Vector2Int(0,-1)
        };
        for (int i = 0; i < 3; ++i)
        {
            summonedUnits[i] = new bool[3];
        }
    }

   
    private void Update()
    {
        if(Soliders.Count >= summonLimit || isSummoning) return;
        StartCoroutine(SummonByTime());

    }

    IEnumerator SummonByTime()
    {
        isSummoning = true;
        
        yield return new WaitForSeconds(m_SummonCooldown);
        OnSummon();
        isSummoning = false;
    }

    private void OnSummon()
    {
        Vector3 newPosition = GetSpawnPos(m_SpawnPosition);
        Solider solider = PoolingManager.Spawn(m_Unit,newPosition, default,transform)
            .GetComponent<Solider>();
        Soliders.Add(solider);
        solider.Init(this,m_SpawnPosition);
    }

    private Vector3 GetSpawnPos(Vector3 root)
    {
        foreach (Vector2Int pos in randomPositions)
        {
            int i = pos.x + 1;
            int j = pos.y + 1;
            if (!summonedUnits[i][j])
            {
                summonedUnits[i][j] = true;
                return new Vector3(root.x + pos.x, root.y + pos.y, root.z);
            }
        }
        return Vector3.zero;
    }

    public void Disband()
    {
        foreach (var solider in Soliders)
        {
            PoolingManager.Despawn(solider.gameObject);
        }
    }
    
}
