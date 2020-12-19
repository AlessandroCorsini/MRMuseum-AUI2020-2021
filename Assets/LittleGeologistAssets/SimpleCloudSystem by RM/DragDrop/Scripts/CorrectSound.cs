using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSound : MonoBehaviour
{
    public static AudioSource correctSound;

    // Start is called before the first frame update
    void Start()
    {
        correctSound = GetComponent<AudioSource>();
    }

    public static void PlayCorrectSound()
    {
        correctSound.Play();
    }
}
