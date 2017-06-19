using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject main_canvas;
    public GameObject level_pick_canvas;

    void Start() {

        main_canvas.SetActive(true);
        level_pick_canvas.SetActive(false);
    }

    public void SwitchMenuState(bool level_pick_active) {
        if (level_pick_active) {
            level_pick_canvas.SetActive(true);
            main_canvas.SetActive(false);
        }
        else {
            level_pick_canvas.SetActive(false);
            main_canvas.SetActive(true);
        }
    }

    public void SwitchScene(int level_index) {
        SceneManager.LoadScene(level_index);
    }

}
