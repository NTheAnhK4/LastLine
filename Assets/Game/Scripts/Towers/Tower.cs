
using System;
using System.Collections;
using UnityEngine;


public class Tower : ComponentBehavior
{
    [SerializeField] private Animator anim;
    
    public TowerUI TowerUIPrefab;
    [SerializeField] private TowerUI towerUI;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (anim == null) anim = transform.GetComponentInChildren<Animator>();
    }

    public void ShowUI()
    {
        towerUI = PoolingManager.Spawn(TowerUIPrefab.gameObject,transform.position).GetComponent<TowerUI>();
        towerUI.tower = this;
    }

    public void HideUI()
    {
        PoolingManager.Despawn(towerUI.gameObject);
    }

    
    public void UpdateTower(GameObject tower, string updateAnimName, float timerBuild = 1)
    {
        anim.SetTrigger(updateAnimName);
        StartCoroutine(BuildNewTower(tower, timerBuild));
    }

    IEnumerator BuildNewTower(GameObject tower, float timeBuild = 1)
    {
        yield return new WaitForSeconds(timeBuild);
        PoolingManager.Spawn(tower,transform.position);
        PoolingManager.Despawn(this.gameObject);
        
    }
    
}
