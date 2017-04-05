using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public float move_force = 10;
    public float max_speed = 5;
    public float max_speed_y = 7;
    public float jump_force = 8;

    public int air_jumps = 0;
    [HideInInspector] public int remaining_jumps;

    [HideInInspector] public bool facing_right = true;
    [HideInInspector] public bool is_jumping = false;
    [HideInInspector] public bool is_grounded = false;

    private LevelLogic level_logic;

    public Transform ground_check;
    public Transform hands;

    public float ground_radius = 0.2f;
    public LayerMask what_is_ground;
    public LayerMask what_is_block_creation_ground;
    public LayerMask what_is_switch;

    private PlatformController platform_controller;
    private BlockController block_controller;

    private int raindrop_hit_count;

    private InfoText listener;

    private Switch[] switches;

    public bool test_frame_rate = false;
    public int testing_fps = 30;

    public bool IsBlockCreationGrounded() {
        return platform_controller.CheckBlockCreationGrounded();
    }

    public void AssignListener(InfoText info_text) {
        this.listener = info_text;
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
        raindrop_hit_count = 0;
    }

    void Start () {
        remaining_jumps = air_jumps;
	}

    void FixedUpdate() {

        if (!block_controller.IsLiftingBlock()) {
            UpdatePlatformController();
        }

        block_controller.UpdateController(Input.GetKeyDown(KeyCode.Space), 
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.DownArrow));
    }

    private void UpdatePlatformController() {
        platform_controller.EdgeCheck();

        // bool jump_key_down = Input.GetButtonDown("Jump");
        bool up_arrow_down = Input.GetKeyDown(KeyCode.UpArrow);
        bool up_arrow_press = Input.GetKey(KeyCode.UpArrow);
        
        platform_controller.UpdateJump(up_arrow_down, up_arrow_press);

        platform_controller.UpdateHorizontalMovement();
        is_grounded = platform_controller.CheckGrounded();

        foreach (Switch target_switch in switches) {
            bool is_on_switch = target_switch.IsObjectOnSwitch(this.ground_check.gameObject);
            if (is_on_switch) {
                target_switch.Press();
            }
            //platform_controller.TriggerSwitch(target_switch);
        }
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
