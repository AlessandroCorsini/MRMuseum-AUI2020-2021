using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explainations : MonoBehaviour
{
    bool showed = false;

    public Camera wallCamera;
    public GameObject textObject;

    void Start()
    {
        textObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = wallCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (hit.transform == transform)
                    textObject.SetActive(true);
                // To make the objects disapear
                //else
                //    textObject.SetActive(false);

            }
        }
       
    }
}
