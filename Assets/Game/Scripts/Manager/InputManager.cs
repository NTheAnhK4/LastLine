
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    [SerializeField] private Tower currentSelectedTower = null;
    private RaycastHit2D GetRayCast()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        worldPosition.z = 0;
        return Physics2D.Raycast(worldPosition,Vector2.right,1);
    }
   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D pointer = GetRayCast();
            HandleClickTower(pointer);
        }
        
    }

    private void HandleClickTower(RaycastHit2D pointer)
    {
        if (currentSelectedTower != null)
        {
            currentSelectedTower.HideUI();
            currentSelectedTower = null;
        }

        if (pointer.collider != null)
        {
            
            currentSelectedTower = pointer.collider.GetComponent<Tower>();
            if(currentSelectedTower != null) currentSelectedTower.ShowUI();
        }
    }
}
