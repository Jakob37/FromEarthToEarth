﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager instance;
    public int test_value;
    public ProgressData progress_data;
    public static bool debug_always_load_flowers = false;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;

            LoadData();

            if (progress_data == null) {
                print("Creating progress data");
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

        if (completed_level > instance.progress_data.completed_levels) {
            instance.progress_data.completed_levels = completed_level;
        }

        SaveData();
    }

    public static void SpecialFlowerPick(int level_index, int flower_index) {
        instance.progress_data.picked_flowers[level_index, flower_index] = true;
        SaveData();
    }

    public static bool GetFlowerPicked(int level_index, int flower_index) {

        if (debug_always_load_flowers) {
            return false;
        }

        return instance.progress_data.picked_flowers[level_index, flower_index];
    }

    public static int GetFlowersPickedOnLevel(int level_index) {

        int pick_count = 0;
        for (int i = 0; i <= 2; i++) {
            if (instance.progress_data.picked_flowers[level_index, i]) {
                pick_count++;
            }
        }

        return pick_count;
    }

    public static void SaveData() {

        if (!Directory.Exists("Saves")) {
            Directory.CreateDirectory("Saves");
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");
        ProgressData local_copy = instance.progress_data;
        formatter.Serialize(saveFile, local_copy);
        saveFile.Close();
    }

    public static void LoadData() {

        BinaryFormatter formatter = new BinaryFormatter();

        try {
            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
            ProgressData local_copy = (ProgressData)formatter.Deserialize(saveFile);
            instance.progress_data = local_copy;
            saveFile.Close();
        }
        catch (DirectoryNotFoundException e) {
            print("Caught exception: " + e);
            print("Saves directory not found, probably no data saved this far");
        }
        catch (FileNotFoundException e) {
            print("Caught exception: " + e);
            print("Save file not found, probably no data saved this far");
        }
    }

    public static void ResetProgress() {
        instance.progress_data.completed_levels = 0;
        instance.progress_data.picked_flowers = new bool[10, 3];
        SaveData();
    }
}
