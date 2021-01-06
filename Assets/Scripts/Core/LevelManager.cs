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
        public LevelStatus LevelStatus { get; set; } = LevelStatus.Locked;
    }

    private static LazyValue<LevelData[]> levelStatuses;

    private void Awake()
    {
        levelStatuses = new LazyValue<LevelData[]>(InitializeLevelStatuses);        
    }    

    private static LevelData[] InitializeLevelStatuses()
    {
        LevelData[] statuses = new LevelData[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            LevelData newLevelData = new LevelData();
            statuses.SetValue(newLevelData, i);
        }
        statuses[0].LevelStatus = LevelStatus.Unlocked;
        statuses[1].LevelStatus = LevelStatus.Unlocked;
        return statuses;
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex > levelStatuses.value.Length) { return; }
        print(levelIndex);
        print(levelStatuses.value[levelIndex].LevelStatus);
        print(levelStatuses.value[levelIndex].deathsEasy);
        print(levelStatuses.value[levelIndex].deathsHard);
        if (levelStatuses.value[levelIndex].LevelStatus != LevelStatus.Locked)
        {
            SceneManager.LoadScene(levelIndex);            
        }
    }

    public void SetLevelUnlock(int levelIndex)
    {
        levelStatuses.value[levelIndex].LevelStatus = LevelStatus.Unlocked;
    }

    public void SetLevelCompleted(int levelIndex)
    {
        levelStatuses.value[levelIndex].LevelStatus = LevelStatus.Completed;
    }

    public void SetLevelDeaths(int levelIndex,bool isHard,int levelDeaths)
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
    
    public bool IsLevelLocked(int levelIndex)
    {        
        if (levelIndex >= levelStatuses.value.Length) { return true; }        
        return levelStatuses.value[levelIndex].LevelStatus == LevelStatus.Locked;
    }

    public int GetLevelDeaths(int levelIndex, bool hardDifficulty)
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
        levelStatuses.value = (LevelData[]) state;
    }
}
