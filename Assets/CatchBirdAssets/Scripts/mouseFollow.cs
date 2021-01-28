using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollow : MonoBehaviour
{

    public Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Camera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Mathf.Abs(Camera.transform.position.z - transform.position.z)));
        
        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}
