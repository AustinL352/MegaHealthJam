using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour {

    public PlayerController script;
    public AnxietyController anxiety;


    bool played = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if(!anxiety.Final && !played)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("The End") as AudioClip;
            audioSource.volume = .25f;
            audioSource.Play();
            played = true;
        }


	}


    void OnEnable()
    {
        script.enabled = false;

        anxiety.Final = true;

    }
}
