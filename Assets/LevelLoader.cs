using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public static bool startAnimation = false;

    public void Update()
    {
        if (startAnimation)
        {
            startAnimation = false;
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator  LoadLevel()
    {
        //Play animation
        transition.SetTrigger("start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene("VolcanoExplosion"); 
    }

    public static void Eruption()
    {
        startAnimation = true;
    }
}
