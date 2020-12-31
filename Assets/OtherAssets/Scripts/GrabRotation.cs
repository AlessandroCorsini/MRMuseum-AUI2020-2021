using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotation : MonoBehaviour
{
    private Vector3 previousPosition;

    public float speedRotation = 1f;
    public float minimumAngle = -40f;
    public float maximumAngle = 40f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition =
                transform.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction =
                previousPosition - transform.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
            
            Vector3 oldRotation = transform.eulerAngles;

            float xRotation = oldRotation.x + -direction.y * speedRotation * 180;
            float yRotation = oldRotation.y + direction.x * speedRotation * 180;

            // xRotation is an angle expressed in range [0f, 360f]
            // it is necessary to normalize it in the case in which the number i high
            if(xRotation > 180f)
            {
                xRotation -= 360f;
            }

            xRotation = Mathf.Clamp(xRotation, minimumAngle, maximumAngle);

            transform.eulerAngles = new Vector3(xRotation, yRotation, 0); 
            
            previousPosition = transform.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);

        }
    }
}

