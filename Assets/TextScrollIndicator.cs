using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScrollIndicator : MonoBehaviour {

    private bool is_initialized;
    private Text text_object;
    private bool full_iteration_performed;

    public bool disabled;

    void Awake() {
        text_object = GetComponent<Text>();
        full_iteration_performed = false;
        if (disabled) {
            full_iteration_performed = true;
        }
    }

    public void SignalIteratedThrough(int board_length) {

        if (board_length > 1) {
            text_object.enabled = false;
            full_iteration_performed = true;
        }
    }

    public void ActivateIndicator(int board_length) {

        if (board_length > 1 && !full_iteration_performed) {
            text_object.enabled = true;
        }
        else {
            text_object.enabled = false;
        }
    }
}