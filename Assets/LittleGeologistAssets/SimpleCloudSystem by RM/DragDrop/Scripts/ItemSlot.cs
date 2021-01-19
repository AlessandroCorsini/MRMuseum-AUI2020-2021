using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//IDropHandler
public class ItemSlot : MonoBehaviour {

    public List<GameObject> correctRocks;
    public GameObject slotStart;
    public GameObject[] lavaLevels;
    public GameObject[] rocks;
    public string[] rocksNames;
    public TextMesh nameDisplay;
    public Image positive;
    public Image negative;
    public GameObject canvas;
    GameObject detectedRock;
    public string nextSceneName;
    public static bool start = false;
    public GameObject winningObjects;
    public GameObject gameObjects;

    public void Start()
    {
        positive.canvasRenderer.SetAlpha(0.0f);
        negative.canvasRenderer.SetAlpha(0.0f);
    }

    public void Update()
    {
        if (start)
        {
            start = false;
            nameDisplay.text = rocksNames[0];
        }
    }

    public static void GameStart()
    {
        start = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetChild(0) != null)
        {
            detectedRock = collision.transform.GetChild(0).gameObject;
                Debug.Log(detectedRock.name + " enter");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(detectedRock.name + " stay");
            if (correctRocks.Contains(detectedRock))
            {
                detectedRock.SetActive(false);
                detectedRock.transform.SetParent(null);
                StartCoroutine(StartFeedbackPositive());
            }    
            else
            {
                CameraShaker.GetInstance("Main Camera").ShakeOnce(4f, 4f, .1f, 2f);
                StartCoroutine(StartFeedbackNegative());
                detectedRock.GetComponent<RectTransform>().anchoredPosition = slotStart.GetComponent<RectTransform>().anchoredPosition;
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
            RockTexure.updateCount();
            nameDisplay.text = rocksNames[Counting.rockCounter-1];
        }
        else
        {
            CameraShaker.GetInstance("Main Camera").ShakeOnce(4f, 4f, .1f, 2f);

            StartCoroutine(Eruption());

        }
    }

    public IEnumerator Eruption()
    {
        winningObjects.SetActive(true);
        gameObjects.GetComponent<CanvasGroup>().alpha = 0f;
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.red);
        yield return new WaitForSeconds(3f);
        Debug.Log("transition");
        LevelLoader.startTransition(nextSceneName);
        canvas.SetActive(false);
    }

    public IEnumerator StartFeedbackPositive()
    {
        CorrectSound.PlayCorrectSound();
        positive.CrossFadeAlpha(1, 2, false);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
        yield return new WaitForSeconds(2.0f);
        positive.CrossFadeAlpha(0, 1, false);
        nextLavaLevelOn();
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
        yield return new WaitForSeconds(1.0f);
        nextRock();
    }

    public IEnumerator StartFeedbackNegative()
    {
        PlayWrongSounds.PlayWrongSound();
        negative.CrossFadeAlpha(1, 2, false);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.red);
        yield return new WaitForSeconds(2.0f);
        negative.CrossFadeAlpha(0, 1, false);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
        yield return new WaitForSeconds(1.0f);
    }

}
