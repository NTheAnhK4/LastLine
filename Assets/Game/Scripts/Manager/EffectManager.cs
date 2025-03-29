
using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public enum EffectID
{
    PhysicalShield
}

public class EffectManager : Singleton<EffectManager>
{ 
    [SerializeField] private List<EffectSource> m_EffectSources;
    private const string m_AddressEffect = "Assets/Game/Prefabs/Effect";
   
    public static GameObject PlayEffect(EffectID effectID, Vector3 position, Transform parent = null)
    {
        GameObject effectPrefab = Instance.m_EffectSources[(int)effectID].EffectPrefab;
        if (effectPrefab == null)
        {
            Debug.LogWarning("Effect " + effectID.ToString() + " is null");
            return null;
        }

        return PoolingManager.Spawn(effectPrefab, position, Quaternion.identity, parent);

    }

    

    private void LoadData()
    {
        string[] names = Enum.GetNames(typeof(EffectID));
        if (m_EffectSources == null) m_EffectSources = new List<EffectSource>();
       
        while(m_EffectSources.Count > names.Length) m_EffectSources.RemoveAt(m_EffectSources.Count - 1);
        for (int i = 0; i < names.Length; ++i)
        {
            if (i < m_EffectSources.Count) m_EffectSources[i].Name = names[i];
            else m_EffectSources.Add(new EffectSource() { Name = names[i] });

            m_EffectSources[i].LoadEffectPrefab(m_AddressEffect);
        }
    }

    private void Reset()
    {
        LoadData();
    }

    private void OnValidate()
    {
        LoadData();
    }

   
}

[Serializable]
public class EffectSource
{
    [HideInInspector] public string Name;
    [SerializeField] private GameObject m_EffectPrefab;
    public GameObject EffectPrefab
    {
        get => m_EffectPrefab;
    }

    public void LoadEffectPrefab(string folderPath)
    {
        string prefabPath = $"{folderPath}/{Name}.prefab";
        m_EffectPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (m_EffectPrefab == null)
        {
            Debug.LogWarning($"Can't find prefab: {prefabPath}");
        }
    }
}
