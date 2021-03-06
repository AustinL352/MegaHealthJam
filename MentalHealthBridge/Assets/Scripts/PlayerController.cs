﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool breathe;
    public bool beach;

    public AnxietyController anxiety;
    public EarthQuakeScene quakeController;
    public FinalScene finalTrigger;

    public Slider slider;
    public Animator anim;

    Vector3 velo;

    float horizontalRot = 0;
    float verticalRot = 0;
    float zRot = 0;

    float upDownRange = 45.0f;
    public float speed = 150;
    float cameraSpeed = 5;

    float breathTime = 1;
    float walkTime = 1;

    bool PlayedRight = false;
    bool PlayedLeft = false;


    CharacterController characterController;
    public AudioSource footStep;

    void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
        horizontalRot = 90;
        AudioSource footStep = new AudioSource();
    }

    void Update()
    {
        if (anxiety.GameStart)
        {
            if (!breathe && !beach)
                velo = ((transform.right * Input.GetAxis("HorizontalButton")) + (transform.forward * Input.GetAxis("VerticalButton"))) * speed;
            else
                velo = Vector3.zero;
            velo.y = 0;

            //verticalVelocity += Physics.gravity.y * Time.deltaTime;
            //velo.y = verticalVelocity;

            if (Input.GetButton("VerticalButton") && !breathe && !beach)
            {
                anim.SetBool("Walking", true);
                if (walkTime > 0.3f)
                {
                    zRot += (Time.deltaTime * 3);
                    if (!PlayedRight)
                    {
                        footStep.Play();
                        PlayedRight = true;
                        PlayedLeft = false;
                    }
                }
                else if (walkTime >= -0.4)
                {
                    zRot -= (Time.deltaTime * 3);
                    if (!PlayedLeft)
                    {
                        footStep.Play();
                        PlayedRight = false;
                        PlayedLeft = true;
                    }
                }

                walkTime -= (Time.deltaTime);

                if (walkTime < -0.4F)
                    walkTime = 1;
            }
            else if (zRot != 0)
            {
               anim.SetBool("Walking", false);
                if (zRot < 0)
                    zRot += (Time.deltaTime * 3);
                else if (zRot > 0)
                    zRot -= (Time.deltaTime * 3);
            }

            horizontalRot += Input.GetAxis("Mouse X") * cameraSpeed;
            verticalRot -= Input.GetAxis("Mouse Y") * cameraSpeed;
            verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange - 20);
            transform.localRotation = Quaternion.Euler(verticalRot, horizontalRot, zRot);

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

            slider.value = anxiety.anxietyLevel;

            if (anxiety.anxietyLevel >= 0.50f)
                anim.SetBool("Anxiety", true);
            else
                anim.SetBool("Anxiety", false);

            Vector3 clamp = new Vector3(transform.position.x, 5, transform.position.z);
            transform.position = clamp;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Quake")
        {
            quakeController.enabled = true;
            other.enabled = false;
        }
        else if(other.tag == "FinalTrigger")
        {
            finalTrigger.enabled = true;
        }
        else if(other.tag == "Auditorium")
        {
            RenderSettings.ambientIntensity = .15f;
            other.enabled = false;
        }
    }
}
