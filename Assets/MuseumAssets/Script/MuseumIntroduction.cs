using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumIntroduction : MonoBehaviour
{
    public Animator EmiAnimator;
    public string introduction;
    public Text textBuble;
    public Animator fadeOutAnimator;
    public static bool buttonPressed = false;
    public float textTime = 0.02f;


    public void Start()
    {
        StartIntroduction();
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.yellow);
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.EndSpeak += StopTalking();
    }

    public void Update()
    {
        if (buttonPressed)
        {
            buttonPressed = false;
            startGame();
        }
    }

    private void StartIntroduction()
    {
        //Play animation
        EmiAnimator.SetTrigger("talking");

        // activate the text to speech component
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.GenerateAudioFromText(introduction);

        // starting to write text in Emi dialog bubble
        textBuble.text = "";
        StartCoroutine(EffectTypeWriter());
    }

    public System.Action StopTalking()
    {
        return BackToIdleEmi;
    }

    public void BackToIdleEmi()
    {
        //stopping Emi from talking
        EmiAnimator.SetTrigger("idle");
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
    }

    private IEnumerator EffectTypeWriter()
    {
        foreach (char character in introduction.ToCharArray())
        {
            textBuble.text += character;
            yield return new WaitForSeconds(textTime);
        }

        BackToIdleEmi();
    }

    public void startGame()
    {
        StartCoroutine(fadeOutEmi());
    }

    private IEnumerator fadeOutEmi()
    {
        fadeOutAnimator.SetTrigger("start");

        yield return new WaitForSeconds(1f);
    }

    public static void StartGame()
    {
        buttonPressed = true;
    }
}
