using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public void Init(GameObject levelPrefab)
    {
        Instantiate(levelPrefab,default,default,transform);
    }
}
