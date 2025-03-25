
using System.Collections.Generic;
using UnityEngine;



public class InputManager : Singleton<InputManager>
{
    [SerializeField] private Tower currentSelectedTower = null;
    
    private RaycastHit2D GetRayCast(LayerMask layerMask)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        worldPosition.z = 0;
       
       
        return Physics2D.Raycast(worldPosition,Vector2.right,0.5f, layerMask);
       
    }
   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClickTower();
            HandleClickGuardianPlace();
        }
        
    }

    private void HandleClickTower()
    {
        int towerLayer = LayerMask.NameToLayer("Tower");
        int defaultLayer = LayerMask.NameToLayer("Default");
        int guardianPlaceLayer = LayerMask.NameToLayer("GuardianPlace");

       
        RaycastHit2D guardianHit = GetRayCast(1 << guardianPlaceLayer);
        if (guardianHit.collider != null) return;

       
        RaycastHit2D pointer = GetRayCast((1 << towerLayer) | (1 << defaultLayer));
        if (pointer.collider != null)
        {
            Tower towerSelected = pointer.collider.GetComponent<Tower>();
            if (towerSelected != null && towerSelected == currentSelectedTower) return;
        }

        if (currentSelectedTower != null)
        {
            currentSelectedTower.HideUI();
            currentSelectedTower = null;
        }

        if (pointer.collider != null)
        {
            currentSelectedTower = pointer.collider.GetComponent<Tower>();
            if (currentSelectedTower != null) currentSelectedTower.ShowUI();
        }
    }

    private void HandleClickGuardianPlace()
    {
        int guardianPlaceLayer = LayerMask.NameToLayer("GuardianPlace");
        RaycastHit2D pointer = GetRayCast((1 << guardianPlaceLayer));
        if(pointer.collider == null) return;
        else
        {
            if (currentSelectedTower == null)
            {
                Debug.LogWarning("Tower selected is null");
                return;
            }

            if (currentSelectedTower is SummonTower summonTower)
            {
                summonTower.SetNewFlag(pointer.point);
            }
            else
            {
                Debug.LogWarning("Type of tower is wrong");
                return;
            }
        }

    }
    
}
