using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LevelLogic {
    public class LevelEventCarrier {

        public LevelEventType event_type;
        public int nbr_param;

        public LevelEventCarrier(LevelEventType event_type, int nbr_param = 0) {
            this.event_type = event_type;
            this.nbr_param = nbr_param;
        }
    }
}
