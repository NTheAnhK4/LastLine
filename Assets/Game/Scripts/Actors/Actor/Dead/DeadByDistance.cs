
using UnityEngine;

public class DeadByDistance : DeadHandler
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float disLimit = 25;
    [SerializeField] private float distance;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (mainCamera == null) mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    protected override void UpdateLogic()
    {
        distance = Vector3.Distance(actor.position, mainCamera.transform.position);
    }

    protected override bool CanDespawn()
    {
        return distance >= disLimit;
    }
}
