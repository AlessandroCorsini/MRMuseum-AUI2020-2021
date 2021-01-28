using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ciao");
            LevelLoader.startTransition("EndScene");
        }

    }
}
