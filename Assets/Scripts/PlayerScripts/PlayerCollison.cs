using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollison : MonoBehaviour {

    public Moving movement;
    private Transform playerTransform;    

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }


    private void Update()
    {
        float positionY = gameObject.GetComponent<Transform>().position.y;
        float positionX = gameObject.GetComponent<Transform>().position.x;
        if (positionY < (-3) || positionX < -15 || positionX > 15)
        {
            StartCoroutine(FindObjectOfType<GameManager>().RestartLevel());
        }
    }
        
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            StartCoroutine(FindObjectOfType<GameManager>().RestartLevel());
        }        
    }
}
