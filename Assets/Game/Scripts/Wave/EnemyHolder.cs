

using System.Collections.Generic;

using UnityEngine;

public class EnemyHolder : Singleton<EnemyHolder>
{
    private List<GameObject> _Holders = new List<GameObject>();
    private Dictionary<string, List<GameObject>> _EnemyInScene = new Dictionary<string, List<GameObject>>();
    private GameObject FindHolder(string holderName)
    {
        if (_Holders == null) _Holders = new List<GameObject>();
        foreach (GameObject holder in _Holders)
        {
            if (holder.name == holderName) return holder;
        }
    
        GameObject newHolder = new GameObject(holderName);
        newHolder.transform.parent = transform;
        _Holders.Add(newHolder);
        return newHolder;
    }
    
    private void ValidateDict(string holderName)
    {
        if (_EnemyInScene == null) _EnemyInScene = new Dictionary<string, List<GameObject>>();
        if(!_EnemyInScene.ContainsKey(holderName)) _EnemyInScene.Add(holderName, new List<GameObject>());
        if (_EnemyInScene[holderName] == null) _EnemyInScene[holderName] = new List<GameObject>();
    }
    public void HoldEnemy(GameObject enemy, string holderName)
    {
        if(enemy == null) return;
        GameObject holder = FindHolder(holderName);
        enemy.transform.parent = holder.transform;
        
        ValidateDict(holderName);
        _EnemyInScene[holderName].Add(enemy);
    }
    
    public bool IsEnemyEmpty()
    {
        foreach (string key in _EnemyInScene.Keys)
        {
            foreach (GameObject enemy in _EnemyInScene[key])
            {
                if (enemy.gameObject.activeInHierarchy) return false;
            }
        }
    
        return true;
    }
    
}
