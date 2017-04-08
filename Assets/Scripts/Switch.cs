using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {


    private SpriteRenderer sprite_renderer;

    private float seconds_since_switch;
    public float retain_after_press_time = 0.3f;
    public bool IsPressed { get { return seconds_since_switch < retain_after_press_time; } }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        seconds_since_switch = float.MaxValue;
	}
	
    public void Press() {
        seconds_since_switch = 0;
    }

	void Update () {

        seconds_since_switch += Time.deltaTime;

        if (IsPressed) {
            sprite_renderer.color = new Color(1, 0, 0);
        }
        else {
            sprite_renderer.color = new Color(1, 1, 1);
        }
	}

    public bool IsObjectOnSwitch(GameObject other_obj) {

        Collider2D switch_collider = gameObject.GetComponent<Collider2D>();
        Collider2D obj_collider = other_obj.GetComponent<Collider2D>();

        return Physics2D.IsTouching(switch_collider, obj_collider);
    }
}
