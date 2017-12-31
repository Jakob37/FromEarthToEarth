using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoard : MonoBehaviour {

    // public StoryBoardName[] board_names;
    public StoryBoardEntity board_entity;

    private StoryPanelController story_board_controller;
    private int board_index;

    private SoundEffectManager sound_manager;

    private bool is_iterated_through;
    public bool IsIteratedThrough { get { return false; } }

    private bool currently_active;
    public bool CurrentActive { get { return currently_active; } }

    private int BoardSize {
        get {
            int board_size = story_board_controller.GetBoardEntityLength(board_entity);
            return board_size;
        }
    }

    void Awake() {
        story_board_controller = FindObjectOfType<StoryPanelController>();
        sound_manager = FindObjectOfType<SoundEffectManager>();
    }

    void Start() {
        board_index = 0;
        is_iterated_through = false;
        currently_active = false;
    }

    public void ActivateStoryBoard() {

        // StoryBoardEntity board_name = board_names[board_index];
        story_board_controller.ActivateStoryBoard(board_entity, board_index);
        CheckIteratedThrough();
        currently_active = true;
    }

    public void DeactivateStoryBoard() {

        story_board_controller.DeactivateStoryBoard();
        currently_active = false;
    }

    public void IterateStoryBoard() {

        if (!currently_active) {
            ActivateStoryBoard();
            board_index = 0;
        }
        else {
            board_index += 1;
        }

        sound_manager.PlaySound(SoundEffect.iterate_stranger);

        CheckIteratedThrough();

        if (board_index > BoardSize - 1) {
            board_index -= BoardSize;
        }

        // StoryBoardName board_name = board_names[board_index];
        story_board_controller.IterateStoryBoard(board_entity, board_index);
    }

    public void CheckIteratedThrough() {
        if (board_index >= BoardSize) {
            is_iterated_through = true;
            story_board_controller.SignalFullIteration();

            if (BoardSize > 1) {
                DeactivateStoryBoard();
            }
        }
    }
}
