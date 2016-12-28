﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float gravity = -9;
    public float move_force = 10;
    public float max_speed = 5;
    public float jump_force = 100;

    [HideInInspector] public bool facing_right = true;
    [HideInInspector] public bool is_jumping = false;
    [HideInInspector] public bool is_grounded = false;
    private Animator anim;
    private Rigidbody2D rigi;

    void Awake() {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    void Start () {

	}

	void Update () {

        if (Input.GetButtonDown("Jump") && is_grounded) {
            is_jumping = true;
            print("Is jumping");
        }
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
        the_scale *= -1;
        transform.localScale = the_scale;
    }

    void OnCollisionEnter2D(Collision2D coll) {

        print("Collision enter 2D");

        if (transform.position.y > coll.transform.position.y) {
            is_grounded = true;
        }


    }

    // void OnTriggerEnter2D()
}
