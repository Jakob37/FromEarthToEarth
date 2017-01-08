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

    void Awake() {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();

        level_logic = FindObjectOfType<LevelLogic>();
    }

    void Start () {
        remaining_jumps = air_jumps;
	}

	void Update () {

        if (Input.GetButtonDown("Jump")) {

            if (is_grounded) {
                is_jumping = true;
            }
            else if (remaining_jumps > 0) {
                is_jumping = true;
                remaining_jumps -= 1;
                rigi.velocity = new Vector2(rigi.velocity.x, 0);
            }
        }

        EdgeCheck();
	}

    void FixedUpdate() {

        float h = Input.GetAxis("Horizontal");
        if (h * rigi.velocity.x < max_speed) {
            rigi.AddForce(Vector2.right * h * move_force);
        }

        if (Mathf.Abs(rigi.velocity.x) > max_speed) {
            rigi.velocity = new Vector2(Mathf.Sign(rigi.velocity.x) * max_speed, rigi.velocity.y);
        }

        if (h > 0 && !facing_right) {
            Flip();
        }
        else if (h < 0 && facing_right) {
            Flip();
        }

        if (is_jumping) {
            rigi.AddForce(new Vector2(0f, jump_force));
            is_jumping = false;
            is_grounded = false;
        }
    }

    private void Flip() {
        facing_right = !facing_right;
        Vector3 the_scale = transform.localScale;
        the_scale.x *= -1;
        transform.localScale = the_scale;
    }

    void OnCollisionEnter2D(Collision2D coll) {

        // print("Collision enter 2D: " + coll.gameObject);

        ContactPoint2D[] contact_points = coll.contacts;
        ContactPoint2D contact_point = contact_points[0];
        Vector2 first_coll_coord = contact_point.point;

        print(coll.gameObject.GetComponent<WinArea>());

        if (coll.gameObject.GetComponent<WinArea>() != null) {
            print("Win condition!");
            level_logic.WinCondition();
        }

        //print("Collision pos: " + point);
        //print("Character pos: " + transform.position);

        if (transform.position.y > first_coll_coord.y) {
            is_grounded = true;
            remaining_jumps = air_jumps;
        }
    }

    private void EdgeCheck() {

        float edge_margin = 0.02f;

        var viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        viewport_pos.x = Mathf.Clamp(viewport_pos.x, edge_margin, 2);
        transform.position = Camera.main.ViewportToWorldPoint(viewport_pos);

        //if (viewport_pos.x > 1) {
        //    level_logic.WinCondition();
        //}
    }

    // void OnTriggerEnter2D()
}
