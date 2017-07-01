using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager instance;

    public int test_value;

    public ProgressData progress_data;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
            progress_data = new ProgressData();
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            // test_value += 1;
            // print("test_value iterated to: " + test_value);

            print("Current saved value is: " + instance.progress_data.completed_levels);
        }
    }

    public static void UpdateProgress(int completed_level) {

        print("Attempting to update progress with value: " + completed_level);

        if (completed_level > instance.progress_data.completed_levels) {
            instance.progress_data.completed_levels = completed_level;
            print("New completed level value assigned: " + completed_level);
        }
    }
}
