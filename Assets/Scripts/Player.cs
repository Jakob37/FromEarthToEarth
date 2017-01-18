using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float move_force = 10;
    public float max_speed = 5;
    public float jump_force = 100;

    public int air_jumps = 1;
    [HideInInspector] public int remaining_jumps;

    [HideInInspector] public bool facing_right = true;
    [HideInInspector] public bool is_jumping = false;
    [HideInInspector] public bool is_grounded = false;
    private Animator anim;
    private Rigidbody2D rigi;

    private LevelLogic level_logic;

    public Transform ground_check;
    public float ground_radius = 0.2f;
    public LayerMask what_is_ground;

    private PlatformController platform_controller;

    public float throw_force_x = 100;
    public float throw_force_y = 150;

    private Block carried_block;
    private BlockCreationGround touching_ground;

    private int raindrop_hit_count;


    void Awake() {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();

        level_logic = FindObjectOfType<LevelLogic>();
        platform_controller = GetComponent<PlatformController>();

        raindrop_hit_count = 0;
    }

    void Start () {
        remaining_jumps = air_jumps;
	}

    void FixedUpdate() {

        UpdatePlatformController();

        if (Input.GetKeyDown(KeyCode.LeftControl) && carried_block != null) {
            var throw_dir = 1;
            if (!facing_right) {
                throw_dir = -1;
            }

            carried_block.PutDown(new Vector2(throw_dir * throw_force_x, throw_force_y));
            carried_block = null;
        }

        if (carried_block != null) {
            var lift_distance = 0.6f;
            carried_block.transform.position = new Vector3(transform.position.x, transform.position.y + lift_distance, 0);
        }

        if (touching_ground != null && Input.GetKey(KeyCode.LeftControl) && carried_block == null) {
            BlockCreationGround ground_script = touching_ground.gameObject.GetComponent<BlockCreationGround>();
            carried_block = ground_script.GetBlock();
            carried_block.TakenUp();
            carried_block.Solidify();
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && carried_block != null && !carried_block.IsSolidified()) {
            carried_block.Solidify();
        }

    }

    private void UpdatePlatformController() {
        platform_controller.EdgeCheck();

        if (Input.GetButtonDown("Jump")) {
            platform_controller.UpdateJump();
        }

        platform_controller.UpdateHorizontalMovement();
        is_grounded = platform_controller.CheckGrounded();

    }

    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.GetComponent<Block>() != null && Input.GetKey(KeyCode.LeftControl)) {
            carried_block = coll.gameObject.GetComponent<Block>();
            carried_block.TakenUp();
        }

        if (coll.gameObject.GetComponent<WinArea>() != null) {
            print("Win condition!");
            level_logic.WinCondition();
        }

        if (is_grounded) {
            remaining_jumps = air_jumps;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        print("Triggered!");

        if (other.gameObject.GetComponent<BlockCreationGround>() != null) {
            touching_ground = other.gameObject.GetComponent<BlockCreationGround>();
            print("Touching ground!");
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        print("Untriggered!");

        if (touching_ground != null && other == touching_ground.gameObject.GetComponent<Collider2D>()) {
            touching_ground = null;
            print("Not touching anymore!");
        }
    }

    void OnParticleCollision(GameObject other) {

        if (other.name == "RainFallParticleSystem") {
            raindrop_hit_count += 1;

            if (raindrop_hit_count % 5 == 0) {
                print("Hit by: " + raindrop_hit_count + " raindrops");
            }
        }
    }
}
