using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {

    [SerializeField] GameObject mainMenuIcon;
    [SerializeField] GameObject restartIcon;
    [SerializeField] GameObject giveUpIcon;

    private void Start()
    {
        if (GreatRunManager.isGreatRunOn)
        {
            mainMenuIcon.SetActive(false);
            restartIcon.SetActive(false);
            giveUpIcon.SetActive(true);
        }
    }

    void Update()
    {        
        if (Application.platform == RuntimePlatform.Android)
        {            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(FindObjectOfType<GameManager>().LoadLevel(0, 0.5f));
            }
        }
    }

    public void Restart()
    {
        StartCoroutine(FindObjectOfType<GameManager>().RestartLevel());
    }
    public void Menu()
    {
        StartCoroutine(FindObjectOfType<GameManager>().LoadLevel(0,0.5f));
    }   
}

