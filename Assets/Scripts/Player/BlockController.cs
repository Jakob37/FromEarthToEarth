using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public float throw_force_x = 100;
    public float throw_force_y = 150;

    private Player player;
    private Block carried_block;
    public Block CarriedBlock { get { return carried_block; } }

    private BlockCreationGround touching_ground;
    private Block possible_pickup;

    private float pickup_dist = 0.1f;
    private float lift_distance = 0.7f;

    private float pickup_delay = 1f;
    private float current_pickup_delay;

    private float throw_delay = 0.5f;
    private float current_throw_delay;

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
            if (possible_pickup != null && carried_block == null) {
                carried_block = possible_pickup;
                carried_block.TakenUp(this);
                current_pickup_delay = pickup_delay;
                current_throw_delay = throw_delay;
            }
            else if (touching_ground != null && carried_block == null && current_pickup_delay <= 0) {
                PickUpBlock();
                current_pickup_delay = pickup_delay;
                current_throw_delay = throw_delay;
            }
        }

        UpdatePotentialPickup();
        if (control_pressed) {
            if (carried_block != null && current_throw_delay <= 0) {
                ThrowBlock();
            }
        }

        if (carried_block != null) {
            CarryBlock();
        }
    }

    private void UpdatePotentialPickup() {
        var current_target = Physics2D.OverlapCircle(player.hands.position, pickup_dist, player.what_is_ground);
        if (current_target != null && current_target.GetComponent<Block>() != null) {
            possible_pickup = current_target.GetComponent<Block>();
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
    }

    private void CarryBlock() {
        carried_block.transform.position = new Vector3(transform.position.x, transform.position.y + lift_distance, 0);
    }

    private void PickUpBlock() {

        BlockCreationGround ground_script = touching_ground.gameObject.GetComponent<BlockCreationGround>();
        carried_block = ground_script.GetBlock();
        carried_block.TakenUp(this);
        CarryBlock();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.GetComponent<BlockCreationGround>() != null) {
            touching_ground = other.gameObject.GetComponent<BlockCreationGround>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if (touching_ground != null && other == touching_ground.gameObject.GetComponent<Collider2D>()) {
            touching_ground = null;
        }
    }
}
