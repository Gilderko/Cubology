using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Saving;
using GameDevTV.Utils;

public class LevelManager : MonoBehaviour, ISaveable
{
    [System.Serializable]
    public enum LevelStatus {Locked,Unlocked,Completed};

    [System.Serializable]
    public class LevelData 
    {
        public int deathsEasy { get; set; } = int.MaxValue;
        public int deathsHard { get; set; } = int.MaxValue;
        public LevelStatus levelStatus { get; set; } = LevelStatus.Locked;
    }

    private static LazyValue<LevelData[]> levelStatuses;

    private void Awake()
    {
        levelStatuses = new LazyValue<LevelData[]>(InitializeLevelStatuses);        
    }    

    private LevelData[] InitializeLevelStatuses()
    {
        LevelData[] statuses = new LevelData[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            LevelData newLevelData = new LevelData();
            statuses.SetValue(newLevelData, i);
        }
        statuses[0].levelStatus = LevelStatus.Unlocked;
        statuses[1].levelStatus = LevelStatus.Unlocked;
        return statuses;
    }

    public static void LoadLevel(int levelIndex)
    {
        if (levelIndex > levelStatuses.value.Length) { return; }
        print(levelIndex);
        print(levelStatuses.value[levelIndex].levelStatus);
        print(levelStatuses.value[levelIndex].deathsEasy);
        print(levelStatuses.value[levelIndex].deathsHard);
        if (levelStatuses.value[levelIndex].levelStatus != LevelStatus.Locked)
        {
            SceneManager.LoadScene(levelIndex);            
        }
    }

    public static void SetLevelUnlock(int levelIndex)
    {
        levelStatuses.value[levelIndex].levelStatus = LevelStatus.Unlocked;
    }

    public static void SetLevelCompleted(int levelIndex)
    {
        levelStatuses.value[levelIndex].levelStatus = LevelStatus.Completed;
    }

    public static void SetLevelDeaths(int levelIndex,bool isHard,int levelDeaths)
    {
        LevelData levelData = levelStatuses.value[levelIndex];
        if (isHard && levelDeaths < levelData.deathsHard)
        {
            levelStatuses.value[levelIndex].deathsHard = levelDeaths;
        }
        else if (!isHard && levelDeaths < levelData.deathsEasy)
        {
            levelStatuses.value[levelIndex].deathsEasy = levelDeaths;
        }
    }
    
    public static bool IsLevelLocked(int levelIndex)
    {        
        if (levelIndex >= levelStatuses.value.Length) { return true; }        
        return levelStatuses.value[levelIndex].levelStatus == LevelStatus.Locked;
    }

    public static int GetLevelDeaths(int levelIndex, bool hardDifficulty)
    {
        if (levelIndex >= levelStatuses.value.Length) { return 9999; }        
        return hardDifficulty ? levelStatuses.value[levelIndex].deathsHard : levelStatuses.value[levelIndex].deathsEasy;
    }

    object ISaveable.CaptureState()
    {
        return levelStatuses.value;
    }

    void ISaveable.RestoreState(object state)
    {
        print("Restoring Level Statuses");
        LevelData[] currentStates = (LevelData[])state;
        int currentStateSceneCount = currentStates.Length;
        LevelData[] tempState;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        if (currentStates.Length < totalSceneCount)
        {
            tempState = InitializeLevelStatuses();
            for (int i = 0; i < currentStateSceneCount; i++)
            {
                tempState[i].levelStatus = currentStates[i].levelStatus;
            }
        }
        else if (currentStates.Length > totalSceneCount)
        {
            tempState = InitializeLevelStatuses();
            for (int i = 0; i < totalSceneCount; i++)
            {
                tempState[i].levelStatus = currentStates[i].levelStatus;
            }
        }
        else
        {
            tempState = currentStates;
        }
        levelStatuses.value = tempState;
    }
}
