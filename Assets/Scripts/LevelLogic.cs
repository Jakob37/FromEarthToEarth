using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.LevelLogic;
using System;

public class LevelLogic : MonoBehaviour {

    // public int current_level;

    private InfoText listener;

    private float elapsed_time;
    private int last_trig_second = 0;

    void Start() {
        elapsed_time = 0;
    }

    public void AssignListener(InfoText info_text) {
        this.listener = info_text;
    }

    private void DispatchEvent(LevelEventCarrier occured_event) {
        if (listener != null) {
            listener.DispatchEvent(occured_event);
        }
        else {
            throw new MissingMemberException("No listener assigned!");
        }
    }

    public void WinCondition() {

        var current_level = SceneManager.GetActiveScene().buildIndex;
        var new_level = current_level + 1;
        print("Loading scene: " + new_level);
        SceneManager.LoadScene(new_level);
    }

    public static int GetCurrentLevel() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    void Update() {
        elapsed_time += Time.deltaTime;
        if (elapsed_time > last_trig_second + 1) {
            last_trig_second += 1;
            // print("Trigging:" + last_trig_second);
            var event_carrier = new LevelEventCarrier(LevelEventType.TimeSinceStartPassed, nbr_param:last_trig_second);
            DispatchEvent(event_carrier);
        } 
    }
}
