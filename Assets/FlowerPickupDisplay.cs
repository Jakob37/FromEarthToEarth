using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerPickupDisplay : MonoBehaviour {

    private Text display_text;
    private Image flower_image;

    private float display_time_timer;
    public float display_time;

    void Start() {
        display_text = GetComponent<Text>();
        flower_image = GetComponentInChildren<Image>();

        ChangeVisibility(false);
        display_time_timer = 0;
        // ChangeVisibility(false);
        // ShowFlowerCount(1);
    }

    void Update() {
        if (display_time_timer > 0) {
            display_time_timer -= Time.deltaTime;
            if (display_time_timer <= 0) {
                ChangeVisibility(false);
            }
        }
    }

    public void ShowFlowerCount(int flower_count, int total_flower_count=3) {

        string display_string = flower_count + " / " + total_flower_count;
        display_text.text = display_string;
        ChangeVisibility(true);
        display_time_timer = display_time;
    }

    private void ChangeVisibility(bool visible) {

        Color visibility_color;
        if (visible) {
            visibility_color = new Color(1, 1, 1, 1);
        }
        else {
            visibility_color = new Color(1, 1, 1, 0);
        }

        display_text.color = visibility_color;
        flower_image.color = visibility_color;
    }
}