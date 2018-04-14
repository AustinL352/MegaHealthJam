using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VignetteController : MonoBehaviour {

    public VignetteModel.Settings vSettings;
    public PostProcessingProfile postProcess;
	// Use this for initialization
	void Start () {
        vSettings = postProcess.vignette.settings;
        vSettings.intensity = 0;
        postProcess.vignette.settings = vSettings;

    }
	
	// Update is called once per frame
	void Update () {
        vSettings.intensity = Mathf.Lerp(0, 1, Time.time / 180);

        postProcess.vignette.settings = vSettings;
    }
}
