using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState {
    main,
    level_pick,
    credits
}

public class MenuManager : MonoBehaviour {

    public GameObject main_canvas;
    public GameObject level_pick_canvas;
    public GameObject credits_canvas;

    void Start() {

        main_canvas.SetActive(true);
        level_pick_canvas.SetActive(false);
        credits_canvas.SetActive(false);
    }

    public void SwitchMenuState(string menu_state) {

        switch (menu_state) {
            case "main":
                main_canvas.SetActive(true);
                level_pick_canvas.SetActive(false);
                credits_canvas.SetActive(false);
                break;
            case "level_pick":
                main_canvas.SetActive(false);
                level_pick_canvas.SetActive(true);
                credits_canvas.SetActive(false);
                break;
            case "credits":
                main_canvas.SetActive(false);
                level_pick_canvas.SetActive(false);
                credits_canvas.SetActive(true);
                break;
            default:
                throw new ArgumentException("Unknown MenuState: " + menu_state);
        }
    }

    public void SwitchScene(int level_index) {
        print("Loading level index: " + level_index);
        SceneManager.LoadScene(level_index);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ResetProgress() {
        SaveManager.ResetProgress();
    }
}
