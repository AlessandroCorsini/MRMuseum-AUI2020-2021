using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;

public class ItemSlot : MonoBehaviour, IDropHandler {

    public List<GameObject> correctRocks;
    public GameObject slotStart;
    public GameObject[] lavaLevels;
    public GameObject[] rocks;
    public string[] rocksNames;
    public TextMesh nameDisplay;
    public static bool isInASlot;

    public void Start()
    {
        nameDisplay.text = rocksNames[0];
        isInASlot = false;
    }
    

    public void OnDrop(PointerEventData eventData) {

        if (eventData.pointerDrag != null) {

            isInASlot = true;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if(correctRocks.Contains(eventData.pointerDrag.gameObject))
            {
                eventData.pointerDrag.gameObject.SetActive(false);
                nextLavaLevelOn();
                nextRock();

            }
            else
            {
                CameraShaker.GetInstance("Main Camera").ShakeOnce(4f, 4f, .1f, 2f);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = slotStart.GetComponent<RectTransform>().anchoredPosition;
                isInASlot = false;
            }

        }
    }

    private void nextLavaLevelOn()
    {
        for (int i = 0; i < 6 ; i++)
        {
            if (!lavaLevels[i].activeInHierarchy)
            {
                lavaLevels[i].SetActive(true);
                return;
            }
        }
    }

    private void nextRock()
    {
        if(Counting.rockCounter < Counting.max)
        {
            rocks[Counting.rockCounter].SetActive(true);
            Counting.updateCounter();
            nameDisplay.text = rocksNames[Counting.rockCounter-1];
        }
    }

}
