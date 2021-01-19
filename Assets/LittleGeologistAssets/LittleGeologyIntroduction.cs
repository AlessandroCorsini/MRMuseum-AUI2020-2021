using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleGeologyIntroduction : MonoBehaviour
{
    public Animator EmiAnimator;
    public GameObject hand;
    public string introduction;
    public Text textBuble;
    public Animator fadeOutAnimator;
    public static bool buttonPressed = false;
    public float textTime = 0.02f;

    public void Start()
    { 
        StartIntroduction();
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.red);
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

        //disabling hand object to prevent fault
        hand.SetActive(false);

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
        // activating hand again
        hand.SetActive(true);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);

        //stopping Emi from talking
        EmiAnimator.SetTrigger("idle");
    }

    private IEnumerator EffectTypeWriter()
    {
        foreach(char character in introduction.ToCharArray())
        {
            textBuble.text += character;
            yield return new WaitForSeconds(textTime);
        }

        BackToIdleEmi();
    }

    public void startGame()
    {
        StartCoroutine(fadeOutEmi());
        ItemSlot.GameStart();
        DragDrop.GameStart();
        RockTexure.GameStart();
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
