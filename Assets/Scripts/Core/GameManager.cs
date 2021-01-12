using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Saving;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject completeLevelUI;
    [SerializeField] GameObject player;            
    public float delay = 1f;
    public static bool isGameHard = false;    
    public static int deathCount;    
    

    Fader screenFader;    
    bool gameHasEnded = false;       

    public IEnumerator Start()
    {
        screenFader = FindObjectOfType<Fader>();        
        print("GameManagerStart");
        screenFader.FadeInImmediate();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<MoveCameraTouch>().blockingRaycast = true;
            yield return screenFader.FadeOut(1f);
            deathCount = 0;
            FindObjectOfType<MoveCameraTouch>().blockingRaycast = false;
        }
        else
        {            
            player = GameObject.FindWithTag("Player");            
            player.GetComponent<Moving>().enabled = false;
            yield return screenFader.FadeOut(1.2f);
            yield return new WaitForSeconds(0.3f);
            player.GetComponent<Moving>().enabled = true;
        }        
    }    

    public IEnumerator CompleteLevel()
    {
        gameHasEnded = true;
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        LevelManager.SetLevelUnlock(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.SetLevelCompleted(SceneManager.GetActiveScene().buildIndex);
        LevelManager.SetLevelDeaths(SceneManager.GetActiveScene().buildIndex, isGameHard, deathCount);
        deathCount = 0;
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        if (GreatRunManager.isGreatRunOn && GreatRunManager.GetHighestLevel(isGameHard) < levelIndex)
        {
            GreatRunManager.SetHighestLevel(isGameHard, levelIndex);
        }

        FindObjectOfType<SavingWrapper>().SaveData();
        GameObject.FindWithTag("InGameMenu").SetActive(false);
        completeLevelUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LevelManager.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadLevel(int levelIndex, float fadetime)
    {
        gameHasEnded = true;
        if (SceneManager.GetActiveScene().buildIndex != 0) { GameObject.FindWithTag("InGameMenu").SetActive(false); }        
        yield return screenFader.FadeIn(fadetime);
        LevelManager.LoadLevel(levelIndex);        
    }

	public IEnumerator RestartLevel()
    {
        print("restarting");
        bool isGreatRunOn = GreatRunManager.isGreatRunOn;
        if (gameHasEnded == false) 
        {
            gameHasEnded = true;            
            deathCount = deathCount + 1;            
            
            GameObject.FindWithTag("InGameMenu").SetActive(false);
            yield return screenFader.FadeIn(0.7f);
            if (GreatRunManager.isGreatRunOn)
            {
                GreatRunManager.isGreatRunOn = false;
                LevelManager.LoadLevel(0);
            }
            else
            {
                LevelManager.LoadLevel(SceneManager.GetActiveScene().buildIndex);
            }
        }        
    }  
}

