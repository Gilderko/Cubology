using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBouncing : MonoBehaviour
{    
    public int jumpForce;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "WaterGround")
        {            
            gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }
}
