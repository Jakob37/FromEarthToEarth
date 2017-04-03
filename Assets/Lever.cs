using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;

    private SpriteRenderer sprite_renderer;

    private Vector3 orig_pos;
    private float initial_length;
    private float current_length;
    private float scale_factor = 4;

    private float GetHeight() {
        return sprite_renderer.sprite.rect.size.y;
    }

	void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        initial_length = GetHeight();
        orig_pos = gameObject.transform.position;
	}
	
	void Update () {

        if (my_switch.IsPressed) {
            gameObject.transform.localScale = new Vector2(1, scale_factor);
            gameObject.transform.position = orig_pos + transform.up; //+ new Vector3(0, initial_length * scale_factor / 2, 0);
        }
        else {
            gameObject.transform.localScale = new Vector2(1, 1);
            gameObject.transform.position = orig_pos;
        }
    }
}
