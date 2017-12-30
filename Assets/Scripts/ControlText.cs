using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlText : MonoBehaviour {

    public GameObject text_object;
    public bool start_active;

    void Start() {
        if (start_active) {
            SetActive(true);
        }
        else {
            SetActive(false);
        }
    }

    public void ToggleActive() {
        if (text_object.activeSelf) {
            SetActive(false);
        }
        else {
            SetActive(true);
        }
    }

    public void SetActive(bool is_active) {
        text_object.SetActive(is_active);
    }
}
