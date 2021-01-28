using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorMovement : MonoBehaviour
{
    public float movementSpeed = 0.5f;
    public float rotationSpeed = 1f;

    private float rotationRegulator = 2000f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizMovement = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float vertMovement = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        if(vertMovement != 0f)
        {
            anim.SetBool("Walk", true);
            transform.Translate(0f, 0f, vertMovement);
        } else
        {
            anim.SetBool("Walk", false);
        }

        if(horizMovement != 0f)
        {
            Vector3 rotation = new Vector3(0f, horizMovement * rotationSpeed * rotationRegulator, 0f);
            transform.Rotate(rotation);
        }
    }
}
