﻿using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.PlayerScripts;

public class PlatformController : MonoBehaviour {

    public bool is_debugging;

    public float max_jump_height;
    public float min_jump_height;

    public float max_block_jump_height;
    public float min_block_jump_height;

    public bool abrupt_stop;

    private Player player;
    private Rigidbody2D rigi;
    private Animator player_anim;

    public float high_jump_delay = 0.2f;
    private float high_jump_press_duration;
    private float time_since_jump;

    public float block_height;

    public bool debug_print;

    private bool has_been_off_ground_since_jump;

    private InfoText listener;
    private JumpInstance current_jump;

    private SoundEffectManager sound_manager;

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

        high_jump_press_duration = 0;
        time_since_jump = 0;
        sound_manager = FindObjectOfType<SoundEffectManager>();
    }

    public void EdgeCheck() {

        float edge_margin = 0.02f;

        var viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        viewport_pos.x = Mathf.Clamp(viewport_pos.x, edge_margin, 2);
        transform.position = Camera.main.ViewportToWorldPoint(viewport_pos);
    }

    public void UpdateJump(bool jump_key_down, bool jump_key_press) {

        if (debug_print) {
            print(player.IsGrounded);
        }

        if (jump_key_down && player.IsGrounded) {

            sound_manager.PlaySound(SoundEffect.jump);

            if (!player.IsCarryingBlock) {
                current_jump = new JumpInstance(player, min_jump_height, max_jump_height, debug_jump: is_debugging);
            }
            else {
                current_jump = new JumpInstance(player, min_block_jump_height, max_block_jump_height, debug_jump: is_debugging);
            }
        }

        if (current_jump != null) {
            current_jump.UpdateJump(jump_key_press);
        }
    }

    private void UpdateExtendedJump(bool jump_key_press) {

        if (jump_key_press && time_since_jump < high_jump_delay) {
            high_jump_press_duration += Time.deltaTime;
        }
        else {

            high_jump_press_duration = 0;
            time_since_jump = int.MaxValue;
        }
    }

    public void SetAnimParams() {
        player_anim.SetBool("is_jumping", player.IsJumping);
        player_anim.SetFloat("move_speed", Mathf.Abs(rigi.velocity.x));
    }

    public void UpdateHorizontalMovement() {

        if (Input.GetButtonDown("Horizontal")) {
            DispatchEvent(Assets.LevelLogic.LevelEventType.IsMoving);
        }

        float h = Input.GetAxis("Horizontal");
        if (h == 0) {
            if (abrupt_stop || player.IsGroundedOnLever) {
                rigi.velocity = new Vector3(0, rigi.velocity.y);
            }
        }
        else if (h * rigi.velocity.x < player.max_speed) {
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

    public bool CheckHeadHit() {

        if (!player.IsCarryingBlock) {
            return Physics2D.OverlapCircle(player.head_check.position, player.head_radius, player.what_is_head_ground);
        }
        else {
            Vector3 corner_offset = new Vector3(0.2f, 0.1f);
            bool overlapping = Physics2D.OverlapArea(player.box_head_check.position - corner_offset,
                player.box_head_check.position + corner_offset,
                player.what_is_head_ground);

            return overlapping;
        }
    }

    public bool CheckBlockCreationGrounded() {
        return Physics2D.OverlapCircle(player.ground_check.position, player.ground_radius, player.what_is_block_creation_ground);
    }

    public bool CheckGroundedOnLever() {
        return Physics2D.OverlapCircle(player.ground_check.position, player.ground_radius, player.what_is_lever);
    }

    public bool IsHandsInGround() {
        return Physics2D.OverlapCircle(player.hands.position, player.hand_radius, player.what_is_ground);
    }

    public void Flip() {
        player.facing_right = !player.facing_right;
        Vector3 the_scale = transform.localScale;
        the_scale.x *= -1;
        transform.localScale = the_scale;
    }
}
