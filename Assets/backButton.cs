using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButton : MonoBehaviour
{
    private string museumSceneName = "Museum";

    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("buttun pressed");
            LevelLoader.startTransition(museumSceneName);
        }

    }
}
