using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] bool moveRight = true;
    [SerializeField] bool moveLeft = false;
    [SerializeField] float force = 8;
    Rigidbody rb;    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            rb.AddForce(force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        else if (moveLeft)
        {
            rb.AddForce(-force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "BorderLeft")
        {
            moveRight = true;
            moveLeft = false;
        }

        if (collisionInfo.tag == "BorderRight")
        {
            moveRight = false;
            moveLeft = true;
        }
    }
}
