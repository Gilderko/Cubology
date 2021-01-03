using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTrigger : MonoBehaviour {

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject ingameMenu;
    public int level;
    public int deaths;

    public void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        deaths = GameManager.deathCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            StartCoroutine(gameManager.CompleteLevel());
        }        
    }    
}

