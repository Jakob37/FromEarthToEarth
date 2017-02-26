using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public bool is_fading = false;

    private Rigidbody2D rigi;

    private SpriteRenderer sprite_renderer;

    public Sprite[] frames;

    private float remaining_percentage;

    private bool solidified;
    private bool is_solidifying;

    private float SOLIDIFYING_SPEED = 100;
    private BlockController carrier;

    private float rain_deduction = 0.5f;

    public bool IsFadeInDone() {
        return this.sprite_renderer.color.a == 1f;
    }

	void Awake() {

        rigi = gameObject.GetComponent<Rigidbody2D>();

        rigi.isKinematic = false;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        remaining_percentage = 100;
	}

    public void Initialize() {
        remaining_percentage = 100;
    }

    public void TakenUp(BlockController block_controller) {
        carrier = block_controller;
        rigi.isKinematic = true;
    }

    public void PutDown(Vector2 throwForce) {
        rigi.isKinematic = false;
        rigi.AddForce(throwForce);
    }
	
	void Update () {

        var target_frame = (int)((100 - remaining_percentage) / 100 * (frames.Length));
        if (target_frame > frames.Length - 1) target_frame = frames.Length - 1;
        sprite_renderer.sprite = frames[target_frame];

        if (remaining_percentage <= 0) {
            Destroy(gameObject);
        }
	}

    void OnParticleCollision(GameObject other) {

        if (other.name == "RainFallParticleSystem") {
            remaining_percentage -= rain_deduction;
        }
    }
}
