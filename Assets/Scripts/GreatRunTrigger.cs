using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreatRunTrigger : MonoBehaviour
{    
    [SerializeField] Text deathsDescription;

    private void Start()
    {
        int highestHard = GreatRunManager.GetHighestLevel(true);
        int highestEasy = GreatRunManager.GetHighestLevel(false);

        deathsDescription.text = "Top easy: " + highestEasy + "\nTop Hard: " + highestHard;
    }

    public void StartGreatRun()
    {
        StartCoroutine(FindObjectOfType<GameManager>().LoadLevel(1, 0.7f));
    }
}
