using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeScene : MonoBehaviour
{
    public PlayerController script;
    public AnxietyController anxiety;

    public GameObject quake;


    Vector3 originalPos;
    Quaternion originalRot;

    float quakeTimer = 0;

    void Start ()
    {
		
	}

	void Update ()
    {
        script.enabled = false;
        anxiety.anxietyLevel = 0.5f;
        anxiety.enabled = true;

        if (quakeTimer < 2)
        {
            originalRot = transform.localRotation;

            //Quaternion prev = new Quaternion(0, 0, 0, 0);
            //Quaternion curr = new Quaternion(-50, 90, 0, 0);

            //transform.rotation = Quaternion.Slerp(transform.rotation, , 0);

            //transform.localPosition = transform.position + Random.insideUnitSphere * .1f;

            //transform.LookAt(new Vector3(transform.position.x, 0, transform.position.z));

           // Vector3.RotateTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 2);

           // Vector3 camFix = new Vector3(transform.localPosition.x, originalPos.y, transform.localPosition.z);
           // transform.localPosition = camFix;


        }
        else
        {
           
            transform.localPosition = originalPos;
            if(!anxiety.Quakin)
            {
                script.enabled = true;
                enabled = false;
            }
        }

        quakeTimer += Time.deltaTime;

        
    }

    private void OnEnable()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load("Final Floor Crack") as AudioClip;
        audioSource.volume = .25f;
        audioSource.Play();

        originalPos = transform.localPosition;

        anxiety.Quakin = true;
        quake.SetActive(true);
    }
}
