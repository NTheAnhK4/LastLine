using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using UnityEditor.Search;
using UnityEngine;

public class DevelopScripts : MonoBehaviour
{
    public int Count = 1;
    private List<float> DifficultRate;

    private void Calc()
    {
        DifficultRate = new List<float>();
        float total = 0;
        for (int i = 0; i < Count; ++i)
        {
            float value = Mathf.Pow(i + 1, 2);
            DifficultRate.Add(value);
            total += value;
        }

        for (int i = 0; i < Count; ++i)
        {
            DifficultRate[i] /= total;
        }
        for(int i = 0; i < Count; ++i) Debug.Log(DifficultRate[i]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Calc();
    }
}
