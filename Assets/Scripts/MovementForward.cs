using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForward : MonoBehaviour {
    
    [SerializeField] int speed;
    [SerializeField] int activationDistance;

    Rigidbody rb;
    Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate () {             
        if (player.position.z > activationDistance)
        {
            rb.useGravity = true;
            Vector3 speedVector = new Vector3(0, 0, speed);
            rb.AddForce(speedVector * Time.deltaTime, ForceMode.VelocityChange);
        }        
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Ground")
        {

        }
        
        else if(collisionInfo.gameObject.tag == "Player")
        {
            
        }

        else
        {
            Destroy(collisionInfo.gameObject);
        }
    }
}
