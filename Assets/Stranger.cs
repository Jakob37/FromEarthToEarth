using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stranger : MonoBehaviour {

    private Collider2D player_coll;
    private Collider2D coll;

    void Start() {
        player_coll = FindObjectOfType<Player>().gameObject.GetComponent<Collider2D>();
        coll = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.GetComponent<Player>() != null) {
            Physics2D.IgnoreCollision(collision.collider, coll);
        }
    }
}