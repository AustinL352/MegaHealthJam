using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    public GameObject text;

    void Update()
    {
        if (Vector3.Distance(text.transform.position, transform.position) <= 600)
            text.gameObject.SetActive(true);
        else
            text.gameObject.SetActive(false);
    }
}
