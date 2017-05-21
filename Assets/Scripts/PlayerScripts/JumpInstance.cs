using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts {
    class JumpInstance {

        private Player player;
        private Rigidbody2D rigi;

        private float min_height;
        private float max_height;

        private Vector2 start_position;
        private bool peak_reached;
        private bool is_done;
        private bool jump_key_held;

        private int received_impulses;

        private bool debug_jump;

        private bool IsPastMaxHeight() {
            return GetCurrentJumpHeight() > max_height;
        }

        private bool IsPastMinHeight() {
            return GetCurrentJumpHeight() > min_height;
        }

        public float GetCurrentJumpHeight() {
            return player.transform.position.y - start_position.y;
        }

        public bool IsDone {
            get { return is_done; }
        }

        public JumpInstance(Player player, float min_height, float max_height, bool debug_jump=false) {

            this.player = player;
            this.rigi = player.GetComponent<Rigidbody2D>();

            this.start_position = (Vector2)player.transform.position;
            this.peak_reached = false;
            this.max_height = max_height;
            this.min_height = min_height;
            this.is_done = false;
            this.jump_key_held = true;

            this.debug_jump = debug_jump;
            received_impulses = 1;

            rigi.velocity = new Vector2(rigi.velocity.x, player.jump_force);   
        }

        public void UpdateJump(bool jump_key_down) {

            if (is_done) {
                return;
            }

            if (player.IsHeadHit) {
                if (debug_jump) Debug.Log("Head hit");
                is_done = true;
            }

            if (IsPastMinHeight() && player.IsGrounded) {
                if (debug_jump) Debug.Log("Grounded");
                is_done = true;
            }

            if (IsPastMaxHeight()) {

                if (debug_jump) {
                    Debug.Log("Past max height");
                    Debug.Log("Current pos: " + player.transform.position + ", impulses: " + received_impulses);
                }

                peak_reached = true;
                is_done = true;
            }

            if (!jump_key_down) {
                if (debug_jump) Debug.Log("Jump key up");
                is_done = true;
            }

            if (!IsPastMinHeight() || (jump_key_down && !IsPastMaxHeight())) {
                if (debug_jump) Debug.Log("Extend jump");
                rigi.velocity = new Vector2(rigi.velocity.x, player.jump_force);
                received_impulses += 1;
            }
        }
    }
}
