using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float cameraspeed;
    void FixedUpdate()
    {
        transform.Rotate(0, Time.deltaTime * cameraspeed, 0);  
    }
}
