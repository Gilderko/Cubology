using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class SavingWrapper : MonoBehaviour
{
    [SerializeField] SavingSystem savingSystem;
    string levelInfoSaveFile = "levelInfoData";    

    private void Awake()
    {
        savingSystem.Load(levelInfoSaveFile);        
    }

    public void DeleteLevelInfo()
    {
        savingSystem.Delete(levelInfoSaveFile);
    }

    public void SaveData()
    {
        savingSystem.Save(levelInfoSaveFile);
    }

    public void LoadData()
    {
        savingSystem.Load(levelInfoSaveFile);
    }
}
