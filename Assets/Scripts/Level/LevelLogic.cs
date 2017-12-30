using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.LevelLogic;
using System;
using DigitalRuby.RainMaker;

public class LevelLogic : MonoBehaviour {

    // public int current_level;
    private InfoText listener;

    private float elapsed_time;
    private int last_trig_second = 0;

    private MusicManager music_manager;
    private ControlText control_text;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    void Awake() {
        music_manager = FindObjectOfType<MusicManager>();
    }

    void Start() {
        elapsed_time = 0;
        control_text = GameObject.FindObjectOfType<ControlText>();
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
        SaveManager.UpdateProgress(current_level);
        music_manager.TrigNewLevel();
        SceneManager.LoadScene(new_level);
    }

    public static int GetCurrentLevel() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    void Update() {
        elapsed_time += Time.deltaTime;
        if (listener != null && elapsed_time > last_trig_second + 1) {
            last_trig_second += 1;
            // print("Trigging:" + last_trig_second);
            var event_carrier = new LevelEventCarrier(LevelEventType.TimeSinceStartPassed, nbr_param:last_trig_second);
            DispatchEvent(event_carrier);
        }

        UpdateLevelControl();
    }

    private void UpdateLevelControl() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            WinCondition();
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            control_text.ToggleActive();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            MusicController music_controller = GameObject.FindObjectOfType<MusicController>();
            music_controller.Mute();
        }

        CheckNumberPress();
    }

    private void CheckNumberPress() {
        for (int i = 0; i < keyCodes.Length; i++) {
            if (Input.GetKeyDown(keyCodes[i])) {
                int numberPressed = i + 1;
                SceneManager.LoadScene(numberPressed - 1);
            }
        }
    }
}



 
