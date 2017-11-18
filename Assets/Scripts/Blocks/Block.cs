using UnityEngine;
using System.Collections;
using DigitalRuby.RainMaker;

public class Block : MonoBehaviour {

    public bool is_fading = false;

    private Rigidbody2D rigi;
    private Collider2D coll;

    private SpriteRenderer sprite_renderer;

    public Sprite[] frames;
    public Sprite waterproof_sprite;

    private float remaining_percentage;

    private bool solidified;
    private bool is_solidifying;

    public float pour_modifier = 10f;

    public float default_rain_deduction = 0.5f;
    private float rain_deduction;
    public bool is_water_resistant = false;

    private Switch[] switches;

    private BaseRainScript base_rain_script;

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

        base_rain_script = FindObjectOfType<BaseRainScript>();
    }

    // public void Initialize(int start_perc=100) {

        // remaining_percentage = start_perc;
        // float perc_frac = 100 / start_perc;
        // rain_deduction = default_rain_deduction / perc_frac;
        // print(default_rain_deduction);
        // print(perc_frac);
        // print(rain_deduction);
    //}

    public void TakenUp(BlockController block_controller) {
        DisableBlock();
    }

    public void PutDown(Vector2 throwForce, Vector2 player_movement) {

        EnableBlock();
        float x_vel = throwForce.x + player_movement.x * 0.5f;
        float y_vel = throwForce.y + player_movement.y * 0.25f;
        rigi.velocity = new Vector2(x_vel, y_vel);
    }
	
    public void PutDownGently() {
        EnableBlock();
    }

    void Update () {

        if (!is_water_resistant) {
            var target_frame = (int)((100 - remaining_percentage) / 100 * (frames.Length));
            if (target_frame > frames.Length - 1) {
                target_frame = frames.Length - 1;
            }
            sprite_renderer.sprite = frames[target_frame];
        }
        else {
            sprite_renderer.sprite = waterproof_sprite;
        }

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

            bool is_pour_active = base_rain_script.intensity_modifier_active;

            float modifier = 1f;
            if (is_pour_active) {
                modifier = pour_modifier;
            }

            remaining_percentage -= default_rain_deduction * modifier;
        }
    }
}
