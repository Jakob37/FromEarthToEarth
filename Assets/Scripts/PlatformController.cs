using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {


    private Player player;
    private Rigidbody2D rigi;

    private float JUMP_DELAY_MILLISECONDS = 100;
    private float current_jump_delay;

    void Start() {
        player = FindObjectOfType<Player>();
        rigi = player.gameObject.GetComponent<Rigidbody2D>();

        current_jump_delay = 0;
    }

    public void EdgeCheck() {

        float edge_margin = 0.02f;

        var viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        viewport_pos.x = Mathf.Clamp(viewport_pos.x, edge_margin, 2);
        transform.position = Camera.main.ViewportToWorldPoint(viewport_pos);
    }

    void FixedUpdate() {
        if (current_jump_delay > 0) {
            current_jump_delay -= Time.deltaTime * 1000;
        }
    }

    public void UpdateJump() {

        if (player.is_grounded) {
            player.is_jumping = true;
        }
        else if (player.remaining_jumps > 0) {
            player.is_jumping = true;
            player.remaining_jumps -= 1;
            rigi.velocity = new Vector2(rigi.velocity.x, 0);
        }

        if (player.is_jumping && current_jump_delay <= 0) {
            rigi.AddForce(new Vector2(0f, player.jump_force));
            player.is_jumping = false;
            player.is_grounded = false;

            current_jump_delay = JUMP_DELAY_MILLISECONDS;
        }
    }

    public void UpdateHorizontalMovement() {

        float h = Input.GetAxis("Horizontal");
        if (h * rigi.velocity.x < player.max_speed) {
            rigi.AddForce(Vector2.right * h * player.move_force);
        }
        if (Mathf.Abs(rigi.velocity.x) > player.max_speed) {
            rigi.velocity = new Vector2(Mathf.Sign(rigi.velocity.x) * player.max_speed, rigi.velocity.y);
        }

        if (h > 0 && !player.facing_right) {
            Flip();
        }
        else if (h < 0 && player.facing_right) {
            Flip();
        }
    }

    public bool CheckGrounded() {

        return Physics2D.OverlapCircle(player.ground_check.position, player.ground_radius, player.what_is_ground);
    }

    public void Flip() {
        player.facing_right = !player.facing_right;
        Vector3 the_scale = transform.localScale;
        the_scale.x *= -1;
        transform.localScale = the_scale;
    }

}
