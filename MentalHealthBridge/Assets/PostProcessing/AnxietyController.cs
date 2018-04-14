using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class AnxietyController : MonoBehaviour {

    VignetteModel.Settings vSettings;
    public PostProcessingProfile postProcess;
    public PlayerController playerController;


    float anxietyLevel = 0;
    float breatheTimer = 0;

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

        }
        if(breatheTimer > 1)
        {
            breatheTimer = 0;
            playerController.breathe = false;
        }

        if (Input.GetButtonDown("E") && !playerController.breathe)
        {
            Debug.Log("EEEEE");
            anxietyLevel -= .25f;
            playerController.breathe = true;
        }

        anxietyLevel = Mathf.Clamp(anxietyLevel, 0, 1);
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Safe Room")
        {
            anxietyLevel -= Time.deltaTime / 120.0f;
        }

       
    }
}
