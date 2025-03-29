using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : Singleton<DataManager>
{
    private Dictionary<Type, ScriptableObject> gameData = new Dictionary<Type, ScriptableObject>();
    public bool IsDataLoaded { get; private set; } = false;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadData("Game Data");
    }

    public void LoadData(string label, Action onComplete = null)
    {
        Addressables.LoadAssetsAsync<ScriptableObject>(label, obj =>
        {
            gameData[obj.GetType()] = obj;
        }).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Load data successful");
                IsDataLoaded = true;
                onComplete?.Invoke();
            }
            else
            {
                Debug.LogError($"Failed to load data with label '{label}'");
            }
        };
    }

    public T GetData<T>() where T : ScriptableObject
    {
        if (!IsDataLoaded)
        {
            Debug.LogWarning("Data has not been loaded yet!");
            return null;
        }

        if (gameData.TryGetValue(typeof(T), out ScriptableObject data))
        {
            return data as T;
        }
        Debug.LogError($"Cannot find data '{typeof(T).Name}' !");
        return null;
    }
}