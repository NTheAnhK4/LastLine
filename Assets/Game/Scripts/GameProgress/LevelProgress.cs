using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class LevelProgress
{
    public List<LevelState> LevelStates;
    public void UpdateLevelState(int levelIndex, int stars)
    {
        if (LevelStates == null) LevelStates = new List<LevelState>();

        int index = LevelStates.FindIndex(level => level.LevelIndex == levelIndex);
    
         if (index == -1) LevelStates.Add(new LevelState() { LevelIndex = levelIndex, Stars = stars });
         else if (index < LevelStates.Count) LevelStates[index].Stars = Math.Max(LevelStates[index].Stars, stars);
    
        JsonSaveSystem.SaveData(this);
    }

}