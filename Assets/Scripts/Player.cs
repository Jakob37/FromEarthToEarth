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


    void Awake() {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();

        level_logic = FindObjectOfType<LevelLogic>();
        platform_controller = GetComponent<PlatformController>();
    }

    void Start () {
        remaining_jumps = air_jumps;
	}

    void FixedUpdate() {

        UpdatePlatformController();
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

        if (coll.gameObject.GetComponent<WinArea>() != null) {
            print("Win condition!");
            level_logic.WinCondition();
        }

        if (is_grounded) {
            remaining_jumps = air_jumps;
        }
    }
}
