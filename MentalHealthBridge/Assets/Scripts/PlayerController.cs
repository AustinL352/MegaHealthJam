using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool breathe;
    public bool beach;
    public AnxietyController anxiety;

    Vector3 velo;

    float horizontalRot = 0;
    float verticalRot = 0;
    float zRot = 0;

    float upDownRange = 45.0f;
    float speed = 250;
    float cameraSpeed = 5;

    float breathTime = 1;
    float walkTime = 1;


    CharacterController characterController;
    public GameObject playerBase;

    void Start ()
    {
        characterController = playerBase.GetComponent<CharacterController>();
        horizontalRot = 90;
    }

	void Update ()
    {
        if (!breathe)
            velo = ((transform.right * Input.GetAxis("HorizontalButton")) + (transform.forward * Input.GetAxis("VerticalButton"))) * speed;
        velo.y = 0;

        //verticalVelocity += Physics.gravity.y * Time.deltaTime;
        //velo.y = verticalVelocity;

        if (Input.GetButton("VerticalButton") )
        {
            if (walkTime > 0)
                zRot += (Time.deltaTime * 5);
            else if (walkTime >= -1)
                zRot -= (Time.deltaTime * 5);

            walkTime -= (Time.deltaTime);

            if (walkTime < -1)
                walkTime = 1;
        }
        else if(zRot != 0)
        {
            if (zRot < 0)
                zRot += (Time.deltaTime * 5);
            else if (zRot > 0)
                zRot -= (Time.deltaTime * 5);
        }

        horizontalRot += Input.GetAxis("Mouse X") * cameraSpeed;
        verticalRot -= Input.GetAxis("Mouse Y") * cameraSpeed;
        verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange - 20);
        playerBase.transform.localRotation = Quaternion.Euler(verticalRot, horizontalRot, zRot);

        if (breathe)
        {
            //Translate
            if (breathTime > 0)
            {
                velo.y += Time.deltaTime * 1000.0f;
                breathTime -= Time.deltaTime;
            }
            else if (breathTime >= -1)
            {
                velo.y -= Time.deltaTime * 1000.0f;
                breathTime -= Time.deltaTime;
            }
        }
        else
            breathTime = 1;

        characterController.Move(velo * Time.deltaTime);
    }
}
