using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public float throw_force_x = 100;
    public float throw_force_y = 150;
    public float put_down_dist = 0.1f;

    private Player player;
    private Block carried_block;
    public Block CarriedBlock { get { return carried_block; } }

    private Block possible_pickup;

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

	void Start() {
        player = GameObject.FindObjectOfType<Player>();
        current_pickup_delay = 0;
        current_throw_delay = 0;
	}

    public void AssignListener(InfoText info_text) {
        listener = info_text;
    }

    private void DispatchEvent(Assets.LevelLogic.LevelEventType occured_event) {
        if (listener != null) {
            listener.DispatchEvent(occured_event);
        }
    }

    public void UpdateController(bool control_pressed, bool control_down, bool key_down_held) {

        is_making_block = false;

        if (current_pickup_delay > 0) {
            current_pickup_delay -= Time.deltaTime;
        }

        if (current_throw_delay > 0) {
            current_throw_delay -= Time.deltaTime;
        }

        if (control_down) {
            if (possible_pickup != null && carried_block == null && current_pickup_delay <= 0) {
                LiftBlock();
            }
            else if (carried_block == null && current_pickup_delay <= 0 && player.IsBlockCreationGrounded()) {
                PickUpBlock();
            }
        }

        UpdatePotentialPickup();
        if (control_pressed) {
            if (carried_block != null && current_throw_delay <= 0 && carried_block.IsFadeInDone()) {
                ThrowBlock(key_down_held);
                DispatchEvent(Assets.LevelLogic.LevelEventType.ThrowingBlock);
            }
        }

        if (carried_block != null) {
            CarryBlock();
        }
    }

    private void LiftBlock() {
        carried_block = possible_pickup;
        carried_block.TakenUp(this);
        is_making_block = true;
        DispatchEvent(Assets.LevelLogic.LevelEventType.LiftingBlock);

        if (use_delays) {
            current_pickup_delay = pickup_delay;
            current_throw_delay = throw_delay;
        }
    }

    private void UpdatePotentialPickup() {

        var current_target = Physics2D.OverlapCircle(player.hands.position, pickup_dist, player.what_is_ground);

        if (current_target != null && current_target.gameObject.GetComponent<Block>() != null) {
            possible_pickup = current_target.gameObject.GetComponent<Block>();
        }
        else {
            possible_pickup = null;
        }
    }

    private void ThrowBlock(bool key_down_held) {

        var throw_dir = 1;
        if (!player.facing_right) {
            throw_dir = -1;
        }

        if (!key_down_held) {
            carried_block.PutDown(new Vector2(throw_dir * throw_force_x, throw_force_y));
        }
        else {
            carried_block.PutDownGently();
            carried_block.transform.position = player.hands.position + new Vector3(put_down_dist * throw_dir, 0, 0);
        }

        carried_block = null;

        if (use_delays) {
            current_pickup_delay = pickup_delay;
        }
    }

    private void CarryBlock() {
        carried_block.transform.position = new Vector3(transform.position.x, transform.position.y + lift_distance, 0);
    }

    private void PickUpBlock() {

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
        block_script.Initialize(start_perc:default_block_life);
        return block_script;
    }
}
