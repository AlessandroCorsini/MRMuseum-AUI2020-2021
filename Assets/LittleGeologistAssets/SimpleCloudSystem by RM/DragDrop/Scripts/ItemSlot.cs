using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemSlot : MonoBehaviour, IDropHandler {

    public List<GameObject> correctRocks;
    public GameObject slotStart;
    public GameObject[] lavaLevels;
    public GameObject[] rocks;
    public string[] rocksNames;
    public TextMesh nameDisplay;
    public Image positive;
    public Image negative;
    public GameObject canvas;


    public void Start()
    {
        nameDisplay.text = rocksNames[0];
        positive.canvasRenderer.SetAlpha(0.0f);
        negative.canvasRenderer.SetAlpha(0.0f);
    }
    

    public void OnDrop(PointerEventData eventData) {

        if (eventData.pointerDrag != null) {

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if(correctRocks.Contains(eventData.pointerDrag.gameObject))
            {
                eventData.pointerDrag.gameObject.SetActive(false);
                StartCoroutine(StartFeedbackPositive());
            }
            else
            {
                CameraShaker.GetInstance("Main Camera").ShakeOnce(4f, 4f, .1f, 2f);
                StartCoroutine(StartFeedbackNegative());
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = slotStart.GetComponent<RectTransform>().anchoredPosition;
            }

        }
    }

    private void nextLavaLevelOn()
    {
        int i = 0;

        for (i = 0; i < 6 ; i++)
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
        else
        {
            LevelLoader.Eruption();
            canvas.SetActive(false);

        }
    }

    public IEnumerator StartFeedbackPositive()
    {
        CorrectSound.PlayCorrectSound();
        positive.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2.0f);
        positive.CrossFadeAlpha(0, 1, false);
        nextLavaLevelOn();
        yield return new WaitForSeconds(1.0f);
        nextRock();
    }

    public IEnumerator StartFeedbackNegative()
    {
        PlayWrongSounds.PlayWrongSound();
        negative.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2.0f);
        negative.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1.0f);
    }

}
