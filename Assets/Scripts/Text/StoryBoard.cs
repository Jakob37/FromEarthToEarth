using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoard : MonoBehaviour {

    public StoryBoardName[] board_names;

    private StoryPanelController story_board_controller;
    private int board_index;

    private bool is_iterated_through;
    public bool IsIteratedThrough { get { return false; } }

    void Start() {
        story_board_controller = FindObjectOfType<StoryPanelController>();
        board_index = 0;
        is_iterated_through = false;
    }

    public void ActivateStoryBoard() {

        StoryBoardName board_name = board_names[board_index];
        story_board_controller.ActivateStoryBoard(board_name);
        CheckIteratedThrough();
    }

    public void DeactivateStoryBoard() {

        story_board_controller.DeactivateStoryBoard();
    }

    public void IterateStoryBoard() {

        board_index += 1;
        CheckIteratedThrough();

        if (board_index > board_names.Length - 1) {
            board_index -= board_names.Length;
        }

        StoryBoardName board_name = board_names[board_index];
        story_board_controller.IterateStoryBoard(board_name);
    }

    public void CheckIteratedThrough() {
        if (board_index >= board_names.Length - 1) {
            is_iterated_through = true;
            story_board_controller.SignalFullIteration();
        }
    }
}
