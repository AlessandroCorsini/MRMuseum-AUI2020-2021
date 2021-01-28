using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public MyTextWriter myTextWriter;
    //SUGGESTIONS
    public GameObject SuggestionsSEAGULL;
    public GameObject SuggestionsEAGLE;
    public GameObject SuggestionsGREAT_HORNED_OWL;
    public GameObject SuggestionsCROW;
    public GameObject SuggestionsPIGEON;
    public GameObject SuggestionsSPARROW;
    private List<GameObject> Suggestions;

    //CAMERAS
    public Camera mainCamera;
    public Camera floorCamera;


    //PARAMETERS
    public bool gotSelection;
    public string selection;
    public float timeRemaining; //todo change in a real timer
    public string round;
    private List<string> rounds = new List<string> { "SEAGULL", "EAGLE", "GREAT_HORNED_OWL", "CROW", "PIGEON", "SPARROW" };
    private Transform selectionTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool play;
    //private bool end;
    private bool endgame;
    private bool change;
    private GameObject lastSuggested;


    //REFERENCES TO OTHER OBJECTS
    public GameObject particles;
    public GameObject SuggestionBox;
    public GameObject GameCanvas;
    public Animator Emi_Animator;


    // Start is called before the first frame update
    void Start()
    {
        InitializeValue();
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            return;
        }

        GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.SetActive(false);
        particles.SetActive(false);

        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.white);
        MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "OFF");


        if (play && !endgame)
        {
            GameBody();
        }
        else if(play && endgame)
        {
            GameEnd();
        }
    }

    private void InitializeValue()
    {
        selectionTransform = null;
        selection = "";
        gotSelection = false;
        lastSuggested = null;
        round = null;
        change = true;
        endgame = false;
        //end = false;
        play = false;
        timeRemaining = 0;
        Suggestions = new List<GameObject> { SuggestionsSEAGULL, SuggestionsEAGLE, SuggestionsGREAT_HORNED_OWL, SuggestionsCROW,
        SuggestionsPIGEON, SuggestionsSPARROW};
        SuggestionsSEAGULL.SetActive(false);
        SuggestionsEAGLE.SetActive(false);
        SuggestionsGREAT_HORNED_OWL.SetActive(false);
        SuggestionsCROW.SetActive(false);
        SuggestionsPIGEON.SetActive(false);
        SuggestionsSPARROW.SetActive(false);
        particles.SetActive(false);
        SuggestionBox.SetActive(true);
        GameCanvas.SetActive(true);
    }

    private void GameStart()
    {
        GameCanvas.transform.GetChild(0).gameObject.SetActive(true);
        GameCanvas.transform.GetChild(1).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.SetActive(false);
        Emi_Animator.SetBool("Speak", true);
        myTextWriter.AddWriter(
            GameCanvas.GetComponentInChildren<Text>(),
            "Are you ready to play the CATCH THE BIG BIRDS game?\nPress the START button" +
            " and let the experience begin!",
            .1f
            );
    }

    public void PlayGame()
    {
        GameCanvas.transform.GetChild(0).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.SetActive(true);
        GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        play = true;
    }

    private void GameBody()
    {
        doRandomization();

        if (gotSelection)
        {
            if (selection.Equals(round))
            {
                selectionTransform = GameObject.Find(selection).transform;
                originalPosition = selectionTransform.position;
                originalRotation = selectionTransform.rotation;

                selectionTransform.position =
                    new Vector3(
                    particles.transform.position.x,
                    particles.transform.position.y,
                    particles.transform.position.z);
                selectionTransform.rotation = new Quaternion(0, 1, 0, 0);
                if (!selection.Equals("PIGEON"))
                {
                    selectionTransform.localScale /= 2;
                }
                particles.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(4).GetComponent<Animator>().SetTrigger("start");
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "Great Choice!";
                change = true;
                MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
                timeRemaining = 3;
                lastSuggested.SetActive(false);
                if (rounds.ToArray().GetLength(0) == 0)
                    endgame = true;
            }
            else
            {
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(3).GetComponent<Animator>().SetTrigger("start");
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameCanvas.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "You can do better!";
                MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.red);
                timeRemaining = 3;
            }
        }
        gotSelection = false;
    }

    private void doRandomization()
    {
        if (change)
        {
            if (selectionTransform != null)
            {
                selectionTransform.position = new Vector3(
                    originalPosition.x,
                    originalPosition.y,
                    originalPosition.z);
                selectionTransform.rotation = originalRotation;
                if (!selection.Equals("PIGEON"))
                {
                    selectionTransform.localScale *= 2;
                }
            }

            int max = rounds.ToArray().GetLength(0);
            Debug.Log(max);
            int nextRound = Random.Range(0, max);
            round = rounds[nextRound];
            Debug.Log("Round: " + round);

            //change suggestion
            lastSuggested = Suggestions[nextRound];
            lastSuggested.SetActive(true);

            Suggestions.RemoveAt(nextRound);
            rounds.RemoveAt(nextRound);

            change = false;
        }
    }

    private void GameEnd()
    {
        if (selectionTransform != null)
        {
            selectionTransform = null;
        }
        SuggestionBox.SetActive(false);
        GameCanvas.transform.GetChild(2).gameObject.SetActive(false);
        GameCanvas.transform.GetChild(1).gameObject.SetActive(true);
        myTextWriter.AddWriter(
            GameCanvas.GetComponentInChildren<Text>(),
            "CONGRATULATIONS!\nNOW YOU ARE A MASTER OF THE BIG BIRDS!",
            .1f
            );
        GameCanvas.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("Speak", true);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
        MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "ON");
        timeRemaining = 5;
        //end = true;
        play = false;
    }

    private void OnApplicationQuit()
    {
        Saving.Reset();
    }
}
