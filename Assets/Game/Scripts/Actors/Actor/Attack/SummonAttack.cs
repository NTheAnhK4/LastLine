

using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class SummonAttack : ComponentBehavior
{
    [SerializeField] private Vector3 m_FlagPosition;

    [SerializeField] private GameObject m_Unit;
    [SerializeField] private int summonLimit;
    [SerializeField] private float m_SummonCooldown;
    
    
    private List<Vector3> m_SpawnPosition = new List<Vector3>();
    private Dictionary<Vector3, Solider> activeUnits = new Dictionary<Vector3, Solider>();

    private int m_ActorLevel;
    public void Init(GameObject unit, Vector3 flagPosition, float summonedCooldown, int actorLevel = 1)
    {
        activeUnits = new Dictionary<Vector3, Solider>();
        m_Unit = unit;
        m_ActorLevel = actorLevel;
        
        summonLimit = 3;
        
       
        m_SummonCooldown = summonedCooldown;
        m_FlagPosition = flagPosition;
        InitializeSpawnPosition(m_FlagPosition);
        
       
        for(int i = 0; i < summonLimit; ++i) OnSummon();


    }


    private void InitializeSpawnPosition(Vector3 flagPosition, float radius = 0.8f)
    {
        m_SpawnPosition.Clear();
        for (int i = 0; i < summonLimit; ++i)
        {
            float angle = (i / (float)summonLimit) * Mathf.PI * 2;

            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0f) * radius + flagPosition;
            m_SpawnPosition.Add(pos);
        }
    }
   
   

    IEnumerator SummonByTime()
    {
        yield return new WaitForSeconds(m_SummonCooldown);
        if (activeUnits.Count < summonLimit)
        {
            OnSummon();
        }
        
    }

    private void OnSummon()
    {
        Vector3 newPosition = GetSpawnPosition();
        Transform transform1;
        Solider solider = PoolingManager.Spawn(m_Unit,(transform1 = transform).parent.parent.position)
            .GetComponent<Solider>();
        if (solider != null)
        {
            activeUnits[newPosition] = solider;
            solider.Init(this,newPosition, m_ActorLevel);
            
            solider.Move.MoveToTarget(newPosition, true);


        }
       
    }

    private Vector3 GetSpawnPosition()
    {
        foreach (Vector3 spawnPos in m_SpawnPosition)
        {
            if (!activeUnits.ContainsKey(spawnPos)) return spawnPos;
        }

        Debug.LogWarning("Not enough space for solider");
        return Vector3.zero;
    }

    public void OnSoliderDead(Vector3 position)
    {
        if (activeUnits.ContainsKey(position))
        {
            activeUnits.Remove(position);
            StartCoroutine(SummonByTime());
        }
        
    }
    public void Disband()
    {
        foreach (var solider in activeUnits.Values)
        {
            if(solider != null) PoolingManager.Despawn(solider.gameObject);
            
        }
    }

    public void SetNewFlagPosition(Vector3 newFlagPosition)
    {
        m_FlagPosition = newFlagPosition;
        
        //clear old dictionary
        List<Solider> soliders = new List<Solider>(activeUnits.Values);
        activeUnits.Clear();
        InitializeSpawnPosition(newFlagPosition);
        
        //move solider
        foreach (Solider solider in soliders)
        {
            Vector3 newPosition = GetSpawnPosition();
            solider.Move.MoveToTarget(newPosition,true);
            activeUnits[newPosition] = solider;
        }
        
    }
}
