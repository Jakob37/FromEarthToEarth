using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public float move_force = 10;
    public float max_speed = 5;
    public float max_speed_y = 7;
    public float jump_force = 8;

    public int air_jumps = 0;

    private int remaining_jumps;
    public int RemainingJumps { get { return remaining_jumps; } }

    public bool facing_right = true;

    private bool is_jumping = false;
    public bool IsJumping { get { return is_jumping; } }

    private bool is_grounded = false;
    public bool IsGrounded { get { return is_grounded; } }

    private bool is_grounded_on_lever = false;
    public bool IsGroundedOnLever { get { return is_grounded_on_lever; } }

    private bool is_head_hit = false;
    public bool IsHeadHit { get { return is_head_hit; } }

    public bool IsCarryingBlock { get { return block_controller.CarriedBlock != null; } }

    private LevelLogic level_logic;

    public Transform ground_check;
    public Transform head_check;
    public Transform hands;

    public float ground_radius = 0.01f;
    public float head_radius = 0.01f;
    public LayerMask what_is_ground;
    public LayerMask what_is_head_ground;
    public LayerMask what_is_block_creation_ground;
    public LayerMask what_is_switch;
    public LayerMask what_is_block;
    public LayerMask what_is_lever;

    private PlatformController platform_controller;
    private BlockController block_controller;
    private int raindrop_hit_count;

    // private InfoText listener;

    private Switch[] switches;
    private StoryBoard current_story_board;

    public bool test_frame_rate = false;
    public int testing_fps = 30;

    private Rigidbody2D rigi;
    public Rigidbody2D Rigi { get {return rigi; }}

    public bool IsBlockCreationGrounded() {
        return platform_controller.CheckBlockCreationGrounded();
    }

    public void AssignListener(InfoText info_text) {
        // this.listener = info_text;
        platform_controller.AssignListener(info_text);
        block_controller.AssignListener(info_text);
    }

    void Awake() {

        if (test_frame_rate) {
            #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = testing_fps;
            #endif
        }

        level_logic = FindObjectOfType<LevelLogic>();
        platform_controller = GetComponent<PlatformController>();
        block_controller = GetComponent<BlockController>();
        switches = FindObjectsOfType<Switch>();

        rigi = GetComponent<Rigidbody2D>();
        raindrop_hit_count = 0;
    }

    void Start () {
        remaining_jumps = air_jumps;
	}

    void Update() {

        block_controller.UpdateController(Input.GetKeyDown(KeyCode.Space), 
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.DownArrow));

        if (!block_controller.IsLiftingBlock()) {
            UpdatePlatformController();
        }

        if (current_story_board != null && Input.GetKeyDown(KeyCode.DownArrow)) {
            current_story_board.IterateStoryBoard();
        }
    }

    private void UpdatePlatformController() {
        platform_controller.EdgeCheck();

        // bool jump_key_down = Input.GetButtonDown("Jump");
        bool up_arrow_down = Input.GetKeyDown(KeyCode.UpArrow);
        bool up_arrow_press = Input.GetKey(KeyCode.UpArrow);

        platform_controller.UpdateJump(up_arrow_down, up_arrow_press);

        platform_controller.UpdateHorizontalMovement();
        is_grounded = platform_controller.CheckGrounded();
        is_head_hit = platform_controller.CheckHeadHit();
        is_grounded_on_lever = platform_controller.CheckGroundedOnLever();

        foreach (Switch target_switch in switches) {
            bool is_on_switch = target_switch.IsObjectOnSwitch(this.ground_check.gameObject);
            if (is_on_switch) {
                target_switch.Press();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.GetComponent<WinArea>() != null) {
            print("Win condition!");
            level_logic.WinCondition();
        }

        if (IsGrounded) {
            remaining_jumps = air_jumps;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<StoryBoard>() != null) {
            current_story_board = coll.gameObject.GetComponent<StoryBoard>();
            coll.gameObject.GetComponent<StoryBoard>().ActivateStoryBoard();
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<StoryBoard>() != null) {
            coll.gameObject.GetComponent<StoryBoard>().DeactivateStoryBoard();
            current_story_board = null;
        }
    }

    void OnParticleCollision(GameObject other) {

        if (other.name == "RainFallParticleSystem") {
            raindrop_hit_count += 1;
        }
    }
}
