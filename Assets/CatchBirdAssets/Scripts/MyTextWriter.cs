using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTextWriter : MonoBehaviour
{

    private Text uiText;
    private string textToWrite;
    private int characterindex;
    private float timePerCharacter;
    private float timer;


    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter)
    {
        characterindex = 0;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.uiText = uiText;
        timer = 0f;
    }

    private void Update()
    {
        if(uiText != null)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                timer += timePerCharacter;
                characterindex++;
                uiText.text = textToWrite.Substring(0, characterindex);
            }
            if(textToWrite.Length <= characterindex)
            {
                uiText = null;
                return;
            }
        }
    }

}
