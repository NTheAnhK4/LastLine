using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNewTower : MonoBehaviour
{
    public void BuildTower()
    {
        
        Tower tower = transform.GetComponentInParent<Tower>();
        if(tower != null) StartCoroutine( tower.BuildNewTower());
    }

   
}
