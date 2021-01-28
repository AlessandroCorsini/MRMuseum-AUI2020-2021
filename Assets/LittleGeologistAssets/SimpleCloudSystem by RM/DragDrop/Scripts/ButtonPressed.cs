using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{

    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            LittleGeologyIntroduction.StartGame();
        }

    }

}
