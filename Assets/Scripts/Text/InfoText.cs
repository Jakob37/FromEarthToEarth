using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.LevelLogic;
// using UnityEditor;
using System.Text.RegularExpressions;
using System;

public class InfoText : MonoBehaviour {

    public bool deactivate;

    public bool show_player_stats;
    private Player player;
    private LevelLogic level_logic;

    private List<LevelTextEntry> story_data;

    private int current_level;

    private Text text_object;
    private InfoTextPanel info_text_panel;

    private int current_text_index;
    private string level_text_field_delim = ";";

    private List<LevelEventCarrier> occured_events;

    // Check this out for text asset reading
    // http://gamedev.stackexchange.com/questions/85807/how-to-read-a-data-from-text-file-in-unity

    void Start () {

        text_object = GetComponentInChildren<Text>();
        info_text_panel = GetComponentInChildren<InfoTextPanel>();

        int current_level = LevelLogic.GetCurrentLevel();
        // story_data = ParseLevelEvents(current_level);
        current_text_index = 0;
        occured_events = new List<LevelEventCarrier>();

        player = GameObject.FindObjectOfType<Player>();
        player.AssignListener(this);
        level_logic = GameObject.FindObjectOfType<LevelLogic>();
        level_logic.AssignListener(this);
	}

    public void DispatchEvent(LevelEventType occured_event) {
        occured_events.Add(new LevelEventCarrier(occured_event));
    }

    public void DispatchEvent(LevelEventCarrier occured_event) {
        occured_events.Add(occured_event);
    }

    private List<LevelTextEntry> ParseLevelEventsManual() {

        string[] level_text_lines = {
            "press arrow;Press [Arrows] to move",
            "press space;Press [Space] to jump",
            "do midair_jump;[Space] midair to double jump",
            "do block_pickup;[Control] while standing next to a block to pick it up",
            "do throw_block;[Control] while holding a block to throw it",
            "do make_block;[Control] while standing on mud to make a block",
            "do reach_end;Reach the end of the level to travel to the next area"
        };

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

    private List<LevelTextEntry> ParseLevelEvents(int current_level) {
    
        int level_number = current_level + 1;

        TextAsset level_text = (TextAsset)Resources.Load("l" + level_number);

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

        if (deactivate) {
            return;
        }

        if (story_data.Count > current_text_index && story_data[current_text_index].IsEventTriggered(occured_events)) {
            current_text_index += 1;
        }
        occured_events.Clear();
        
        var show_text = "";

        if (show_player_stats) {
            string mon_text = "";
            mon_text += GetMonText("Jump", player.IsJumping.ToString());
            mon_text += GetMonText("Grounded", player.IsGrounded.ToString());
            mon_text += GetMonText("Jumps", player.RemainingJumps.ToString());
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

        UpdatePanel(show_text);
	}

    private void UpdatePanel(string target_text) {

        if (target_text == " ") {
            info_text_panel.gameObject.SetActive(false);
        }
        else {
            info_text_panel.gameObject.SetActive(true);
            AdaptPanelSizeToText(target_text);
        }
    }
    
    private void AdaptPanelSizeToText(string target_text) {

    }


    private string GetMonText(string descr, string text) {
        return string.Format("{0}: {1}\n", descr, text);
    }
}
