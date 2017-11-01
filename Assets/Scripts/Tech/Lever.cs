using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;
    private SpriteRenderer sprite_renderer;
    public float lever_speed = 0.1f;

    public bool inverse_lever = false;
    public bool debug_print = false;

    private LeverPole pole;
    private Transform max_pos_transform;
    private Transform curr_top_transform;
    private Transform min_pos_transform;

    private Transform origin_transform;

    private float DistToTop {
        get {
            // return max_pos_transform.position.y - curr_top_transform.position.y;
            return Vector2.Distance(new Vector2(max_pos_transform.position.x, max_pos_transform.position.y), new Vector2(curr_top_transform.position.x, curr_top_transform.position.y));
        }
    }

    private float DistToBottom {
        get {
            // return Vector2.Distance(curr_top_transform.position, min_pos_transform.position);
            return Vector2.Distance(new Vector2(min_pos_transform.position.x, min_pos_transform.position.y), new Vector2(curr_top_transform.position.x, curr_top_transform.position.y));
            //return Vector2.Distance(curr_top_transform.position, min_pos_transform.position);
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

    void Awake() {
        pole = GetComponentInChildren<LeverPole>();
    }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        curr_top_transform = GetComponentInChildren<TopEdge>().gameObject.transform;
        max_pos_transform = GetComponentInChildren<MaxHeight>().gameObject.transform;
        min_pos_transform = GetComponentInChildren<MinHeight>().gameObject.transform;

        // origin_transform = 
    }

    void Update () {

        float offset;

        if (debug_print) {
            print("DistToBottom: " + DistToBottom + " DistToTop: " + DistToTop);
        }

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
