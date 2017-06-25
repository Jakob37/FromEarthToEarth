using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    private BaseRainScript base_rain_script;

    void Start() {
        base_rain_script = FindObjectOfType<BaseRainScript>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.P)) {
            base_rain_script.intensity_modifier_active = true;
        }
        else if (Input.GetKeyUp(KeyCode.P)) {
            base_rain_script.intensity_modifier_active = false;
        }
    }
}
