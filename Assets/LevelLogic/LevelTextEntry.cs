using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LevelLogic {
    class LevelTextEntry {

        private string level_text;
        private LevelEvent next_text_event;

        public LevelTextEntry(string event_text, string level_text, Player player) {
            this.next_text_event = new LevelEvent(event_text, player);
            this.level_text = level_text;
        }

        public string GetLevelText() {
            return level_text;
        }

        public LevelEvent GetNextTextEvent() {
            return next_text_event;
        }

        public bool IsEventTriggered() {
            return next_text_event.IsTriggered();
        }
    }
}
