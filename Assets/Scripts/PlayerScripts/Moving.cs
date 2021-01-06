using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    private Rigidbody rb;
    public float forwardspeed = 1200;
    private float ScreenWidth;
    public float sidewaysSpeed;

    void Start()
    {       
        ScreenWidth = Screen.width;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        rb.AddForce(0, 0, forwardspeed * Time.deltaTime, ForceMode.Force);
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > (ScreenWidth / 2))
            {
                RunCharacter(sidewaysSpeed);
            }
            if (Input.GetTouch(i).position.x < (ScreenWidth / 2))
            {
                RunCharacter(-sidewaysSpeed);
            }
            i++;
        }
        
        /*
        if (Input.GetKey(KeyCode.D))
        {
            RunCharacter(sidewaysSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            RunCharacter(-sidewaysSpeed);
        }
        */
    }

    private void RunCharacter(float horizontalInput)
    {        
        Vector3 force = new Vector3(horizontalInput,0, 0);
        rb.AddForce(force * Time.deltaTime, ForceMode.Force);
    }
}
