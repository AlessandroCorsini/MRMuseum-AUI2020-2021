using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisible : MonoBehaviour
{
    public Texture2D cursorHand;

    void Start()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        
    }
}
