using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    public GameObject player;

    void OnGUI()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 400)
        {
            Vector2 worldPoint = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(transform.position.x, transform.position.y - 1000, 200, 100), "DEWBACK.");
        }
    }
}
