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
    public Image breatheIcon;
    public RawImage beachIcon;

    public RectTransform blinkPosition;
    public AudioSource[] heartBeats;
    public AudioSource[] breathing;


    public AudioSource ambience;
    public AudioSource crowd;


    float breatheCoolDown = 30;
    float beachCoolDown = 60;

    int currentLevel = 0;

    
    public float anxietyLevel = 0;
    float breatheTimer = 0;
    float beachTimer = 0;



    // Use this for initialization
    void Start () {
        vSettings = postProcess.vignette.settings;
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;

        //breathingOne.Play();

    }
	
	// Update is called once per frame
	void Update () {
        anxietyLevel += (Time.deltaTime / 120.0f) * 3;
        //Color tmp2 = breatheIcon.color;
        //Debug.Log(tmp2.a);

        if (Input.GetButtonDown("E") && breatheTimer == 0 && !playerController.beach)
        {
            anxietyLevel -= .1f;
            playerController.breathe = true;

            Color tmp = breatheIcon.color;
            tmp.a = 0;
            breatheIcon.color = tmp;
        }
        else if (Input.GetButtonDown("Q") && beachTimer == 0 && !playerController.breathe)
        {
            anxietyLevel -= .1f;
            playerController.beach = true;
            Color tmp = beachIcon.color;
            tmp.a = 0;
            beachIcon.color = tmp;
        }


        if (playerController.breathe)
        {
            breatheTimer += Time.deltaTime;
            Color tmp = blinkColor.color;
            tmp.a = Mathf.Sin(breatheTimer * 1.75f) ;
            blinkColor.color = tmp;
        }
        else if(breatheTimer > 0)
        {
            breatheTimer += Time.deltaTime;
            if (breatheTimer == breatheCoolDown)
                breatheTimer = 0;

            Color tmp = breatheIcon.color;
            tmp.a = Mathf.Lerp(0, 1, (breatheTimer - 3) / (breatheCoolDown - 3));
            float gray = tmp.grayscale;
            breatheIcon.color = tmp;
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
        else if(beachTimer > 0)
        {
            beachTimer += Time.deltaTime;
            if (beachTimer == beachCoolDown)
                beachTimer = 0;

            Color tmp = beachIcon.color;
            tmp.a = Mathf.Lerp(0, 1, (beachTimer - 9) / (beachCoolDown - 9));
            beachIcon.color = tmp;
        }

        if (beachTimer > 9)
        {
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
            playerController.breathe = false;
            Color tmp = blinkColor.color;
            tmp.a = 0;
            blinkColor.color = tmp;
        }

        if (anxietyLevel > .25f)
            currentLevel = 1;
        if (anxietyLevel > .75f)
            currentLevel = 2;
        else
            currentLevel = 0;

        anxietyLevel = Mathf.Clamp(anxietyLevel, 0, 1);
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;
        for(int i =0; i < 3; i++)
        {
            if (i == currentLevel)
            {
                heartBeats[i].volume = anxietyLevel;
                breathing[i].volume = Mathf.Lerp(0, 0.1f, anxietyLevel);
            }
            else
            {
                heartBeats[i].volume = 0;
                breathing[i].volume = 0;
            }

        }


       // breathing[0].volume = Mathf.Lerp(0, 0.1f, anxietyLevel);

        //if(anxietyLevel > .25f && currentBreath == 1)
        //{
        //    breathingOne.Stop();
        //    breathingTwo.Play();
        //    breathingTwo.loop = true;
        //    currentBreath = 2;
        //    Debug.Log(currentBreath);
        //}


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Safe Room")
        {
            anxietyLevel -= Time.deltaTime / 120.0f;
        }

       
    }
}
