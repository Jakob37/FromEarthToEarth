using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    private bool is_pressed;
    public bool IsPressed { get { return is_pressed; } }

    private SpriteRenderer sprite_renderer;

	void Start () {
        is_pressed = false;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
	
        if (is_pressed) {
            sprite_renderer.color = new Color(1, 0, 0);
        }
        else {
            sprite_renderer.color = new Color(0, 0, 0);
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {

        var target = coll.gameObject;

        if (target.GetComponent<PhysicalObject>() != null) {
            is_pressed = true;
        }

    }

    void OnCollisionExit2D(Collision2D coll) {
        var target = coll.gameObject;

        if (target.GetComponent<PhysicalObject>() != null) {
            is_pressed = false;
        }
    }
}
