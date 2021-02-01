using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReduceSpeed : MonoBehaviour
{
    [SerializeField] float speedFw = 100;
    [SerializeField] float speedSw = 100;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -5), ForceMode.Impulse);
            other.gameObject.GetComponent<Moving>().forwardspeed = speedFw;
            other.gameObject.GetComponent<Moving>().sidewaysSpeed = speedSw;
            Debug.Log("hit");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Moving>().ResetSpeed();
            Debug.Log("out");
        }
    }
}
