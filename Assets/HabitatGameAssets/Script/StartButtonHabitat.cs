using UnityEngine;
using System.Collections;

public class StartButtonHabitat : MonoBehaviour
{
    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            HabitatIntroduction.StartGame();
        }

    }
}
