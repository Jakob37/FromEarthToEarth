using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlText : MonoBehaviour {

    public enum ControlTextState {
        inactive,
        active,
        indicator
    }

    public GameObject text_object_full;
    public GameObject text_object_short;
    public bool start_active;
    public bool fades_out;

    private Text full_text;
    private Text short_text;

    public float total_fade_s;
    private float current_fade_s;

    public float start_fade_delay_s;

    private ControlTextState active_state;

    void Start() {

        full_text = text_object_full.GetComponent<Text>();
        short_text = text_object_short.GetComponent<Text>();

        if (start_active) {
            SetActiveState(ControlTextState.indicator);
            current_fade_s = total_fade_s;
        }
        else {
            SetActiveState(ControlTextState.inactive);
            current_fade_s = 0;
        }
    }

    void Update() {

        if (start_fade_delay_s > 0 && fades_out) {
            start_fade_delay_s -= Time.deltaTime;
        }

        if (active_state == ControlTextState.indicator && start_fade_delay_s <= 0) {
            if (current_fade_s > 0) {
                FadeOut();
            }
            else {
                SetActiveState(ControlTextState.inactive);
            }
        }
        else {
            SetTextAlpha(1);
        }
    }

    private void FadeOut() {
        current_fade_s -= Time.deltaTime;
        float current_alpha = current_fade_s / total_fade_s;
        SetTextAlpha(current_alpha);
    }

    private void SetTextAlpha(float alpha) {
        full_text.color = new Color(1, 1, 1, alpha);
        short_text.color = new Color(1, 1, 1, alpha);
    }

    public void ToggleActive() {
        if (active_state == ControlTextState.active) {
            SetActiveState(ControlTextState.inactive);
        }
        else {
            SetActiveState(ControlTextState.active);
        }
    }

    public void SetActiveState(ControlTextState state) {

        active_state = state;

        if (state == ControlTextState.active) {
            text_object_full.SetActive(true);
            text_object_short.SetActive(false);
        }
        else if (state == ControlTextState.indicator) {
            text_object_full.SetActive(false);
            text_object_short.SetActive(true);
        }
        else {
            text_object_full.SetActive(false);
            text_object_short.SetActive(false);
        }
    }
}
