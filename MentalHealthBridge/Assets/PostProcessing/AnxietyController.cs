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

    public string heavyBreath;
    public string beachSound;



    public AudioSource ambience;
    public AudioSource crowd;
    private AudioSource beachSource;

    float breatheCoolDown = 15;
    float beachCoolDown = 30;

    int currentLevel = 0;

    
    public float anxietyLevel = 0;
    float breatheTimer = 0;
    float beachTimer = 0;

    float breatheCooldownTimer = 0;
    float beachCooldownTimer = 0;



    // Use this for initialization
    void Start () {
        vSettings = postProcess.vignette.settings;
        vSettings.intensity = anxietyLevel;
        postProcess.vignette.settings = vSettings;

        beachCooldownTimer = beachCoolDown;
        breatheCooldownTimer = breatheCoolDown;
        //breathingOne.Play();

    }
	
	// Update is called once per frame
	void Update () {
        if(!playerController.breathe && !playerController.beach)
            anxietyLevel += (Time.deltaTime / 120.0f) * 3;
        //Color tmp2 = breatheIcon.color;

        if (Input.GetButtonDown("E") && breatheCooldownTimer >= breatheCoolDown && !playerController.beach)
        {
            anxietyLevel -= .5f;
            playerController.breathe = true;

            Color tmp = breatheIcon.color;
            tmp.a = 0;
            breatheIcon.color = tmp;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load(heavyBreath) as AudioClip;
            audioSource.Play();
            breatheCooldownTimer = 0;
        }
        else if (Input.GetButtonDown("Q") && !playerController.breathe)
        {
            
            if (beachCooldownTimer >= beachCoolDown)
            {
                anxietyLevel -= .75f;
                playerController.beach = true;
                Color tmp = beachIcon.color;
                tmp.a = 0;
                beachIcon.color = tmp;

                beachSource = gameObject.AddComponent<AudioSource>();
                beachSource.clip = Resources.Load(beachSound) as AudioClip;
                beachSource.volume = 0;
                beachSource.Play();
                beachCooldownTimer = 0;
            }
        }


        if (playerController.breathe)
        {
            breatheTimer += Time.deltaTime;
            Color tmp = blinkColor.color;
            tmp.a = Mathf.Sin(breatheTimer * 1.75f) ;
            blinkColor.color = tmp;
        }
        else
        {
            breatheCooldownTimer += Time.deltaTime;
            Color tmp = breatheIcon.color;
            tmp.a = Mathf.Lerp(0, 1, (breatheCooldownTimer) / (breatheCoolDown));
            Debug.Log(tmp.a);

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
                beachSource.volume = tmp.a;

            }
            else
            {
                tmp = beach.color;
                tmp.a = Mathf.Lerp(0, 1, (beachTimer - 1) / 5);
                beach.color = tmp;
                beachSource.volume = tmp.a;

            }

        }
        else
        {
            beachCooldownTimer += Time.deltaTime;        
            Color tmp = beachIcon.color;
            tmp.a = Mathf.Lerp(0, 1, (beachCooldownTimer) / (beachCoolDown));
            Debug.Log(tmp.a);

            beachIcon.color = tmp;
            if (beachSource)
            {
                beachSource.Stop();
            }
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
            beachTimer = 0;
        }

        if (breatheTimer > 3)
        {
            playerController.breathe = false;
            Color tmp = blinkColor.color;
            tmp.a = 0;
            blinkColor.color = tmp;
            breatheTimer = 0;
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
            if (i == currentLevel && !playerController.breathe && !playerController.beach)
            {
                Debug.Log(heartBeats[i].volume);
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
