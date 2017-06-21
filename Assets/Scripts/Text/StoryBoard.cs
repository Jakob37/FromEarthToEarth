using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoard : MonoBehaviour {

    public StoryBoardName[] board_names;

    private StoryPanelController story_board_controller;
    private int board_index;
    private bool has_been_active;

    void Start() {
        story_board_controller = FindObjectOfType<StoryPanelController>();
        board_index = 0;
        has_been_active = false;
    }

    public void ActivateStoryBoard() {

        StoryBoardName board_name = board_names[board_index];
        story_board_controller.ActivateStoryBoard(board_name);
        has_been_active = true;
    }

    public void DeactivateStoryBoard() {

        story_board_controller.DeactivateStoryBoard();
        if (has_been_active) {
            UpdateBoardIndex();
            has_been_active = false;
        }
    }

    private void UpdateBoardIndex() {
        board_index += 1;
        if (board_index > board_names.Length - 1) {
            board_index -= board_names.Length;
        }
    }
}
