using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerIndicatorFlower : MonoBehaviour {

    public int flower_number;
    private int button_number;

    public Sprite shade_sprite;
    public Sprite flower_sprite;

    private bool flower_found;
    private SpriteRenderer sprite_renderer;

    void Start() {
        LevelButton parent_button = gameObject.GetComponentInParent<LevelButton>();
        this.button_number = parent_button.level_number;

        this.sprite_renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        flower_found = SaveManager.GetFlowerPicked(this.button_number, this.flower_number);

        if (flower_found) {
            sprite_renderer.sprite = flower_sprite;
        }
        else {
            sprite_renderer.sprite = shade_sprite;
        }
    }
}
