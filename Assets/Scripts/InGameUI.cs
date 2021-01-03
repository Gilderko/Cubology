using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {

	public void Restart()
    {
        StartCoroutine(FindObjectOfType<GameManager>().RestartLevel());
    }
    public void Menu()
    {
        StartCoroutine(FindObjectOfType<GameManager>().LoadLevel(0,0.5f));
    }    
}

