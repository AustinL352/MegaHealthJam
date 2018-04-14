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

    CharacterController characterController;
    public GameObject playerBase;

    void Start ()
    {
        characterController = playerBase.GetComponent<CharacterController>();
    }

	void Update ()
    {
        Vector3 velo = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        velo.Normalize();

        velo = ((transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"))) * speed;
        velo.y = 0;

        //verticalVelocity += Physics.gravity.y * Time.deltaTime;
        //velo.y = verticalVelocity;

        characterController.Move(velo * Time.deltaTime);

        horizontalRot += Input.GetAxis("Mouse X") * cameraSpeed;
        verticalRot -= Input.GetAxis("Mouse Y") * cameraSpeed;
        verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange - 20);
        playerBase.transform.localRotation = Quaternion.Euler(verticalRot, horizontalRot, 0);
    }
}
