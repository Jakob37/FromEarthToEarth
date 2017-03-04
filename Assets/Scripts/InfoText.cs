﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.LevelLogic;
using UnityEditor;
using System.Text.RegularExpressions;
using System;

public class InfoText : MonoBehaviour {

    public bool show_player_stats;
    public Player player;

    private List<LevelTextEntry> story_data;

    private int current_level;

    private Text text_object;
    private int current_text_index;
    private string level_text_field_delim = ";";

    private List<LevelEventType> occured_events;

	void Start () {
        text_object = GetComponent<Text>();
        int current_level = LevelLogic.GetCurrentLevel();
        story_data = ParseLevelEvents(current_level);
        current_text_index = 0;
        occured_events = new List<LevelEventType>();
        player.AssignListener(this);
	}

    public void DispatchEvent(LevelEventType occured_event) {
        occured_events.Add(occured_event);
    }

    private List<LevelTextEntry> ParseLevelEvents(int current_level) {

        int level_number = current_level + 1;
        var level_data_path = "Assets/Text/LevelText/l" + level_number + ".txt";

        print(level_data_path);

        TextAsset level_text = (TextAsset)AssetDatabase.LoadAssetAtPath(level_data_path, typeof(TextAsset));
        var splitFile = new string[] { "\r\n", "\r", "\n" };
        string[] level_text_lines = level_text.text.Split(splitFile, StringSplitOptions.None);

        var story_events = new List<LevelTextEntry>();
        for (int i = 0; i < level_text_lines.Length; i++) {

            string valueLine = level_text_lines[i];
            string[] values = Regex.Split(valueLine, level_text_field_delim); // your splitter here

            var entry_event_trig_description = values[0];
            var entry_text = values[1];
            var level_event = new LevelTextEntry(entry_event_trig_description, entry_text);
            story_events.Add(level_event);
        }

        return story_events;
    }

    void Update () {

        if (story_data[current_text_index].IsEventTriggered(occured_events)) {
            current_text_index += 1;
        }
        occured_events.Clear();
        
        var show_text = "";

        if (show_player_stats) {
            string mon_text = "";
            mon_text += GetMonText("Jump", player.is_jumping.ToString());
            mon_text += GetMonText("Grounded", player.is_grounded.ToString());
            mon_text += GetMonText("Jumps", player.remaining_jumps.ToString());
            show_text = mon_text;
        }
        else {
            if (current_text_index < story_data.Count) {
                show_text = story_data[current_text_index].GetLevelText();
            }
            else {
                show_text = "";
            }
        }
        text_object.text = show_text;
	}

    private string GetMonText(string descr, string text) {
        return string.Format("{0}: {1}\n", descr, text);
    }
}
