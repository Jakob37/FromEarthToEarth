using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LevelLogic {
    class LevelTextEntry {

        private string level_text;
        private LevelEvent next_text_event;

        public LevelTextEntry(string event_text, string level_text) {
            this.next_text_event = new LevelEvent(event_text);
            this.level_text = level_text;
        }

        public string GetLevelText() {
            return level_text;
        }

        public LevelEvent GetNextTextEvent() {
            return next_text_event;
        }

        public bool IsEventTriggered(List<LevelEventCarrier> occured_events) {
            return next_text_event.IsTriggered(occured_events);
        }
    }
}
