using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGeologyIntroduction : MonoBehaviour
{
    public Animator EmiAnimator;
    public GameObject hand;
    public float transitionTime = 1f;
    public string introduction;

    public void Start()
    {
        StartIntroduction();
    }

    public void Update()
    {
          
    }


    private void StartIntroduction()
    {
        StartCoroutine(AnimateEmi());
    }

    IEnumerator AnimateEmi()
    {
        //Play animation
        EmiAnimator.SetTrigger("talking");

        //disabling hand object to prevent fault
        hand.SetActive(false);

        Debug.Log(MagicRoomManager.instance);
        Debug.Log(MagicRoomManager.instance.MagicRoomTextToSpeachManager);
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.GenerateAudioFromText(introduction);

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // activating hand again
        hand.SetActive(true);

        //stopping Emi from talking
        EmiAnimator.SetTrigger("idle");
    }
}
