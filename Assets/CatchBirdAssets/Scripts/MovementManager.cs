using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizMovement = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float vertMovement = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        if (vertMovement != 0 || horizMovement != 0)
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.Translate(horizMovement, 0f, 0f);
            transform.Translate(0f, 0f, vertMovement);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
    }
}