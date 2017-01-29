using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float move_force = 10;
    public float max_speed = 5;
    public float max_speed_y = 7;
    public float jump_force = 5;

    public int air_jumps = 1;
    [HideInInspector] public int remaining_jumps;

    [HideInInspector] public bool facing_right = true;
    [HideInInspector] public bool is_jumping = false;
    [HideInInspector] public bool is_grounded = false;

    private LevelLogic level_logic;

    public Transform ground_check;
    public float ground_radius = 0.2f;
    public LayerMask what_is_ground;

    private PlatformController platform_controller;
    private BlockController block_controller;

    private int raindrop_hit_count;

    void Awake() {

        level_logic = FindObjectOfType<LevelLogic>();
        platform_controller = GetComponent<PlatformController>();
        block_controller = GetComponent<BlockController>();

        raindrop_hit_count = 0;
    }

    void Start () {
        remaining_jumps = air_jumps;
	}

    void FixedUpdate() {

        UpdatePlatformController();
        block_controller.UpdateController(Input.GetKeyDown(KeyCode.LeftControl), Input.GetKey(KeyCode.LeftControl));
    }

    private void UpdatePlatformController() {
        platform_controller.EdgeCheck();

        bool jump_key_down = Input.GetButtonDown("Jump");
        platform_controller.UpdateJump(jump_key_down);

        platform_controller.UpdateHorizontalMovement();
        is_grounded = platform_controller.CheckGrounded();

    }

    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.GetComponent<WinArea>() != null) {
            print("Win condition!");
            level_logic.WinCondition();
        }

        if (is_grounded) {
            remaining_jumps = air_jumps;
        }
    }

    void OnParticleCollision(GameObject other) {

        if (other.name == "RainFallParticleSystem") {
            raindrop_hit_count += 1;
        }
    }
}
