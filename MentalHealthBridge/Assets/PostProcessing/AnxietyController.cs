using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class AnxietyController : MonoBehaviour {

    VignetteModel.Settings vSettings;
    public PostProcessingProfile postProcess;
    public PlayerController playerController;
    private Image blinkColor;
    public RectTransform blinkPosition;
    public AudioSource heartbeat;
    public AudioSource ambience;
    public AudioSource crowd;



    float anxietyLevel = 0;
    float breatheTimer = 0;

	// Use this for initialization
	void Start () {
        vSettings = postProcess.vignette.settings;
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;

        blinkColor = blinkPosition.gameObject.GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        anxietyLevel += Time.deltaTime / 180.0f;

        if(playerController.breathe)
        {
            breatheTimer += Time.deltaTime;
            //blinkPosition.localScale = new Vector3(blinkPosition.localScale.x, blinkPosition.localScale.y + Mathf.Sin(breatheTimer) * 100, blinkPosition.localScale.z);
            Color tmp = blinkColor.color;
            tmp.a = Mathf.Sin(breatheTimer);
            blinkColor.color = tmp;
        }
        if(breatheTimer > 3)
        {
            breatheTimer = 0;
            playerController.breathe = false;
            Color tmp = blinkColor.color;
            tmp.a = 0;
            blinkColor.color = tmp;
        }

        if (Input.GetButtonDown("E") && !playerController.breathe)
        {
            anxietyLevel -= .25f;
            playerController.breathe = true;
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
