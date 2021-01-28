using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityIntroduction : MonoBehaviour
{
    public Animator EmiAnimator;
    public string introductionPart1;
    public string introductionPart2;
    public Text textBuble;
    public Animator fadeOutAnimator;
    public float textTime = 0.02f;
    public static string activityName;
    public static bool firstPress = true;
    public static bool start = false;


    private void Update()
    {
        if (start)
        {
            start = false;
            FakeStart();
        }
    }

    public static void setStartIntroduction()
    {
        start = true;
    }

    public void FakeStart()
    {
        StartIntroduction();
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.EndSpeak += StopTalking();
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.yellow);
        textBuble.text = "";
    }

    private void StartIntroduction()
    {
        //Play animation
        EmiAnimator.SetTrigger("talking");

        // activate the text to speech component
        MagicRoomManager.instance.MagicRoomTextToSpeachManager.GenerateAudioFromText(introductionPart1 + activityName + introductionPart2);

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
        foreach (char character in introductionPart1.ToCharArray())
        {
            textBuble.text += character;
            yield return new WaitForSeconds(textTime);
        }

        foreach (char character in activityName.ToCharArray())
        {
            textBuble.text += character;
            yield return new WaitForSeconds(textTime);
        }

        foreach (char character in introductionPart2.ToCharArray())
        {
            textBuble.text += character;
            yield return new WaitForSeconds(textTime);
        }

        BackToIdleEmi();
    }

    public static void SetActivityName(string name)
    {
        activityName = name;
    }

    public static bool GetFirstPress()
    {
        return firstPress;
    }

    public static void SetFirstPress(bool b)
    {
            firstPress = b;
    }

}
