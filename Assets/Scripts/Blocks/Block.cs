using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public bool is_fading = false;

    private Rigidbody2D rigi;
    private Collider2D coll;

    private SpriteRenderer sprite_renderer;

    public Sprite[] frames;

    private float remaining_percentage;

    private bool solidified;
    private bool is_solidifying;

    private float rain_deduction = 0.5f;
    public bool is_water_resistant = false;

    private Switch[] switches;

    public bool IsFadeInDone() {
        return this.sprite_renderer.color.a == 1f;
    }

    private void DisableBlock() {
        rigi.isKinematic = true;
        coll.enabled = false;
    }

    private void EnableBlock() {
        rigi.isKinematic = false;
        coll.enabled = true;
    }

    void Awake() {

        rigi = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();

        rigi.isKinematic = false;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        switches = FindObjectsOfType<Switch>();

        remaining_percentage = 100;
	}

    public void Initialize(int start_perc=100) {
        remaining_percentage = start_perc;
    }

    public void TakenUp(BlockController block_controller) {
        DisableBlock();
    }

    public void PutDown(Vector2 throwForce) {
        EnableBlock();
        rigi.AddForce(throwForce);
    }
	
    public void PutDownGently() {
        EnableBlock();
    }

    void Update () {

        var target_frame = (int)((100 - remaining_percentage) / 100 * (frames.Length));
        if (target_frame > frames.Length - 1) target_frame = frames.Length - 1;
        sprite_renderer.sprite = frames[target_frame];

        if (remaining_percentage <= 0) {
            Destroy(gameObject);
        }

        foreach (Switch target_switch in switches) {
            bool is_on_switch = target_switch.IsObjectOnSwitch(this.gameObject);
            if (is_on_switch) {
                target_switch.Press();
            }
        }
	}

    void OnParticleCollision(GameObject other) {

        if (!is_water_resistant && other.name == "RainFallParticleSystem") {
            remaining_percentage -= rain_deduction;
        }
    }
}
