using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAttached : MonoBehaviour
{

    Vector3 initialRealativePosition;

    // Start is called before the first frame update
    void Start()
    {
        initialRealativePosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = initialRealativePosition;
    }
}
