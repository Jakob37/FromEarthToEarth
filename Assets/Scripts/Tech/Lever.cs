using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;
    private SpriteRenderer sprite_renderer;
    public float lever_speed = 0.1f;

    public bool inverse_lever = false;

    private LeverPole pole;
    private Transform max_pos_transform;
    private Transform curr_top_transform;
    private Transform min_pos_transform;

    private float DistToTop {
        get {
            return Vector2.Distance(max_pos_transform.position, curr_top_transform.position);
        }
    }

    private float DistToBottom {
        get {
            return Vector2.Distance(curr_top_transform.position, min_pos_transform.position);
        }
    }

    private float MaxOffset {
        get {
            return Vector2.Distance(min_pos_transform.position, max_pos_transform.position);
        }
    }

    public float GetSpriteHeight() {
        return sprite_renderer.sprite.rect.size.y;
    }

    // public Vector2 GetWorldSize() {
    //     Vector2 sprite_size = sprite_renderer.sprite.rect.size;
    //     Vector2 world_sprite_size = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
    //     return world_sprite_size;
    // }

    // public static Vector2 CalculateLocalSpriteSize(SpriteRenderer sprite_renderer) {
    //     Vector2 sprite_size = sprite_renderer.sprite.rect.size;
    //     Vector2 local_sprite_size = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
    //     return local_sprite_size;
    // }

    void Awake() {
        pole = GetComponentInChildren<LeverPole>();
    }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        curr_top_transform = GetComponentInChildren<TopEdge>().gameObject.transform;
        max_pos_transform = GetComponentInChildren<MaxHeight>().gameObject.transform;
        min_pos_transform = GetComponentInChildren<MinHeight>().gameObject.transform;
    }

    void Update () {

        float offset;

        if ((my_switch.IsPressed && !inverse_lever) || (!my_switch.IsPressed &&  inverse_lever)) {

            if (DistToTop > lever_speed) {
                offset = DistToBottom + lever_speed * Time.deltaTime;
            }
            else {
                offset = MaxOffset;
            }
        }
        else {

            if (DistToBottom > lever_speed) {
                offset = DistToBottom - lever_speed;
            }
            else {
                offset = 0;
            }
        }

        pole.AssignOffset(offset);
    }
}
