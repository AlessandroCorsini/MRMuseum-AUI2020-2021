using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            MuseumIntroduction.StartGame();
        }

    }
}
