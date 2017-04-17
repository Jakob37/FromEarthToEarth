using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    private SpriteRenderer sprite_renderer;

    public Sprite[] frames;

    private BoxCollider2D coll;
    private Vector2 orig_coll_size;
    private float coll_box_offset = 0.15f;

    private float seconds_since_switch;
    public float retain_after_press_time = 0.3f;
    public bool IsPressed { get { return seconds_since_switch < retain_after_press_time; } }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        seconds_since_switch = float.MaxValue;
        coll = GetComponent<BoxCollider2D>();

        orig_coll_size = coll.size;
	}
	
    public void Press() {
        seconds_since_switch = 0;
    }

	void Update () {

        seconds_since_switch += Time.deltaTime;

        int target_frame;

        if (IsPressed) {
            target_frame = 1;
            coll.size = new Vector2(orig_coll_size.x, orig_coll_size.y - coll_box_offset);
        }
        else {
            target_frame = 0;
            coll.size = orig_coll_size;
        }

        sprite_renderer.sprite = frames[target_frame];
	}

    public bool IsObjectOnSwitch(GameObject other_obj) {

        Collider2D switch_collider = gameObject.GetComponent<Collider2D>();
        Collider2D obj_collider = other_obj.GetComponent<Collider2D>();

        return Physics2D.IsTouching(switch_collider, obj_collider);
    }
}
