using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoText : MonoBehaviour {

    public bool show_player_stats;
    public Player player;

    public TextAsset level_text;

    private List<string> story_data;

    private int current_level;

    private Text text_object;
    private int current_text_index;

	void Start () {
        text_object = GetComponent<Text>();
        story_data = GenerateTestStoryData();
        current_text_index = 0;

        // current_level
	}

    private List<string> GenerateTestStoryData() {
        var story_text = new List<string>();
        story_text.Add("Press [Arrows] to move");
        story_text.Add("Press [Space] to jump");
        story_text.Add("Press [Space] midair to double jump");
        story_text.Add("Press [Control] while standing next to a block to pick it up");
        story_text.Add("Press [Control] while holding a block to throw it");
        story_text.Add("Press [Control] while standing on mud to make a block");
        story_text.Add("Reach the end of the level to travel to the next area");
        return story_text;
    }

    void Update () {

        if (Input.GetButtonDown("Jump")) {
            current_text_index += 1;
        }
        
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
                show_text = story_data[current_text_index];
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
