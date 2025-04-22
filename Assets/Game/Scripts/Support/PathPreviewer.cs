using UnityEngine;

public class PathPreviewer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Transform current = transform.GetChild(i);
            Transform next = transform.GetChild(i + 1);

            Gizmos.DrawLine(current.position, next.position);
            Gizmos.DrawSphere(current.position, 0.2f);
        }

        if (transform.childCount > 0)
        {
            Gizmos.DrawSphere(transform.GetChild(transform.childCount - 1).position, 0.2f);
        }
    }
}