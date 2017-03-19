using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.LevelLogic {

    public enum LevelEventType {
        IsMoving,
        IsJumping,
        IsDoubleJumping,
        LiftingBlock,
        ThrowingBlock,
        MakingBlock,
        EndReached,
        TimeSinceStartPassed
    }

    class LevelEvent {

        private LevelEventCarrier level_event_carrier;
        public LevelEventType EventType { get { return level_event_carrier.event_type; } }
        public int EventParam { get { return level_event_carrier.nbr_param; } }

        private string param_splitter = "\\|";

        public LevelEvent(string event_string) {
            level_event_carrier = SetupEvent(event_string);
        }

        private LevelEventCarrier SetupEvent(string event_string) {

            string[] values = Regex.Split(event_string, param_splitter);
            string event_type;
            int event_param = 0;

            bool param_initialized = false;

            if (values.Length > 1) {

                event_type = values[0];
                event_param = Convert.ToInt32(values[1]);
                param_initialized = true;
            }
            else {
                event_type = event_string;
            }

            switch (event_type) {
                case "press arrow":
                    return new LevelEventCarrier(LevelEventType.IsMoving);
                case "press space":
                    return new LevelEventCarrier(LevelEventType.IsJumping);
                case "do midair_jump":
                    return new LevelEventCarrier(LevelEventType.IsDoubleJumping);
                case "do block_pickup":
                    return new LevelEventCarrier(LevelEventType.LiftingBlock);
                case "do make_block":
                    return new LevelEventCarrier(LevelEventType.MakingBlock);
                case "do throw_block":
                    return new LevelEventCarrier(LevelEventType.ThrowingBlock);
                case "do reach_end":
                    return new LevelEventCarrier(LevelEventType.EndReached);
                case "time":
                    if (!param_initialized) throw new ArgumentException("Param not initialized, but used anyway!");
                    return new LevelEventCarrier(LevelEventType.TimeSinceStartPassed, event_param);
                default:
                    throw new ArgumentException("Event string not recognized: " + event_string);
            }
        }

        public bool IsTriggered(List<LevelEventCarrier> occured_events) {

            foreach (LevelEventCarrier e in occured_events) {
                if (EventType == e.event_type && EventParam == e.nbr_param) {
                    return true;
                }
            }

            return false;
        }
    }
}
