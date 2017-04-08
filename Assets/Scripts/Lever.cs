using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;

    private SpriteRenderer sprite_renderer;

    private Vector3 orig_pos;
    // private float initial_length;
    private float current_scale_factor;
    private float scale_factor = 4;
    private float SCALE_OFFSET_FACTOR = 1.25f;  // TODO: Calculate from real values

    public float lever_speed = 0.1f;

    private float GetHeight() {
        return sprite_renderer.sprite.rect.size.y;
    }

    private Vector2 local_sprite_size;

    void Awake() {
        orig_pos = gameObject.transform.localPosition;

        // TODO: Move this essential logic to some kind of globally accessible utility class
        Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Vector3 world_size = local_sprite_size;

    }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        current_scale_factor = 1;
	}
	
	void Update () {

        if (my_switch.IsPressed) {

            if (current_scale_factor + lever_speed < scale_factor) {
                current_scale_factor += lever_speed;
            }
            else {
                current_scale_factor = scale_factor;
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

        gameObject.transform.localScale = new Vector2(1, current_scale_factor);

        float sprite_frac = 1 + local_sprite_size.y / 2;
        var effective_scaling = (current_scale_factor - 1) / scale_factor;
        gameObject.transform.localPosition = orig_pos + (transform.up * sprite_frac) * effective_scaling;
    }
}
