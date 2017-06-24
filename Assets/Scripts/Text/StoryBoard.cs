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

    private bool is_iterated_through;
    public bool IsIteratedThrough { get { return false; } }

    void Start() {
        story_board_controller = FindObjectOfType<StoryPanelController>();
        board_index = 0;
        has_been_active = false;
        is_iterated_through = false;
    }

    public void ActivateStoryBoard() {

        StoryBoardName board_name = board_names[board_index];
        story_board_controller.ActivateStoryBoard(board_name);
        // has_been_active = true;
    }

    public void DeactivateStoryBoard() {

        story_board_controller.DeactivateStoryBoard();
        // if (has_been_active) {
        //     IterateStoryBoard();
        //     has_been_active = false;
        // }
    }

    public void IterateStoryBoard() {
        board_index += 1;
        if (board_index > board_names.Length - 1) {
            board_index -= board_names.Length;
            is_iterated_through = true;
            story_board_controller.SignalFullIteration();
        }
        // ActivateStoryBoard();
        StoryBoardName board_name = board_names[board_index];
        story_board_controller.IterateStoryBoard(board_name);
    }
}
