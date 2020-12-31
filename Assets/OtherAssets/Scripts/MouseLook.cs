using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    private float mouseX;
    private float mouseY;

    // public float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // xRotation -= mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // transform.Rotate(Vector3.up * mouseX);
        // transform.Rotate(Vector3.left * mouseY);
    }

    private void OnMouseDrag()
    {
        transform.Rotate(Vector3.up * -mouseX);
        transform.Rotate(Vector3.left * -mouseY);
    }
}
*/