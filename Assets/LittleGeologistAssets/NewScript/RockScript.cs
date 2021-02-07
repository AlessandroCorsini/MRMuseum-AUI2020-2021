using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScript : MonoBehaviour
{
    public static bool taken = false;
    public GameObject slotStart;


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(0))
        {
            taken = true;
            transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            taken = false;
            GetComponent<RectTransform>().anchoredPosition = slotStart.GetComponent<RectTransform>().anchoredPosition;
        }
    }

}
