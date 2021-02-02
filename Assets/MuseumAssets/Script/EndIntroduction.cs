using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndIntroduction : MonoBehaviour
{
    public Animator EmiAnimator;
    public string introduction;
    public Text textBuble;
    public float textTime = 0.02f;


    public void Start()
    {
        //Play animation
        EmiAnimator.SetTrigger("talking");

        // activate the text to speech component
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.GenerateAudioFromText(introduction);

        // starting to write text in Emi dialog bubble
        textBuble.text = "";
        StartCoroutine(EffectTypeWriter());

        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.yellow);
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.EndSpeak += StopTalking();

    }

    public System.Action StopTalking()
    {
        return BackToIdleEmi;
    }

    public void BackToIdleEmi()
    {
        //stopping Emi from talking
        EmiAnimator.SetTrigger("idle");

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

    private void OnApplicationQuit()
    {
        Saving.Reset();
    }
}
