using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTouch : MonoBehaviour
{
    Vector3 touchStart;
    [SerializeField] int cameraMaxX = -20;
    [SerializeField] int cameraMinX = -125;
    [SerializeField] int cameraMaxZ = -7;
    [SerializeField] int cameraMinZ = -80;
    [SerializeField] GameObject mainMenu;
    public bool blockingRaycast = false;

    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(mouseRay, out hit);
            LevelPedestal hitPedestal = hit.transform.GetComponent<LevelPedestal>();
            if (hitPedestal != null && !blockingRaycast)
            {
                blockingRaycast = true;
                hitPedestal.LoadSelectedLevel();
            }
            if (hit.transform.tag == "MenuStarter" && !blockingRaycast)
            {
                blockingRaycast = true;
                mainMenu.SetActive(true);                
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 potentionCameraPosition = Camera.main.transform.position - new Vector3(direction.x, 0f, direction.y);
            float addX = !(potentionCameraPosition.x < cameraMinX || potentionCameraPosition.x > cameraMaxX) ? direction.x : 0;
            float addZ = !(potentionCameraPosition.z < cameraMinZ || potentionCameraPosition.z > cameraMaxZ) ? direction.y : 0;            
            Camera.main.transform.position -= new Vector3(addX, 0f, addZ);
        }
        
    }
}
