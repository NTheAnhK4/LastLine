
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveHandler
{
    private List<Vector3> m_PathList;
    [SerializeField] private Enemy enemyCtrl;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int currentPathIndex;
    [SerializeField] private float moveSpeed;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (enemyCtrl == null) enemyCtrl = transform.GetComponentInParent<Enemy>();
    }

    public void Init(List<Vector3> pathList, float eMoveSpeed)
    {
        m_PathList = pathList;
        moveSpeed = eMoveSpeed;
        if(animHandler != null) animHandler.SetAnim("Move");
        currentPathIndex = 1;
        targetPosition = m_PathList[currentPathIndex];
    }


    protected override void Move()
    {
        Vector3 direction = (targetPosition - enemyCtrl.transform.position).normalized;
        enemyCtrl.transform.Translate(direction * (moveSpeed * Time.deltaTime));
        if (Vector3.Distance(enemyCtrl.transform.position, targetPosition) <= 0.4f)
        {
            if (currentPathIndex == m_PathList.Count - 1) enemyCtrl.enemyDead.OnDead(false);
            else SetNextPosition();
        }
    }
    

    private void SetNextPosition()
    {
        currentPathIndex++;
        targetPosition = m_PathList[currentPathIndex];
    }
    
}
