using UnityEngine;
using System.Collections;
using System;

public class PlatformController : MonoBehaviour {

    private Player player;
    private Rigidbody2D rigi;
    private Animator player_anim;

    private float JUMP_DELAY_MILLISECONDS = 10;
    private float current_jump_delay;

    private bool has_been_off_ground_since_jump;

    private InfoText listener;

    public void AssignListener(InfoText info_text) {
        this.listener = info_text;
    }

    private void DispatchEvent(Assets.LevelLogic.LevelEventType occured_event) {
        if (listener != null) {
            listener.DispatchEvent(occured_event);
        }
    }

    public bool IsMoving() {
        return rigi.velocity.x != 0;
    }

    void Start() {
        player = FindObjectOfType<Player>();
        rigi = player.gameObject.GetComponent<Rigidbody2D>();
        player_anim = player.gameObject.GetComponent<Animator>();

        current_jump_delay = 0;
    }

    public void EdgeCheck() {

        float edge_margin = 0.02f;

        var viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        viewport_pos.x = Mathf.Clamp(viewport_pos.x, edge_margin, 2);
        transform.position = Camera.main.ViewportToWorldPoint(viewport_pos);
    }

    void FixedUpdate() {
        if (current_jump_delay > 0) {
            current_jump_delay -= Time.deltaTime * 1000;
        }
    }

    public void UpdateJump(bool jump_key_down) {

        if (jump_key_down) {
            if (player.is_grounded) {
                player.is_jumping = true;
                DispatchEvent(Assets.LevelLogic.LevelEventType.IsJumping);
            }
            else if (player.remaining_jumps > 0) {
                player.is_jumping = true;
                player.remaining_jumps -= 1;
                rigi.velocity = new Vector2(rigi.velocity.x, 0);
                DispatchEvent(Assets.LevelLogic.LevelEventType.IsDoubleJumping);
            }
        }

        if (Mathf.Abs(rigi.velocity.y) > player.max_speed_y) {
            rigi.velocity = new Vector2(rigi.velocity.x, Mathf.Sign(rigi.velocity.x) * player.max_speed_y);
        }

        SetAnimParams();

        if (player.is_jumping) {

            rigi.velocity = new Vector2(rigi.velocity.x, player.jump_force);
            player.is_jumping = false;
            player.is_grounded = false;

            current_jump_delay = JUMP_DELAY_MILLISECONDS;
        }
    }

    private void SetAnimParams() {
        player_anim.SetBool("is_jumping", player.is_jumping);
        player_anim.SetFloat("move_speed", Mathf.Abs(rigi.velocity.x));
    }

    public void UpdateHorizontalMovement() {

        if (Input.GetButtonDown("Horizontal")) {
            DispatchEvent(Assets.LevelLogic.LevelEventType.IsMoving);
        }

        float h = Input.GetAxis("Horizontal");
        if (h * rigi.velocity.x < player.max_speed) {
            rigi.AddForce(Vector2.right * h * player.move_force);
        }
        if (Mathf.Abs(rigi.velocity.x) > player.max_speed) {
            rigi.velocity = new Vector2(Mathf.Sign(rigi.velocity.x) * player.max_speed, rigi.velocity.y);
        }

        if (h > 0 && !player.facing_right) {
            Flip();
        }
        else if (h < 0 && player.facing_right) {
            Flip();
        }
    }

    public bool CheckGrounded() {
        return Physics2D.OverlapCircle(player.ground_check.position, player.ground_radius, player.what_is_ground);
    }

    public bool CheckBlockCreationGrounded() {
        return Physics2D.OverlapCircle(player.ground_check.position, player.ground_radius, player.what_is_block_creation_ground);
    }

    public void Flip() {
        player.facing_right = !player.facing_right;
        Vector3 the_scale = transform.localScale;
        the_scale.x *= -1;
        transform.localScale = the_scale;
    }

}
