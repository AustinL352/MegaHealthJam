using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        Vector2 worldPoint = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Label(new Rect(worldPoint.x - 100, (Screen.height - worldPoint.y) - 50, 200, 100), "Text to display.");
    }
}
