using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoard : MonoBehaviour {

    public StoryBoardName board_name;

    private StoryPanelController story_board_controller;

    void Start() {
        story_board_controller = FindObjectOfType<StoryPanelController>();
    }

    public void ActivateStoryBoard() {

        story_board_controller.ActivateStoryBoard(board_name);
    }

    public void DeactivateStoryBoard() {
        story_board_controller.DeactivateStoryBoard();
    }
}
