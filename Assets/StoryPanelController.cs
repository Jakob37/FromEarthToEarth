﻿using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public enum StoryBoardName {
    level1_jump,
    level1_block_pickup,
    level1_block_throw,
    level1_block_make,
    level1_block_putdown,
    level1_end,
    level2_putdown,
    level3_help,
    level5_help,
    intermission12_1,
    intermission12_2,
    intermission23_1,
    intermission23_2,
    intermission34_1,
    intermission34_2,
    intermission45_1,
    intermission45_2,
    
    level2_s1_1,
    level2_s1_2,
    level2_s1_3,
    level2_s2_1,
    level2_s2_2,
    level2_s2_3,
    griever1_1,
    griever1_2,
    griever1_3,
    griever1_4,

    level3_s1_1,
    level3_s1_2,
    level3_s1_3,
    level3_s2_1,
    level3_s2_2,
    level3_s3_1,
    level3_s3_2,

    level4_s1_1,
    level4_s1_2,

    level_levers_s1_1,
    level_levers_s1_2,
    level_levers_s1_3,
    level_levers_s2_1,
    level_levers_s2_2,
    dummy
}

public class StoryPanelController : MonoBehaviour {

    public GameObject story_text_panel_go;

    private string board_text_file = "board_texts";
    private Dictionary<StoryBoardName, string> story_board_texts;

    private StoryArrow story_arrow;

    void Start() {
        ParseStoryText(board_text_file);
        story_board_texts = ParseStoryText(board_text_file);

        story_arrow = story_text_panel_go.GetComponentInChildren<StoryArrow>();
    }

    private Dictionary<StoryBoardName, string> ParseStoryText(string resource_name, string splitter="\\|") {

        Dictionary<StoryBoardName, string> board_entries_tmp = new Dictionary<StoryBoardName, string>();

        List<string[]> board_entries = Utils.ParseTextToSplitList(resource_name, splitter);
        foreach (string[] board_entry in board_entries) {

            StoryBoardName board_name = Utils.ParseEnum<StoryBoardName>(board_entry[0]);
            string board_text = board_entry[1];
            board_entries_tmp[board_name] = board_text;
        }

        return board_entries_tmp;
    }

    public void ActivateStoryBoard(StoryBoardName board_name) {
        story_text_panel_go.SetActive(true);
        story_text_panel_go.GetComponentInChildren<Text>().text = story_board_texts[board_name];
        story_arrow.Reset();
    }

    public void IterateStoryBoard(StoryBoardName board_name) {
        story_text_panel_go.GetComponentInChildren<Text>().text = story_board_texts[board_name];
    }

    public void DeactivateStoryBoard() {
        story_text_panel_go.SetActive(false);
    }

    public void SignalFullIteration() {
        story_arrow.SignalIteratedThrough();
    }
}
