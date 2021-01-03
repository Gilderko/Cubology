using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPedestal : MonoBehaviour
{
    [SerializeField] int levelIndex = 100;
    [SerializeField] Material levelUnfinishedMaterial;
    [SerializeField] Material levelFinishedMaterial;
    [SerializeField] Text description;
    [SerializeField] Text deathsDescription;
    private bool isLocked = true;

    
    void Start()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        isLocked = levelManager.IsLevelLocked(levelIndex);
        if (!isLocked)
        {
            gameObject.GetComponent<Renderer>().material = levelFinishedMaterial;
            int hardDeaths = levelManager.GetLevelDeaths(levelIndex, true);
            int easyDeaths = levelManager.GetLevelDeaths(levelIndex, false);
            deathsDescription.text = "Easy Ds: " + (easyDeaths == int.MaxValue ? "X" : easyDeaths.ToString())  + "\nHard Ds: " + (hardDeaths == int.MaxValue ? "X" : hardDeaths.ToString());
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
}
