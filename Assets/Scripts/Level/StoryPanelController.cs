﻿using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public enum StoryBoardEntity {

    dummy,
    none,

    level1_jump,
    level1_block_pickup,
    level1_block_throw,
    level1_block_make,
    level1_block_putdown,
    level1_end,
    level2_putdown,
    level3_help,
    level5_help,

    level1_1,
    level1_2,
    level1_3,
    level1_4,

    level2_1,
    level2_2,
    level2_3,
    level2_4,
    level2_5,
    level2_griever,

    level3_1,
    level3_2,
    level3_3,
    level3_4,
    level3_5,

    level4_1,
    level4_2,
    level4_3,
    level4_4,

    level5_1,
    level5_2,
    theelder,
    theelder_activated,

    win_1,
    win_2,

    intermission12,
    intermission23,
    intermission34,
    intermission45,
    griever1,
    level2_s1,
    level2_s2,
    level3_s1,
    level3_s2,
    level3_s3,
    level4_s1,
    level_levers_s1,
    level_levers_s2,
    level1_further_controls
}

public class StoryPanelController : MonoBehaviour {

    public GameObject story_text_panel_go;

    private string board_text_file = "board_texts";
    private Dictionary<StoryBoardEntity, List<string>> story_board_entities;
    private int current_story_index;
    private StoryArrow story_arrow;
    private TextScrollIndicator text_scroll_indicator;

    private StoryBoardEntity active_story_board_entity;

    public int GetBoardEntityLength(StoryBoardEntity entity) {
        return story_board_entities[entity].Count;
    }

    void Awake() {
        story_arrow = story_text_panel_go.GetComponentInChildren<StoryArrow>();
        text_scroll_indicator = story_text_panel_go.GetComponentInChildren<TextScrollIndicator>();
    }

    void Start() {

        story_board_entities = ParseStoryEntities("board_entities");
        active_story_board_entity = StoryBoardEntity.none;
        story_text_panel_go.SetActive(false);
    }

    private Dictionary<StoryBoardEntity, List<string>> ParseStoryEntities(string resource_name, string splitter="\\|") {

        Dictionary<StoryBoardEntity, List<string>> board_entities_tmp = new Dictionary<StoryBoardEntity, List<string>>();

        List<string[]> board_entries = Utils.ParseTextToSplitList(resource_name, splitter);
        foreach (string[] board_entry in board_entries) {

            if (board_entry[0] == "" || board_entry[0].Substring(0, 1) == "#") {
                continue;
            }

            StoryBoardEntity board_name = Utils.ParseEnum<StoryBoardEntity>(board_entry[0]);
            if (board_entry[1] == "name") {
                continue;
            }

            string board_text = board_entry[1];
            if (!board_entities_tmp.ContainsKey(board_name)) {
                board_entities_tmp[board_name] = new List<String>();
            }

            board_entities_tmp[board_name].Add(board_text);
        }

        return board_entities_tmp;
    }

    public void ActivateStoryBoard(StoryBoardEntity board_entity, int board_index=0) {

        story_text_panel_go.SetActive(true);
        story_text_panel_go.GetComponentInChildren<Text>().text = story_board_entities[board_entity][board_index];

        active_story_board_entity = board_entity;
        int board_length = GetBoardEntityLength(board_entity);

        bool hidden_arrow = false;
        if (board_length == 1) {
            hidden_arrow = true;
        }
        story_arrow.Reset(hidden_arrow);


        text_scroll_indicator.ActivateIndicator(board_length);
    }

    public void IterateStoryBoard(StoryBoardEntity board_entity, int board_index=0) {

        story_text_panel_go.GetComponentInChildren<Text>().text = story_board_entities[board_entity][board_index];
    }

    public void DeactivateStoryBoard() {
        story_text_panel_go.SetActive(false);
        active_story_board_entity = StoryBoardEntity.none;
    }

    public void SignalFullIteration() {

        int board_length = GetBoardEntityLength(active_story_board_entity);
        story_arrow.SignalIteratedThrough();
        text_scroll_indicator.SignalIteratedThrough(board_length);
    }

    public StoryBoardEntity CurrentlyActiveStoryBoardname() {
        return active_story_board_entity;
    }
}
