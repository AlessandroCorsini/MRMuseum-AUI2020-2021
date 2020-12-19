using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWrongSounds : MonoBehaviour
{
    public static AudioSource wrongSound;

    // Start is called before the first frame update
    void Start()
    {
        wrongSound = GetComponent<AudioSource>();
    }

    public static void PlayWrongSound()
    {
        wrongSound.Play();
    }
}
