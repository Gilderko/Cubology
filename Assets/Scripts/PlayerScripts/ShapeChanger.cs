using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapeChanger : MonoBehaviour
{    
    public static Mesh playerMesh;    
    MainMenu mainMenu;

    void Start()
    {
        ChangeShape();
        mainMenu = FindObjectOfType<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.OnShapeChange += ChangeShape;
        }
    } 

    private void ChangeShape()
    {
        if (MainMenu.playerMesh == null) { return; }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            playerMesh = MainMenu.playerMesh;
        }
        GetComponent<MeshFilter>().mesh = playerMesh;
        AdjustPlayerValues(gameObject);        
    }

    private static void AdjustPlayerValues(GameObject gameObject)
    {
        if (playerMesh.name.Contains("Cube"))
        {
            gameObject.GetComponent<Transform>().localScale = SceneManager.GetActiveScene().buildIndex != 0 ? new Vector3(1f, 1f, 1f) : new Vector3(1.5f, 1.5f, 1.5f);
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<Moving>().forwardspeed = 1050f;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;            
        }
        if (playerMesh.name.Contains("Sphere"))
        {
            gameObject.GetComponent<Transform>().localScale = SceneManager.GetActiveScene().buildIndex != 0 ? new Vector3(1.1f, 1.1f, 1.1f) : new Vector3(1.5f, 1.5f, 1.5f); ;
            gameObject.GetComponent<SphereCollider>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Moving>().forwardspeed = 900f;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
