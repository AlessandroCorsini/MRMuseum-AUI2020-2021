using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerManager : MonoBehaviour
{
    private float backgroundVolume;
    // Start is called before the first frame update
    void Start()
    {
        backgroundVolume = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.CompareTag("Readable"))
        {
            GameObject.Find("SuggestionBox").GetComponentInChildren<Text>().text = transform.GetComponent<Text>().text;
        }
        else if (transform.CompareTag("Listenable"))
        {
            GameObject.Find("BackGroundSound").GetComponent<AudioSource>().volume = 0.09f;
            if (!transform.GetComponent<AudioSource>().isPlaying)
            {
                transform.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (transform.CompareTag("Selectable"))
        {
            Debug.Log(transform.name + " entered");
            if (Input.GetMouseButtonDown(0))
            {
                GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

                gameManager.gotSelection = true;
                gameManager.selection = transform.name;
                Debug.Log(transform.name);
            }
        }
        if (transform.CompareTag("Playable"))
        {
            transform.GetComponent<Image>().color = Color.grey;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject.Find("Game Manager").GetComponent<GameManager>().PlayGame();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.CompareTag("Readable"))
        {
            GameObject.Find("SuggestionBox").GetComponentInChildren<Text>().text = "";
        }
        else if (transform.CompareTag("Listenable"))
        {
            GameObject.Find("BackGroundSound").GetComponent<AudioSource>().volume = backgroundVolume;
            transform.GetComponent<AudioSource>().Stop();
        }
        else if (transform.CompareTag("Playable"))
        {
            transform.GetComponent<Image>().color = Color.white;
        }
    }
}
