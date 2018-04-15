using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeScene : MonoBehaviour
{
    public PlayerController script;
    public AnxietyController anxiety;

    Vector3 originalPos;
    Quaternion originalRot;

    void Start ()
    {
		
	}

	void Update ()
    {
        script.enabled = false;
        anxiety.anxietyLevel = 0.5f;

        originalRot = transform.localRotation;
        transform.rotation = Quaternion.Euler(transform.localRotation.x, 90, transform.localRotation.z);

        originalPos = transform.localPosition;
        transform.localPosition = transform.position + Random.insideUnitSphere * 5;

        Vector3 camFix = new Vector3(transform.localPosition.x, originalPos.y, transform.localPosition.z);
        transform.localPosition = camFix;
    }
}
