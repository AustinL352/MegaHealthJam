using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool breathe;
    float horizontalRot = 0;
    float verticalRot = 0;
    float upDownRange = 45.0f;
    float speed = 250;
    float cameraSpeed = 5;

    float breathTime;
    float upTimer = 1;
    float downTimer = 1;

    CharacterController characterController;
    public GameObject playerBase;

    void Start ()
    {
        characterController = playerBase.GetComponent<CharacterController>();
        horizontalRot = 90;
    }

	void Update ()
    {
        Vector3 velo = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        velo.Normalize();

        if (!breathe)
            velo = ((transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"))) * speed;
        velo.y = 0;

        //verticalVelocity += Physics.gravity.y * Time.deltaTime;
        //velo.y = verticalVelocity;

        characterController.Move(velo * Time.deltaTime);

        horizontalRot += Input.GetAxis("Mouse X") * cameraSpeed;
        verticalRot -= Input.GetAxis("Mouse Y") * cameraSpeed;
        verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange - 20);
        playerBase.transform.localRotation = Quaternion.Euler(verticalRot, horizontalRot, 0);

        if (breathe)
        {
            //Translate
            while (upTimer >= 0)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * 10), transform.position.z);
                transform.position = newPos;
                upTimer -= Time.deltaTime;
            }

            while (downTimer >= 0)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 10), transform.position.z);
                transform.position = newPos;
                downTimer -= Time.deltaTime;
            }
            breathTime = 0;
        }
    }
}
