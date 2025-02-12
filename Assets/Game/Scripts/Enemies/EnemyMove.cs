
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public List<Vector3> pathList;
    [SerializeField] private Enemy enemyCtrl;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int currentPathIndex;
    [SerializeField] private float moveSpeed;

    public void Init(Enemy enemy, float eMoveSpeed)
    {
        enemyCtrl = enemy;
        moveSpeed = eMoveSpeed;
        
        currentPathIndex = 0;
        targetPosition = pathList[currentPathIndex];
    }

   
    private void Move()
    {
        Vector3 direction = (targetPosition - enemyCtrl.transform.position).normalized;
        enemyCtrl.transform.Translate(direction * (moveSpeed * Time.deltaTime));
        if (Vector3.Distance(enemyCtrl.transform.position, targetPosition) <= 0.4f)
        {
            if (currentPathIndex == pathList.Count - 1) enemyCtrl.enemyDead.OnDead();
            else SetNextPosition();
        }
    }

    private void SetNextPosition()
    {
        currentPathIndex++;
        targetPosition = pathList[currentPathIndex];
    }

    private void Update()
    {
        Move();
    }
}
