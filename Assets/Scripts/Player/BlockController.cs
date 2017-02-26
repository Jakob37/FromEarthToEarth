using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public float throw_force_x = 100;
    public float throw_force_y = 150;

    private Player player;
    private Block carried_block;
    public Block CarriedBlock { get { return carried_block; } }

    private Block possible_pickup;

    private float pickup_dist = 0.1f;
    private float lift_distance = 0.7f;

    private float pickup_delay = 0.5f;
    private float current_pickup_delay;

    private float throw_delay = 0.2f;
    private float current_throw_delay;

    public GameObject block_prefab;

    public bool IsLiftingBlock() {
        return carried_block != null && !carried_block.IsFadeInDone();
    }

    public bool IsCarryingBlock() {
        return carried_block != null;
    }

	void Start() {
        player = GameObject.FindObjectOfType<Player>();
        current_pickup_delay = 0;
        current_throw_delay = 0;
	}
	
	public void UpdateController(bool control_pressed, bool control_down) {

        if (current_pickup_delay > 0) {
            current_pickup_delay -= Time.deltaTime;
        }

        if (current_throw_delay > 0) {
            current_throw_delay -= Time.deltaTime;
        }

        if (control_down) {
            if (possible_pickup != null && carried_block == null && current_pickup_delay <= 0) {
                carried_block = possible_pickup;
                carried_block.TakenUp(this);
                current_pickup_delay = pickup_delay;
                current_throw_delay = throw_delay;
            }
            else if (carried_block == null && current_pickup_delay <= 0 && player.IsBlockCreationGrounded()) {
                PickUpBlock();
                current_pickup_delay = pickup_delay;
                current_throw_delay = throw_delay;
            }
        }

        UpdatePotentialPickup();
        if (control_pressed) {
            if (carried_block != null && current_throw_delay <= 0 && carried_block.IsFadeInDone()) {
                ThrowBlock();
            }
        }

        if (carried_block != null) {
            CarryBlock();
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

    private void ThrowBlock() {

        var throw_dir = 1;
        if (!player.facing_right) {
            throw_dir = -1;
        }

        carried_block.PutDown(new Vector2(throw_dir * throw_force_x, throw_force_y));
        carried_block = null;
        current_pickup_delay = pickup_delay;
    }

    private void CarryBlock() {
        carried_block.transform.position = new Vector3(transform.position.x, transform.position.y + lift_distance, 0);
    }

    private void PickUpBlock() {

        carried_block = GetBlock();
        carried_block.TakenUp(this);
        CarryBlock();
    }

    public Block GetBlock() {
        GameObject block = GameObject.Instantiate(block_prefab);
        Block block_script = block.GetComponent<Block>();
        block_script.Initialize();
        return block_script;
    }
}
