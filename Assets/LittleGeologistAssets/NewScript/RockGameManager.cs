using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class RockGameManager : MonoBehaviour
{
    public string[] rockNames;
    public List<string> rockSedimentary;
    public List<string> rockIgneous;
    public List<string> rockMetamorphic;

    public Sprite[] rockSprites;
    public Image rock;

    public static string slotId = null;
    public GameObject slotStart;

    public int round = 0;
    public int maxRounds = 5;
    public GameObject[] lavaLevels;

    public GameObject winningObjects;
    public TextMesh nameDisplay;
    public Image positive;
    public Image negative;
    public GameObject canvas;
    public string nextSceneName;
    public GameObject objectOfTheGame;

    public bool start = false;


    public void Start()
    {
        positive.canvasRenderer.SetAlpha(0.0f);
        negative.canvasRenderer.SetAlpha(0.0f);
        nameDisplay.text = rockNames[round];
    }

    // Update is called once per frame
    void Update()
    {
        if (slotId != null)
        {

            if (slotId == "sed" && rockSedimentary.Contains(rockNames[round])) // se è dentro Sedimentary ed è la risposta corretta
            {
                // start feedbackPositive e aggiornamenti livelli
                slotId = null; //riazzero
                StartCoroutine(StartFeedbackPositive());

            }
            else if (slotId == "ign" && rockIgneous.Contains(rockNames[round])) // se è dentro Igneous ed è la risposta corretta
            {
                // start feedbackPositive e aggiornamenti livelli
                slotId = null; //riazzero
                StartCoroutine(StartFeedbackPositive());
            }
            else if (slotId == "met" && rockMetamorphic.Contains(rockNames[round])) // se è dentro Metamorphic ed è la risposta corretta
            {
                // start feedbackPositive e aggiornamenti livelli
                slotId = null; //riazzero
                StartCoroutine(StartFeedbackPositive());
            }
            else // sbagliato
            {
                slotId = null; //riazzero
                CameraShaker.GetInstance("Main Camera").ShakeOnce(4f, 4f, .1f, 2f);
                StartCoroutine(StartFeedbackNegative());
                //detectedRock.GetComponent<RectTransform>().anchoredPosition = slotStart.GetComponent<RectTransform>().anchoredPosition;
            }

            slotId = null; //riazzero

        }
    }

    private void updateRound()
    {
        rock.gameObject.SetActive(true);
        nextLavaLevelOn(); // aggiorna qui la lava
        nextRock();
    }

    public static void SendSlotIdentifier(string slot)
    {
        slotId = slot;
    }
        
    private void nextLavaLevelOn()
    {
        int i = 0;

        for (i = 0; i < 6; i++)
        {
            if (!lavaLevels[i].activeInHierarchy)
            {
                lavaLevels[i].SetActive(true);
                return;
            }
        }

    }

    // DA RIFARE GESTIONE ROCCE
    private void nextRock()
    {
        if (round < maxRounds)
        {
            round += 1; // da controllare
            rock.sprite = rockSprites[round];
            RockTexure.updateCount(); // inalterato
            nameDisplay.text = rockNames[round]; 
        }
    }

    public IEnumerator Eruption()
    {
        objectOfTheGame.GetComponent<CanvasGroup>().alpha = 0f;
        winningObjects.SetActive(true);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.red);
        MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "ON");
        yield return new WaitForSeconds(5f);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
        MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "OFF");
        Debug.Log(nextSceneName);
        LevelLoader.startTransition(nextSceneName);
        canvas.SetActive(false);
    }

    public IEnumerator StartFeedbackPositive()
    {
        rockDisappear();
        CorrectSound.PlayCorrectSound();
        positive.CrossFadeAlpha(1, 2, false);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
        yield return new WaitForSeconds(2.0f);
        positive.CrossFadeAlpha(0, 1, false);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
        yield return new WaitForSeconds(1.0f);

        if (round <maxRounds)
            updateRound();
        else
        {
            nextLavaLevelOn();
            StartCoroutine(Eruption());
        }
    }

    public void rockDisappear()
    {
        rock.gameObject.SetActive(false);
        nameDisplay.text = "";
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

    private void OnApplicationQuit()
    {
        Saving.Reset();
    }
}
