using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transitionFloor;
    public float transitionTime = 1f;
    public static bool startAnimation = false;
    public static string nextSceneName;

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
        transitionFloor.SetTrigger("start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(nextSceneName); 
    }

    public static void startTransition(string sceneName)
    {
        startAnimation = true;
        nextSceneName = sceneName;
    }
}
