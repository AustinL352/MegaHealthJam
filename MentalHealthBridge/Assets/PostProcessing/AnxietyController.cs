using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class AnxietyController : MonoBehaviour {

    VignetteModel.Settings vSettings;
    public PostProcessingProfile postProcess;
    public PlayerController playerController;
    public Image blinkColor;
    public RawImage beach;

    public RectTransform blinkPosition;
    public AudioSource heartbeat;
    public AudioSource ambience;
    public AudioSource crowd;


    
    public float anxietyLevel = 0;
    float breatheTimer = 0;
    float beachTimer = 0;


    // Use this for initialization
    void Start () {
        vSettings = postProcess.vignette.settings;
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;


    }
	
	// Update is called once per frame
	void Update () {
        anxietyLevel += Time.deltaTime / 180.0f;

        if(playerController.breathe)
        {
            breatheTimer += Time.deltaTime;
            Color tmp = blinkColor.color;
            tmp.a = Mathf.Sin(breatheTimer * 1.75f) ;
            blinkColor.color = tmp;
        }

        if (playerController.beach)
        {
            Color tmp = new Color();
            if (beachTimer > 8)
            {
                beachTimer += Time.deltaTime;
                tmp = blinkColor.color;
                tmp.a = 1 - Mathf.Lerp(0, 1, beachTimer - 8);
                blinkColor.color = tmp;
            }
            else
            {
                beachTimer += Time.deltaTime;
                tmp = blinkColor.color;
                tmp.a = Mathf.Lerp(0, 1, beachTimer);
                blinkColor.color = tmp;
            }

            if (beachTimer > 7)
            {
                tmp = beach.color;
                tmp.a = 1 - Mathf.Lerp(0, 1, (beachTimer - 7));
                beach.color = tmp;
            }
            else
            {
                tmp = beach.color;
                tmp.a = Mathf.Lerp(0, 1, (beachTimer - 1) / 5);
                beach.color = tmp;
            }

        }

        if (beachTimer > 9)
        {
            beachTimer = 0;
            playerController.beach = false;
            Color tmp = blinkColor.color;
            tmp.a = 0;
            blinkColor.color = tmp;
            tmp = beach.color;
            tmp.a = 0;
            beach.color = tmp;
        }

        if (breatheTimer > 3)
        {
            breatheTimer = 0;
            playerController.breathe = false;
            Color tmp = blinkColor.color;
            tmp.a = 0;
            blinkColor.color = tmp;
        }

        if (Input.GetButtonDown("E") && !playerController.breathe)
        {
            anxietyLevel -= .1f;
            playerController.breathe = true;
        }
        else if (Input.GetButtonDown("Q") && !playerController.breathe)
        {
            anxietyLevel -= .1f;
            playerController.beach = true;
        }

        anxietyLevel = Mathf.Clamp(anxietyLevel, 0, 1);
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;
        heartbeat.volume = anxietyLevel;

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Safe Room")
        {
            anxietyLevel -= Time.deltaTime / 120.0f;
        }

       
    }
}
