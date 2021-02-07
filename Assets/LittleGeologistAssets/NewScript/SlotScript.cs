using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    public string identifier;
    public bool isRock;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0) && isRock)
        {
            RockGameManager.SendSlotIdentifier(identifier);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isRock = RockScript.taken;
    }

    private void OnTriggerExit(Collider other)
    {
        isRock = false;
    }
}
