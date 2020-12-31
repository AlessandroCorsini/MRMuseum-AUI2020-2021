using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    // The target we are following
    public Transform frontCamera;
    public Camera floorCamera;
    public Camera frontCameraa;

    private int joypadLayerMask;

    private Vector3 lastRotation;

    void Start()
    {
        joypadLayerMask = LayerMask.GetMask("Joypad");
        Debug.Log("joypad layer mask: " + joypadLayerMask);
        //joypadLayerMask = 1 << joypadLayerMask;
        Debug.Log("joypad layer mask: " + joypadLayerMask);

        /*
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Debug.Log("More than one display");
            // Display.displays[i].Activate();
        }
        if (Display.displays.Length > 1)
        {
        }
        */

        lastRotation = frontCamera.eulerAngles;
    }
    void Update()
    {
         if (!frontCamera)
        {
            Debug.LogError("The target is null, assign a target to the FollowCamera");
            return;
        }

        createRaycast();

        Vector3 frontRotation = frontCamera.transform.eulerAngles;
        
        float deltaRotation = lastRotation.y - frontRotation.y;
        lastRotation = frontRotation;

        Vector3 oldRotation = transform.eulerAngles;

        transform.eulerAngles = new Vector3(oldRotation.x, oldRotation.y, oldRotation.z + deltaRotation);

    }

    private void createRaycast()
    {
        Ray ray = floorCamera.ScreenPointToRay(Input.mousePosition);
        // Ray ray = new Ray(floorCamera.transform.position, Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            /*
            Debug.Log("camera position:" + floorCamera.transform.position);
            Debug.Log("mouse position: " + Input.mousePosition);
            Debug.Log("floor screen to point: " + floorCamera.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("floor screen to point: " + frontCameraa.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("ray origin: " + ray.origin);
            Debug.Log("ray direction: " + ray.direction);
            */
            if (Physics.Raycast(ray, out hit, 100, joypadLayerMask, QueryTriggerInteraction.Collide))
            {
                ArrowManager.setRaycasting(hit.collider.name);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit, 100, joypadLayerMask, QueryTriggerInteraction.Collide))
            {
                ArrowManager.clearRaycasting();
            }
        }
        
    }
}