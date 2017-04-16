using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;

    private SpriteRenderer sprite_renderer;

    private float current_scale_factor;
    private float max_scale_factor = 4;
    // private float SCALE_OFFSET_FACTOR = 1.25f;  // TODO: Calculate from real values

    public float lever_speed = 0.1f;

    private LeverPole pole;

    Transform max_pos_transform;
    Transform curr_top_transform;
    Transform min_pos_transform;

    private float DistToTop {
        get {
            return max_pos_transform.position.y - curr_top_transform.position.y;
        }
    }

    private float DistToBottom {
        get {
            return curr_top_transform.position.y - min_pos_transform.position.y;
        }
    }

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

        curr_top_transform = GetComponentInChildren<TopEdge>().gameObject.transform;
        max_pos_transform = GetComponentInChildren<MaxHeight>().gameObject.transform;
        min_pos_transform = GetComponentInChildren<MinHeight>().gameObject.transform;
    }

    void Update () {

        // if (my_switch.IsPressed) {
        // 
        //     if (current_scale_factor + lever_speed < max_scale_factor) {
        //         current_scale_factor += lever_speed;
        //     }
        //     else {
        //         current_scale_factor = max_scale_factor;
        //     }
        // }
        // else {
        // 
        //     if (current_scale_factor - lever_speed > 1) {
        //         current_scale_factor -= lever_speed;
        //     }
        //     else {
        //         current_scale_factor = 1;
        //     }
        // }
        //pole.AssignOffset((current_scale_factor - 1) / 1.5f);


        if (my_switch.IsPressed) {

            if (DistToTop > 0) {
                current_scale_factor += lever_speed;
            }
            // else {
            //     current_scale_factor = max_scale_factor;
            // }
        }
        else {

            if (DistToBottom > 0) {
                current_scale_factor -= lever_speed;
            }
            // else {
            //     current_scale_factor = 1;
            // }
        }

        pole.AssignOffset((current_scale_factor - 1) / 1.5f);
        // pole.AssignOffset((current_scale_factor - 1) / 1.5f);
    }
}
