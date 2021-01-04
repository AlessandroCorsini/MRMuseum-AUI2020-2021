using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine.Audio;
using System.Linq;

public class GameMechanism : MonoBehaviour
{

    public class Card{
        public string animal;
        public string habitat;
        public string team;
    }

    public TextAsset jsonFile;
    JObject cards;
    IList<Card> allCards = new List<Card>();
    List<string> animalList = new List<string>();
    List<string> habitatList = new List<string>();
    List<string> teamList = new List<string>();
    string selectedAnimal = null;
    string selectedHabitat = null;
    string playingTeam = null;

    public GameObject renderSavana;
    public GameObject renderForestFirst;
    public GameObject renderAntarticFirst;
    public GameObject renderForestSecond;
    public GameObject renderAntarticSecond;
    public GameObject activeRender;

    public GameObject renderSavanaFloor;
    public GameObject renderForestFirstFloor;
    public GameObject renderAntarticFirstFloor;
    public GameObject renderForestSecondFloor;
    public GameObject renderAntarticSecondFloor;
    public GameObject activeRenderFloor;

    public GameObject forestCamera;
    public GameObject antartideCamera;

    public GameObject forestCameraFloor;
    public GameObject antartideCameraFloor;

    public Text teamAscore;
    public Text teamBscore;
    public Text victoryMessage;
    private int scoreA = 0;
    private int scoreB = 0;

    public Text timer;
    float currentTime = 0f;
    public float startingTime = 300.0f;
    bool active = true;

    public List<AudioClip> clips;
    public static AudioSource aud;

    public Image correct;
    public Image wrong;

    public GameObject animalsObjects;
    private GameObject animalToSetActive;
    public GameObject FinalBackground;
    public GameObject FinalFloorBackground;

    public Text error;

    // Start is called before the first frame update
    void Start()
    {
        // leggo il file json e salvo la lista di carte
        cards = JObject.Parse(jsonFile.text);
        IList<JToken> results = cards["cards"].Children().ToList();

        foreach (JToken result in results)
        {
            // JToken.ToObject is a helper method that uses JsonSerializer internally
            Card card = result.ToObject<Card>();
            animalList.Add(card.animal);
            habitatList.Add(card.habitat);
            teamList.Add(card.team);
            allCards.Add(card);
        }

        // rendering settings
        renderForestFirst.SetActive(false);
        renderAntarticFirst.SetActive(false);
        renderForestSecond.SetActive(false);
        renderAntarticSecond.SetActive(false);

        renderForestFirstFloor.SetActive(false);
        renderAntarticFirstFloor.SetActive(false);
        renderForestSecondFloor.SetActive(false);
        renderAntarticSecondFloor.SetActive(false);

        teamAscore.text = scoreA.ToString();
        teamBscore.text = scoreB.ToString();
        victoryMessage.text = "";
        error.text = "";

        currentTime = startingTime;

        aud = GetComponent<AudioSource>();

        correct.canvasRenderer.SetAlpha(0.0f);
        wrong.canvasRenderer.SetAlpha(0.0f);
    }



// Update is called once per frame
void Update()
    {
        int minute = 1;
        int seconds = 1;

        if (active)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CheckMatch("leone");
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                CheckMatch("giraffa");
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                CheckMatch("zebra");
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                CheckMatch("volpe");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckMatch("elefante");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                CheckMatch("cervo");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                CheckMatch("orso polare");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                CheckMatch("lupo");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                CheckMatch("pinguino");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                CheckMatch("orso");
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CheckMatch("savana");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CheckMatch("foresta");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CheckMatch("antartide");
            }

            currentTime -= 1 * Time.deltaTime;
            minute = ((int)currentTime / 60);
            seconds = ((int)currentTime % 60);

            if (currentTime >= 0)
            {
                timer.text = minute + ":" + seconds;
            }
            else
                TimerEnded();
        }

        teamAscore.text = scoreA.ToString();
        teamBscore.text = scoreB.ToString();
    }

    private void CheckMatch(string card)
    {

        IEnumerable<Card> searchedAnimalCard = allCards.Where(x => x.animal == card);
        if (searchedAnimalCard.ToList().Count() != 0) // se ho trovato l'animale
        {
            if(selectedAnimal == null && selectedHabitat == null)
            {
                selectedAnimal = searchedAnimalCard.First().animal;
                PlayAudioClip(selectedAnimal);
                playingTeam = searchedAnimalCard.First().team;
            }
            else if(selectedHabitat != null )
            {
                playingTeam = searchedAnimalCard.First().team;
                PlayAudioClip(searchedAnimalCard.First().animal);

                if (selectedHabitat == searchedAnimalCard.First().habitat)
                {
                    StartCoroutine(StartFeedbackPositive());
                    setAnimalActive(searchedAnimalCard.First().animal);
                    UpdateScore(playingTeam);
                }
                else
                {
                    StartCoroutine(StartFeedbackNegative());
                }

                ResetMatch();
            }
            else
            {
                StartCoroutine(StartErrorDisplay("You cannot scan two animals in a row. \n Please provide an animal and an habitat"));
                ResetMatch();
            }
        }

        IEnumerable<Card> searchedHabitatCard = allCards.Where(x => x.habitat == card);
        if (searchedHabitatCard.ToList().Count() != 0) // se ho trovato l'animale
        {
            if (selectedHabitat == null && selectedAnimal == null)
            {
                selectedHabitat = searchedHabitatCard.First().habitat;
                ScreenFade(selectedHabitat);
                FloorFade(selectedHabitat);
            }
            else if (selectedAnimal != null)
            {
                IEnumerable<Card> matchedCards = searchedHabitatCard.Where(x => x.animal == selectedAnimal);
                if (matchedCards.ToList().Count() == 0)
                    StartCoroutine(StartFeedbackNegative());
                else
                {
                    StartCoroutine(StartFeedbackPositive());
                    setAnimalActive(selectedAnimal);
                    UpdateScore(playingTeam);
                    ScreenFade(searchedHabitatCard.First().habitat);
                    FloorFade(searchedHabitatCard.First().habitat);
                }

                ResetMatch();
            }
            else
            {
                StartCoroutine(StartErrorDisplay("You cannot scan two habitats in a row. \n Please provide an animal and an habitat"));
                ResetMatch();
            }
        }     

    }

    private void ResetMatch()
    {
        selectedHabitat = null;
        selectedAnimal = null;
    }


    // FADING FUNCTION

    public void ScreenFade(string habitat)
    {
        if (habitat == "savana")
        {
            if(activeRender == renderAntarticFirst || activeRender == renderAntarticSecond) // from antartide to savana
            {
                renderAntarticSecond.SetActive(true);
                renderAntarticFirst.SetActive(false);

                activeRender = renderAntarticSecond;
                renderSavana.SetActive(true);
                activeRender.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderAntarticSecond, antartideCamera));
                //activeRender.SetActive(false);
                activeRender = renderSavana;
            }
            else if(activeRender == renderForestFirst || activeRender == renderForestSecond) // from forest to savana
            {
                renderForestSecond.SetActive(true);
                renderForestFirst.SetActive(false);

                activeRender = renderForestSecond;
                renderSavana.SetActive(true);
                activeRender.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderForestSecond, forestCamera));
                //activeRender.SetActive(false);
                activeRender = renderSavana;
            }

        }
        else if(habitat == "foresta")
        {

            forestCamera.SetActive(true);

            if (activeRender == renderSavana) // from savana to forest
            {
                renderForestFirst.SetActive(true);
                activeRender.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderSavana, null));
                activeRender = renderForestFirst;
            }
            if (activeRender == renderAntarticFirst || activeRender == renderAntarticSecond) // from antartide to savana
            {
                renderAntarticSecond.SetActive(true);
                renderAntarticFirst.SetActive(false);

                activeRender = renderAntarticSecond;
                renderForestFirst.SetActive(true);
                activeRender.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderAntarticSecond, antartideCamera));
                //activeRender.SetActive(false);
                activeRender = renderForestFirst;
            }
        }
        else if(habitat == "antartide")
        {
            antartideCamera.SetActive(true);

            if (activeRender == renderSavana) // from savana to forest
            {
                renderAntarticFirst.SetActive(true);
                activeRender.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderSavana, null));
                //activeRender.SetActive(false);
                activeRender = renderAntarticFirst;
            }
            else if (activeRender == renderForestFirst || activeRender == renderForestSecond) // from forest to savana
            {
                renderForestSecond.SetActive(true);
                renderForestFirst.SetActive(false);

                activeRender = renderForestSecond;
                renderAntarticFirst.SetActive(true);
                activeRender.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderForestSecond, forestCamera));
                //activeRender.SetActive(false);
                activeRender = renderAntarticFirst;
            }
        }
    }

    public void FloorFade(string habitat)
    {
        if (habitat == "savana")
        {
            if (activeRenderFloor == renderAntarticFirstFloor || activeRenderFloor == renderAntarticSecondFloor) // from antartide to savana
            {
                renderAntarticSecondFloor.SetActive(true);
                renderAntarticFirstFloor.SetActive(false);

                activeRenderFloor = renderAntarticSecondFloor;
                renderSavanaFloor.SetActive(true);
                activeRenderFloor.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderAntarticSecondFloor, antartideCameraFloor));
                //activeRender.SetActive(false);
                activeRenderFloor = renderSavanaFloor;
            }
            else if (activeRenderFloor == renderForestFirstFloor || activeRenderFloor == renderForestSecondFloor) // from forest to savana
            {
                renderForestSecondFloor.SetActive(true);
                renderForestFirstFloor.SetActive(false);

                activeRenderFloor = renderForestSecondFloor;
                renderSavanaFloor.SetActive(true);
                activeRenderFloor.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderForestSecondFloor, forestCameraFloor));
                //activeRender.SetActive(false);
                activeRenderFloor = renderSavanaFloor;
            }

        }
        else if (habitat == "foresta")
        {

            forestCameraFloor.SetActive(true);

            if (activeRenderFloor == renderSavanaFloor) // from savana to forest
            {
                renderForestFirstFloor.SetActive(true);
                activeRenderFloor.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderSavanaFloor, null));
                activeRenderFloor = renderForestFirstFloor;
            }
            if (activeRenderFloor == renderAntarticFirstFloor || activeRenderFloor == renderAntarticSecondFloor) // from antartide to savana
            {
                renderAntarticSecondFloor.SetActive(true);
                renderAntarticFirstFloor.SetActive(false);

                activeRenderFloor = renderAntarticSecondFloor;
                renderForestFirstFloor.SetActive(true);
                activeRenderFloor.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderAntarticSecondFloor, antartideCameraFloor));
                //activeRender.SetActive(false);
                activeRenderFloor = renderForestFirstFloor;
            }
        }
        else if (habitat == "antartide")
        {
            antartideCameraFloor.SetActive(true);

            if (activeRenderFloor == renderSavanaFloor) // from savana to forest
            {
                renderAntarticFirstFloor.SetActive(true);
                activeRenderFloor.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderSavanaFloor, null));
                //activeRender.SetActive(false);
                activeRenderFloor = renderAntarticFirstFloor;
            }
            else if (activeRenderFloor == renderForestFirstFloor || activeRenderFloor == renderForestSecondFloor) // from forest to savana
            {
                renderForestSecondFloor.SetActive(true);
                renderForestFirstFloor.SetActive(false);

                activeRenderFloor = renderForestSecondFloor;
                renderAntarticFirstFloor.SetActive(true);
                activeRenderFloor.GetComponent<Animation>().Play("CrossFade");

                StartCoroutine(DisableRenderView(renderForestSecondFloor, forestCameraFloor));
                //activeRender.SetActive(false);
                activeRenderFloor = renderAntarticFirstFloor;
            }
        }
    }


    IEnumerator DisableRenderView(GameObject toBeDisabled,GameObject cameraToBeDisabled)
    {
        if (cameraToBeDisabled != null)
            cameraToBeDisabled.SetActive(false);

        yield return new WaitForSeconds(5);

        toBeDisabled.SetActive(false);

    }

    public void UpdateScore(string team)
    {
        if (team == "A")
        {
            scoreA += 1;
            if (scoreA == 5)
                StartCoroutine(DeclareWinner("TEAM A WINS"));
        }
        else if(team == "B")
        {
            scoreB += 1;
            if (scoreB == 5)
                StartCoroutine(DeclareWinner("TEAM B WINS"));
        }
    }

    public IEnumerator DeclareWinner(string message)
    {
        yield return new WaitForSeconds(2.0f);
        FinalBackground.SetActive(true);
        FinalFloorBackground.SetActive(true);
        activeRender.GetComponent<Animation>().Play("CrossFade");
        activeRenderFloor.GetComponent<Animation>().Play("CrossFade");
        victoryMessage.text = message;
        active = false;
    }


    public void PlayAudioClip(string clipToPlay)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == clipToPlay)
            {
                aud.PlayOneShot(clip);
            }
        }
    }

    public void setAnimalActive(string animal)
    {
        Debug.Log(animalsObjects);
        animalToSetActive = animalsObjects.transform.Find(animal).gameObject;
        Debug.Log(animalToSetActive);
        animalToSetActive.SetActive(true);
    }

    public IEnumerator StartFeedbackPositive()
    {
        CorrectSound.PlayCorrectSound();
        correct.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2.0f);
        correct.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator StartErrorDisplay(string text)
    {
        PlayWrongSounds.PlayWrongSound();
        error.text = text;
        error.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2.0f);
        error.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1.0f);
        error.text = "";
    }

    public IEnumerator StartFeedbackNegative()
    {
        PlayWrongSounds.PlayWrongSound();
        wrong.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2.0f);
        wrong.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1.0f);
    }

    public void TimerEnded()
    {
        PlayWrongSounds.PlayWrongSound();
        timer.color = Color.red;
        active = false;

        if (scoreA > scoreB)
            StartCoroutine(DeclareWinner("TEAM A WINS"));
        else if (scoreB > scoreA)
            StartCoroutine(DeclareWinner("TEAM B WINS"));
        else
            StartCoroutine(DeclareWinner("DRAW"));
        ;    }
}
 
