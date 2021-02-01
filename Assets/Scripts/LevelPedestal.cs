using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPedestal : MonoBehaviour
{
    [SerializeField] string tooManyDeathsQuote;
    [SerializeField] int levelIndex = 100;
    [SerializeField] Material levelUnfinishedMaterial;
    [SerializeField] Material levelFinishedMaterial;
    [SerializeField] Text description;
    [SerializeField] Text deathsDescription;

    private bool isLocked = true;
    
    void Start()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        isLocked = LevelManager.IsLevelLocked(levelIndex);
        if (!isLocked)
        {
            gameObject.GetComponent<Renderer>().material = levelFinishedMaterial;
            int hardDeaths = LevelManager.GetLevelDeaths(levelIndex, true);
            int easyDeaths = LevelManager.GetLevelDeaths(levelIndex, false);
            int actualEasyDeaths = easyDeaths == int.MaxValue ? -1 : easyDeaths;
            int actualHardDeaths = hardDeaths == int.MaxValue ? -1 : hardDeaths;
            if (actualEasyDeaths >= 5 || actualHardDeaths >= 5)
            {
                deathsDescription.text = tooManyDeathsQuote;
            }
            else
            {
                string displayEasyDeaths = (easyDeaths == int.MaxValue) ? "X" : easyDeaths.ToString();
                string displayHardDeaths = (hardDeaths == int.MaxValue) ? "X" : hardDeaths.ToString();
                deathsDescription.text = "Easy Deaths: " + displayEasyDeaths + "\nHardDeaths: " + displayHardDeaths;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = levelUnfinishedMaterial;
            deathsDescription.text = "Easy Deaths: X\nHardDeaths: x";
        }
        description.text = "Level " + levelIndex;        
    }

    public void LoadSelectedLevel()
    {
        if (!isLocked)
        {
            StartCoroutine(FindObjectOfType<GameManager>().LoadLevel(levelIndex,0.7f));
        }        
    } 

    public bool IsLocked()
    {
        return isLocked;
    }
}
