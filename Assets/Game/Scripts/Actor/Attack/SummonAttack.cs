

using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class SummonAttack : ComponentBehavior
{
    [SerializeField] private Vector3 m_SpawnPosition;

    [SerializeField] private GameObject SummonedUnitPrefab;
    [SerializeField] private int summonLimit;
    [SerializeField] private float m_SummonCooldown;
    [SerializeField] private int currentNumber;
    private bool isSummoning;
    private bool[][] summonedUnits = new bool[3][];
    private List<Vector2Int> randomPositions;

    public void Init(Vector3 spawnPosition, float summonedCooldown)
    {
        summonLimit = 3;
        currentNumber = 0;
        m_SummonCooldown = summonedCooldown;
        m_SpawnPosition = spawnPosition;
        isSummoning = false;
        for (int i = 1; i <= summonLimit; ++i)
        {
            Vector3 newPosition = GetSpawnPos(m_SpawnPosition);
            Solider solider = PoolingManager.Spawn(SummonedUnitPrefab,newPosition, default,transform)
                .GetComponent<Solider>();
        
            solider.Init(m_SpawnPosition);
        }

        currentNumber = summonLimit;
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
        if(currentNumber >= summonLimit || isSummoning) return;
        StartCoroutine(Summon());

    }

    IEnumerator Summon()
    {
        isSummoning = true;
        currentNumber++;
        yield return new WaitForSeconds(m_SummonCooldown);
        Vector3 newPosition = GetSpawnPos(m_SpawnPosition);
        Solider solider = PoolingManager.Spawn(SummonedUnitPrefab,newPosition, default,transform)
            .GetComponent<Solider>();
        
        solider.Init(m_SpawnPosition);
        isSummoning = false;
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
    
}
