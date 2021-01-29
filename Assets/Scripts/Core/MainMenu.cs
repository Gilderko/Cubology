using System;
using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static Material playerMaterial;
    public static Mesh playerMesh = null;       
    [SerializeField] GameObject menu;
    [SerializeField] Mesh[] meshOptions;
    LevelManager levelManager;

    public event Action OnSkinChange;
    public event Action OnShapeChange;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }        

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void DeleteSaveFile()
    {
        FindObjectOfType<SavingWrapper>().DeleteLevelInfo();
    }

    public void BacktoLevelSelection()
    {        
        menu.GetComponent<Animation>().Play("ToggleOffMenu");
        menu.GetComponent<CanvasGroup>().interactable = false;
        StartCoroutine(ToggleOfMenu());
    }

    private IEnumerator ToggleOfMenu()
    {
        yield return new WaitForSeconds(0.5f);
        print("seting false");
        FindObjectOfType<MoveCameraTouch>().blockingRaycast = false;
        menu.SetActive(false);
    }

    public void ChangeSkinnCactus()
    {
        playerMaterial = (Material)Resources.Load("Cactus", typeof(Material));
        OnSkinChange();
    }
    public void ChangeSkinnPlayer()
    {
        playerMaterial = (Material)Resources.Load("Player", typeof(Material));
        OnSkinChange();
    }
    public void ChangeSkinnCoin()
    {
        playerMaterial = (Material)Resources.Load("Coin", typeof(Material));
        OnSkinChange();
    }
    public void ChangeSkinDirt()
    {
        playerMaterial = (Material)Resources.Load("Dirt", typeof(Material));
        OnSkinChange();
    }
    public void ChangeSkinnRock()
    {
        playerMaterial = (Material)Resources.Load("Rock", typeof(Material));
        OnSkinChange();
    }
    public void ChangeSkinnWall()
    {
        playerMaterial = (Material)Resources.Load("Wall", typeof(Material));
        OnSkinChange();
    }
    public void ChangeShapeCube()
    {
        playerMesh = meshOptions[1];
        GameManager.isGameHard = true;
        OnShapeChange();
    }
    public void ChangeShapeSphere()
    {
        playerMesh = meshOptions[0];
        GameManager.isGameHard = false;
        OnShapeChange();
    }
}
