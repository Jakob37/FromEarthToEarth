using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager instance;

    public int test_value;

    public ProgressData progress_data;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;

            LoadData();

            if (progress_data == null) {
                progress_data = new ProgressData();
            }
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            print("Current saved value is: " + instance.progress_data.completed_levels);
        }
    }

    public static void UpdateProgress(int completed_level) {

        print("Attempting to update progress with value: " + completed_level);

        if (completed_level > instance.progress_data.completed_levels) {
            instance.progress_data.completed_levels = completed_level;
            print("New completed level value assigned: " + completed_level);
        }

        SaveData();
    }

    public static void SaveData() {

        print("Save function called");

        if (!Directory.Exists("Saves")) {
            Directory.CreateDirectory("Saves");
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");
        ProgressData local_copy = instance.progress_data;
        formatter.Serialize(saveFile, local_copy);
        saveFile.Close();

        print("Save function completed");
    }

    public void LoadData() {

        print("Load function called");

        BinaryFormatter formatter = new BinaryFormatter();

        try {
            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
            ProgressData local_copy = (ProgressData)formatter.Deserialize(saveFile);
            instance.progress_data = local_copy;
            saveFile.Close();
        }
        catch (DirectoryNotFoundException e) {
            print("Saves directory not found, probably no data saved this far");
        }

        print("Load function completed");
    }
}
