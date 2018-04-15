using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool breathe;
    public bool beach;

    public AnxietyController anxiety;
    public GameObject sliderObject;

    Vector3 velo;

    float horizontalRot = 0;
    float verticalRot = 0;
    float zRot = 0;

    float upDownRange = 45.0f;
    float speed = 150;
    float cameraSpeed = 5;

    float breathTime = 1;
    float walkTime = 1;


    CharacterController characterController;
    public GameObject playerBase;

    void Start ()
    {
        characterController = playerBase.GetComponent<CharacterController>();
        Slider slider = sliderObject.GetComponent<Slider>();
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
            Debug.Log("VericalButton");
            if (walkTime > 0.3f)
                zRot += (Time.deltaTime * 3);
            else if (walkTime >= -0.4f)
                zRot -= (Time.deltaTime * 3);

            walkTime -= (Time.deltaTime);

            if (walkTime < -0.4f)
                walkTime = 1;
        }
        else if(zRot != 0)
        {
            if (zRot < 0)
                zRot += (Time.deltaTime * 3);
            else if (zRot > 0)
                zRot -= (Time.deltaTime * 3);
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
