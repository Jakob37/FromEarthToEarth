using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public bool is_fading = true;

    private Rigidbody2D rigi;

    private SpriteRenderer sprite_renderer;

    private float remaining_percentage;

	void Start () {
        rigi = gameObject.GetComponent<Rigidbody2D>();
        rigi.isKinematic = false;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        remaining_percentage = 100;
	}

    public void TakenUp() {
        rigi.isKinematic = true;
    }

    public void PutDown(Vector2 throwForce) {
        rigi.isKinematic = false;
        rigi.AddForce(throwForce);
    }
	
	void Update () {

        if (remaining_percentage > 0 && is_fading) {
            remaining_percentage -= Time.deltaTime * 10;
        }

        if (remaining_percentage <= 0) {
            Destroy(gameObject);
        }

        var transparency = remaining_percentage / 100;
        sprite_renderer.color = new Color(1, 1, 1, transparency);
	}
}
