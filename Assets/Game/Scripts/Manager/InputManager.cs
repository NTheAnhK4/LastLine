
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputManager : Singleton<InputManager>
{
    
    
    public Vector2 minLimits = new Vector2(0, -5); 
    public Vector2 maxLimits = new Vector2(0, 3.75f);

    private Vector3 dragOrigin;
    [SerializeField] private Tower currentSelectedTower = null;
    [SerializeField] private Camera mainCamera;

    public Camera MainCamera
    {
        get
        {
            if(mainCamera == null) mainCamera = GameObject.FindObjectOfType<Camera>();
            return mainCamera;
        }
    }

    private string currentSceneName;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    private void OnEnable()
    {
        LoadComponent();
        UpdateScene();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void LoadComponent()
    {
        if (mainCamera == null) mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    private void Reset()
    {
        LoadComponent();
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        UpdateScene();
    }
    private void UpdateScene()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "WorldMap")
        {
            minLimits = Vector2.zero;
            maxLimits = Vector2.zero;
        }
        
    }
    private RaycastHit2D GetRayCast(LayerMask layerMask)
    {
        Vector3 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        worldPosition.z = 0;
       
       
        return Physics2D.Raycast(worldPosition,Vector2.right,0.5f, layerMask);
       
    }
   

    private void Update()
    {
        if(currentSceneName == "InGame") HandleInputInGame();
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) 
        {
            Vector3 difference = dragOrigin - MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = MainCamera.transform.position + difference;

            
            newPosition.x = Mathf.Clamp(newPosition.x, minLimits.x, maxLimits.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minLimits.y, maxLimits.y);

            MainCamera.transform.position = newPosition;
        }
        
    }

    #region InGame

    private void HandleInputInGame()
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
        
        }
        

    }

    #endregion

    public void SetLimitScene(int currentLevel)
    {
        LevelParam levelParam = DataManager.Instance.GetData<LevelData>().Levels[currentLevel];
        minLimits = levelParam.MinLimitCamera;
        maxLimits = levelParam.MaxLimitCamera;
    }
    
   
}
