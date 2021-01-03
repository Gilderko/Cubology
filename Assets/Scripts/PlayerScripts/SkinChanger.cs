using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SkinChanger : MonoBehaviour {
 
    public static Material material;
    MainMenu mainMenu;

    void Start()
    {        
        ChangeSkin();
        mainMenu = FindObjectOfType<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.OnSkinChange += ChangeSkin;
        }
    }

    public void ChangeSkin()
    {
        if (MainMenu.playerMaterial == null) { return; }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            material = MainMenu.playerMaterial;
        }
        gameObject.GetComponent<Renderer>().material = material;
    }  
}
