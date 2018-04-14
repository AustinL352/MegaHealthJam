using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool breathe;
    public bool beach;
    public AnxietyController anxiety;

    float horizontalRot = 0;
    float verticalRot = 0;
    float upDownRange = 45.0f;
    float speed = 250;
    float cameraSpeed = 5;

    float breathTime = 1;

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

        horizontalRot += Input.GetAxis("Mouse X") * cameraSpeed;
        verticalRot -= Input.GetAxis("Mouse Y") * cameraSpeed;
        verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange - 20);
        playerBase.transform.localRotation = Quaternion.Euler(verticalRot, horizontalRot, 0);

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
