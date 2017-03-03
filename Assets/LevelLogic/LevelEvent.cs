using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LevelLogic {
    class LevelEvent {

        private string event_text;
        private string level_text;

        public LevelEvent(string event_text, string level_text) {
            this.event_text = event_text;
            this.level_text = level_text;
        }

        public string GetLevelText() {
            return level_text;
        }

        public string GetEventTriggerText() {
            return event_text;
        }
    }
}
