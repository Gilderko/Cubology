using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeedOnImpact : MonoBehaviour
{
    public float speed;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Moving>().forwardspeed = speed;
            Debug.Log("hit");
        }
    }

}
