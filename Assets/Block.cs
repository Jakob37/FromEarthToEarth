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

    private float SOLIDIFYING_SPEED = 50;

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

    public void TakenUp() {
        rigi.isKinematic = true;
    }

    public void PutDown(Vector2 throwForce) {
        rigi.isKinematic = false;
        rigi.AddForce(throwForce);
    }
	
	void Update () {

        if (!solidified && is_solidifying) {
            is_solidifying = false;
            if (remaining_percentage >= 100) {
                solidified = true;
            }
        }

        remaining_percentage = Mathf.Clamp(remaining_percentage, 0, 100);

        var target_frame = (int)((100 - remaining_percentage) / 100 * 7);
        sprite_renderer.sprite = frames[target_frame];
	}

    void OnParticleCollision(GameObject other) {


        if (other.name == "RainFallParticleSystem") {
            if (solidified) {
                remaining_percentage -= 1;
            }
        }
    }
}
