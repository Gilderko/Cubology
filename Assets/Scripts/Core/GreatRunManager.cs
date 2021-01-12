using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Utils;
using Saving;

public class GreatRunManager : MonoBehaviour, ISaveable
{
    [System.Serializable]
    public class GreatRunData
    {
        public int highestLevelEasy { get; set; } = 0;
        public int highestLevelHard { get; set; } = 0;       
    }

    private static LazyValue<GreatRunData> runData = new LazyValue<GreatRunData>(InitializeGreatRunData);
    public static bool isGreatRunOn = false;
    
    public static void SetHighestLevel(bool hardMode, int highestLevelReached)
    {
        if (hardMode)
        {
            runData.value.highestLevelHard = highestLevelReached;
        }
        else
        {
            runData.value.highestLevelEasy = highestLevelReached;
        }        
    }

    public static int GetHighestLevel(bool hardMode)
    {
        if (hardMode)
        {
            return runData.value.highestLevelHard;
        }
        else
        {
            return runData.value.highestLevelEasy;
        }
    }    

    private static GreatRunData InitializeGreatRunData()
    {
        GreatRunData data = new GreatRunData();
        return data;
    }
    
    object ISaveable.CaptureState()
    {
        print("capturing GreatRun");
        print(runData.value.highestLevelEasy);
        Dictionary<bool, int> saveData = new Dictionary<bool, int>();
        saveData.Add(true, runData.value.highestLevelHard);
        saveData.Add(false, runData.value.highestLevelEasy);
        return saveData;
    }

    void ISaveable.RestoreState(object state)
    {
        Dictionary<bool, int> savedData = (Dictionary<bool, int>) state;
        runData.value.highestLevelEasy = savedData[false];
        runData.value.highestLevelHard = savedData[true];
    }
    
}
