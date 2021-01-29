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
    [SerializeField] int multipleX;
    [SerializeField] int multipleY;
    public bool blockingRaycast = false;


    private const int defVal = 1000000;
    public static float xPosition = defVal;
    public static float zPosition = defVal;

    private void Awake()
    {
        float currentY = transform.position.y;  
        if (xPosition != defVal)
        {
            transform.position = new Vector3(xPosition, currentY, zPosition);
        }               
    }

    void Update()
    { 
        if (blockingRaycast) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(mouseRay, out hit);
            LevelPedestal hitPedestal = hit.transform.GetComponent<LevelPedestal>();
            GreatRunTrigger greatRunTrigger = hit.transform.GetComponent<GreatRunTrigger>();
            if (hitPedestal != null && !hitPedestal.IsLocked())
            {                
                blockingRaycast = true;
                hitPedestal.LoadSelectedLevel();
                xPosition = transform.position.x;
                zPosition = transform.position.z;
            }
            if (hit.transform.tag == "MenuStarter")
            {
                blockingRaycast = true;
                mainMenu.GetComponent<CanvasGroup>().interactable = true;
                mainMenu.SetActive(true);                
            }
            if (greatRunTrigger != null)
            {
                blockingRaycast = true;
                GreatRunManager.isGreatRunOn = true;
                greatRunTrigger.StartGreatRun();
                xPosition = transform.position.x;
                zPosition = transform.position.z;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 potentionCameraPosition = Camera.main.transform.position - new Vector3(direction.x, 0f, direction.y);
            float addX = (potentionCameraPosition.x < cameraMinX || potentionCameraPosition.x > cameraMaxX) ? 0 : direction.x;
            float addZ = (potentionCameraPosition.z < cameraMinZ || potentionCameraPosition.z > cameraMaxZ) ? 0 : direction.y;            
            Camera.main.transform.position -= new Vector3(addX * multipleX * Time.deltaTime, 0f, addZ * multipleY * Time.deltaTime);
        }
        
    }
}
