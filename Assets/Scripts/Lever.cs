using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;

    private SpriteRenderer sprite_renderer;

    private float current_scale_factor;
    private float max_scale_factor = 4;
    private float SCALE_OFFSET_FACTOR = 1.25f;  // TODO: Calculate from real values

    public float lever_speed = 0.1f;

    private LeverPole pole;

    public float GetSpriteHeight() {
        return sprite_renderer.sprite.rect.size.y;
    }

    public Vector2 GetWorldSize() {
        Vector2 sprite_size = sprite_renderer.sprite.rect.size;
        Vector2 world_sprite_size = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
        return world_sprite_size;
    }

    public static Vector2 CalculateLocalSpriteSize(SpriteRenderer sprite_renderer) {
        Vector2 sprite_size = sprite_renderer.sprite.rect.size;
        Vector2 local_sprite_size = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
        return local_sprite_size;
    }

    void Awake() {
        pole = GetComponentInChildren<LeverPole>();
    }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        current_scale_factor = 1;
	}
	
	void Update () {

        if (my_switch.IsPressed) {

            if (current_scale_factor + lever_speed < max_scale_factor) {
                current_scale_factor += lever_speed;
            }
            else {
                current_scale_factor = max_scale_factor;
            }
        }
        else {

            if (current_scale_factor - lever_speed > 1) {
                current_scale_factor -= lever_speed;
            }
            else {
                current_scale_factor = 1;
            }
        }

        // pole.AssignCurrentScale(current_scale_factor);
        pole.AssignOffset((current_scale_factor - 1) / 1.5f);
    }
}
