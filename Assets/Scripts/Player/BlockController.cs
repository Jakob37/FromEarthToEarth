﻿using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public float throw_speed_x = 1f;
    public float throw_speed_y = 1.5f;
    public float put_down_dist = 0.1f;

    private Player player;
    private Block carried_block;
    public Block CarriedBlock { get { return carried_block; } }

    private bool use_delays = true;

    private float pickup_dist = 0.1f;
    private float lift_distance = 0.6f;

    private float pickup_delay = 0.5f;
    private float current_pickup_delay;

    private float throw_delay = 0.2f;
    private float current_throw_delay;

    public int default_block_life = 100;

    public GameObject block_prefab;

    private bool is_making_block;
    private InfoText listener;
    private SoundEffectManager sound_manager;

    private BoxCollider2D box_coll;

    public bool IsLiftingBlock() {
        return carried_block != null && !carried_block.IsFadeInDone();
    }

    public bool IsThrowingBlock() {
        return current_throw_delay > 0;
    }

    public bool IsMakingBlock() {
        return is_making_block;
    }

    public bool IsCarryingBlock() {
        return carried_block != null;
    }

    void Awake() {
        player = GameObject.FindObjectOfType<Player>();
        box_coll = player.gameObject.GetComponent<BoxCollider2D>();
    }

    void Start() {
        current_pickup_delay = 0;
        current_throw_delay = 0;
        sound_manager = FindObjectOfType<SoundEffectManager>();
        DeactivateBlockCarry();
    }

    public void AssignListener(InfoText info_text) {
        listener = info_text;
    }

    private void DispatchEvent(Assets.LevelLogic.LevelEventType occured_event) {
        if (listener != null) {
            listener.DispatchEvent(occured_event);
        }
    }

    public void UpdateController(bool throw_button_pressed, bool button_down, bool key_down_held) {

        is_making_block = false;
        
        if (current_pickup_delay > 0) {
            current_pickup_delay -= Time.deltaTime;
        }
        
        if (current_throw_delay > 0) {
            current_throw_delay -= Time.deltaTime;
        }
        
        Block possible_pickup = GetPotentialPickup();
        
        if (button_down) {
            if (possible_pickup != null && carried_block == null && current_pickup_delay <= 0) {
                sound_manager.PlaySound(SoundEffect.pickup_block);
                LiftBlock(possible_pickup);
            }
            else if (carried_block == null && current_pickup_delay <= 0 && player.IsBlockCreationGrounded()) {
                sound_manager.PlaySound(SoundEffect.make_block);
                MakeBlockFromGround();
            }
        }
        
        if (throw_button_pressed) {
            if (carried_block != null && current_throw_delay <= 0 && carried_block.IsFadeInDone()) {
                sound_manager.PlaySound(SoundEffect.throw_block);
                ThrowBlock(key_down_held);
                DispatchEvent(Assets.LevelLogic.LevelEventType.ThrowingBlock);
            }
        }
        
        if (carried_block != null) {
            CarryBlock();
        }
    }

    private void LiftBlock(Block possible_pickup) {

        carried_block = possible_pickup;
        carried_block.TakenUp(this);
        is_making_block = true;
        DispatchEvent(Assets.LevelLogic.LevelEventType.LiftingBlock);

        ActivateBlockCarry();

        if (use_delays) {
            current_pickup_delay = pickup_delay;
            current_throw_delay = throw_delay;
        }
    }

    private Block GetPotentialPickup() {

        Block potential_pickup;

        var current_target = Physics2D.OverlapCircle(player.hands.position, pickup_dist, player.what_is_block);

        if (current_target != null && current_target.gameObject.GetComponent<Block>() != null) {
            potential_pickup = current_target.gameObject.GetComponent<Block>();
        }
        else {
            potential_pickup = null;
        }

        return potential_pickup;
    }

    private void ThrowBlock(bool key_down_held) {

        DeactivateBlockCarry();

        var throw_dir = 1;
        if (!player.facing_right) {
            throw_dir = -1;
        }

        if (key_down_held) {
            if (!player.IsHandsInGround) {
                Vector3 y_offset = new Vector3(0, 0.2f, 0);
                carried_block.PutDownGently();
                carried_block.transform.position = player.hands.position + y_offset + new Vector3(put_down_dist * throw_dir, 0, 0);
            }
            else {
                carried_block.PutDown(new Vector2(throw_dir * throw_speed_x, 0), player.Rigi.velocity);
            }
        }
        else {
            carried_block.PutDown(new Vector2(throw_dir * throw_speed_x, throw_speed_y), player.Rigi.velocity);
        }

        carried_block = null;

        if (use_delays) {
            current_pickup_delay = pickup_delay;
        }
    }

    private void CarryBlock() {
        ActivateBlockCarry();
        carried_block.transform.position = new Vector3(transform.position.x, transform.position.y + lift_distance, 0);
    }

    private void MakeBlockFromGround() {

        DispatchEvent(Assets.LevelLogic.LevelEventType.MakingBlock);
        carried_block = GetBlock();
        carried_block.TakenUp(this);
        CarryBlock();

        if (use_delays) {
            current_pickup_delay = pickup_delay;
            current_throw_delay = throw_delay;
        }
    }

    public Block GetBlock() {
        GameObject block = GameObject.Instantiate(block_prefab);
        Block block_script = block.GetComponent<Block>();
        return block_script;
    }

    public void ActivateBlockCarry() {
        box_coll.enabled = true;
    }

    private void DeactivateBlockCarry() {
        box_coll.enabled = false;
    }

    public void DoRainHit() {
        if (carried_block != null) {
            carried_block.RainReduceBlock();
        }
    }
}
