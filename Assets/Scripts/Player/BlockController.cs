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


    public bool IsCarryingBlock() {
        return carried_block != null;
    }

    public bool IsCarriedBlockSolidified() {

        if (!IsCarryingBlock()) {
            return false;
        }

        return carried_block.IsSolidified();
    }

	void Start() {
        player = GameObject.FindObjectOfType<Player>();
	}
	
	public void UpdateController(bool control_pressed, bool control_down) {

        UpdatePotentialPickup();
        if (control_pressed) {
            if (carried_block != null && carried_block.IsSolidified()) {
                ThrowBlock();
            }
            else if (possible_pickup != null) {
                carried_block = possible_pickup;
                carried_block.TakenUp(this);
            }
            else if (touching_ground != null) {
                PickUpBlock();
            }
        }

        if (carried_block != null) {
            CarryBlock();
        }

        if (control_down && IsCarryingBlock() && !IsCarriedBlockSolidified()) {
            SolidifyBlock();
        }
        else if (!control_down && IsCarryingBlock()) {
            carried_block.StopSolidifying();
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
    }

    private void SolidifyBlock() {
        carried_block.Solidify();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        // if (coll.gameObject.GetComponent<Block>() != null && Input.GetKey(KeyCode.LeftControl)) {
        //     carried_block = coll.gameObject.GetComponent<Block>();
        //     carried_block.TakenUp();
        // }
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
