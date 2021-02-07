using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouseCanvas : MonoBehaviour
{
    /*
     * previous code
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
    */

    Image spriteRenderer;
    public Sprite grab;
    public Sprite normal;

    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = grab;
        }
        else
        {
            spriteRenderer.sprite = normal;
        }
    }
}
