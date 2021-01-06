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
        screenFader = GameObject.FindObjectOfType<Fader>();
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
        //screenFader.FadeOutImmediate();
    }    

    public IEnumerator CompleteLevel()
    {
        gameHasEnded = true;
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.SetLevelUnlock(SceneManager.GetActiveScene().buildIndex + 1);
        levelManager.SetLevelCompleted(SceneManager.GetActiveScene().buildIndex);
        levelManager.SetLevelDeaths(SceneManager.GetActiveScene().buildIndex, isGameHard, deathCount);
        deathCount = 0;

        FindObjectOfType<SavingSystem>().Save("levelInfoData");
        GameObject.FindWithTag("InGameMenu").SetActive(false);
        completeLevelUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadLevel(int levelIndex, float fadetime)
    {
        gameHasEnded = true;
        if (SceneManager.GetActiveScene().buildIndex != 0) { GameObject.FindWithTag("InGameMenu").SetActive(false); }        
        yield return screenFader.FadeIn(fadetime);
        FindObjectOfType<LevelManager>().LoadLevel(levelIndex);        
    }

	public IEnumerator RestartLevel()
    {
        print("restarting");
        if (gameHasEnded == false) 
        {
            deathCount = deathCount + 1;            
            gameHasEnded = true;
            GameObject.FindWithTag("InGameMenu").SetActive(false);
            yield return screenFader.FadeIn(0.7f);            
            FindObjectOfType<LevelManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }  
}

