using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    public int level_number;

    private Button button;

    void Start() {
        button = gameObject.GetComponent<Button>();
    }

    void Update() {

        int completed_levels = SaveManager.instance.progress_data.completed_levels;

        if (level_number - 1 <= completed_levels) {
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }
    }
}
