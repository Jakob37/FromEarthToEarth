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

	void Awake() {

        rigi = gameObject.GetComponent<Rigidbody2D>();

        rigi.isKinematic = false;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        remaining_percentage = 10;

        solidified = false;
        is_solidifying = false;
	}

    public bool IsSolidified() {
        return solidified;
    }

    public void Solidify() {

        remaining_percentage += Time.deltaTime * SOLIDIFYING_SPEED;
        is_solidifying = true;
    }

    public void StopSolidifying() {
        is_solidifying = false;
    }

    public void TakenUp() {
        rigi.isKinematic = true;
    }

    public void PutDown(Vector2 throwForce) {
        rigi.isKinematic = false;
        rigi.AddForce(throwForce);
    }
	
	void Update () {

        if (!solidified && !is_solidifying) {
            remaining_percentage = 0;
        }

        if (!solidified && is_solidifying) {
            if (remaining_percentage >= 100) {
                solidified = true;
                is_solidifying = false;
            }
        }

        remaining_percentage = Mathf.Clamp(remaining_percentage, 0, 100);

        var target_frame = (int)((100 - remaining_percentage) / 100 * 7);
        sprite_renderer.sprite = frames[target_frame];

        if (remaining_percentage <= 0) {
            Destroy(gameObject);
        }
	}

    void OnParticleCollision(GameObject other) {


        if (other.name == "RainFallParticleSystem") {
            if (solidified) {
                remaining_percentage -= 1;
            }
        }
    }
}
