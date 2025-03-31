using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopScripts : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    public Vector2 minLimits = new Vector2(0, -5); 
    public Vector2 maxLimits = new Vector2(0, 3.75f);

    private Vector3 dragOrigin;
    [SerializeField] private Camera mainCamera;

    public Camera MainCamera
    {
        get
        {
            if(mainCamera == null) mainCamera = GameObject.FindObjectOfType<Camera>();
            return mainCamera;
        }
    }

    private float PosX = -10;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            GameObject signal =  PoolingManager.Spawn(Prefab,new Vector3(PosX, 0, 0),default,transform);
            PosX += 2.5f;
            signal.transform.localScale = Vector3.one;
            
            //signal.transform.Find("SignalWay").GetComponent<RectTransform>().position = pos;
        }
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // Khi kéo chuột
        {
            Vector3 difference = dragOrigin - MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = MainCamera.transform.position + difference;
            
            // Giới hạn phạm vi di chuyển của Camera
            newPosition.x = Mathf.Clamp(newPosition.x, minLimits.x, maxLimits.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minLimits.y, maxLimits.y);

            MainCamera.transform.position = newPosition;
        }
    }
}
