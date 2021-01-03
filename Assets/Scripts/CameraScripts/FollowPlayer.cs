using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    Vector3 distanceVector;
    public Transform player;

    private void Start()
    {
        distanceVector = player.position - transform.position;        
    }

    void Update () {
        transform.position = player.position - distanceVector;		
	}
}
