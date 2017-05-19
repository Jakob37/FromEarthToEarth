using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts {
    class JumpInstance {

        private Player player;
        private float min_height;
        private float max_height;

        private Vector2 start_position;
        private bool peak_reached;
        private bool is_done;
        private bool jump_key_held;

        private bool IsPastMaxHeight() {
            return (player.transform.y - start_position.y) > max_height;
        }

        public JumpInstance(Player player, float min_height, float max_height) {

            this.player = player;
            this.start_position = (Vector2)player.transform.position;
            this.peak_reached = false;
            this.max_height = max_height;
            this.min_height = min_height;
            this.is_done = false;
            this.jump_key_held = true;
        }

        public void UpdateJump(bool jump_key_down) {

            if (is_done) {
                return;
            }

            if (player.is_grounded) {
                is_done = true;
            }

            if (IsPastMaxHeight()) {
                peak_reached = true;
            }

            if (!jump_key_down) {
                
            }
        }
    }
}
