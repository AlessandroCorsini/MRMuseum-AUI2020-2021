using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutGames : MonoBehaviour
{
    public string gameScene;

    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            LevelLoader.startTransition(gameScene);
        }

    }
}
