
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveHandler
{
    public List<Vector3> pathList;
    [SerializeField] private Enemy enemyCtrl;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int currentPathIndex;
    [SerializeField] private float moveSpeed;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyCtrl == null) enemyCtrl = transform.GetComponentInParent<Enemy>();
    }

    public void Init(float eMoveSpeed)
    {
        moveSpeed = eMoveSpeed;
        if(animHandler != null) animHandler.SetAnim("Move");
        currentPathIndex = 1;
        targetPosition = pathList[currentPathIndex];
    }


    protected override void Move()
    {
        Vector3 direction = (targetPosition - enemyCtrl.transform.position).normalized;
        enemyCtrl.transform.Translate(direction * (moveSpeed * Time.deltaTime));
        if (Vector3.Distance(enemyCtrl.transform.position, targetPosition) <= 0.4f)
        {
            if (currentPathIndex == pathList.Count - 1) enemyCtrl.enemyDead.OnDead(false);
            else SetNextPosition();
        }
    }
    

    private void SetNextPosition()
    {
        currentPathIndex++;
        targetPosition = pathList[currentPathIndex];
    }
    
}
