using System;

using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BindableValue<T>
{
    [SerializeField] private T m_Value;
    public event Action<T,T> OnValueChanged;

    public BindableValue(T initialValue = default)
    {
        m_Value = initialValue;
    }
    public T Value
    {
        get => m_Value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(m_Value,value))
            {
                T oldValue = m_Value;
                m_Value = value;
                OnValueChanged?.Invoke(oldValue, m_Value);
            }
            
        }
    }
    

    
}

