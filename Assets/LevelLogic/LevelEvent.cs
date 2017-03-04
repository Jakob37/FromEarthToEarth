using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LevelLogic {

    public enum LevelEventType {
        IsMoving,
        IsJumping,
        IsDoubleJumping,
        LiftingBlock,
        ThrowingBlock,
        MakingBlock,
        EndReached
    }

    class LevelEvent {

        private LevelEventType event_type;

        public LevelEvent(string event_string) {
            event_type = SetupEvent(event_string);
        }

        private LevelEventType SetupEvent(string event_string) {
            switch (event_string) {
                case "press arrow":
                    return LevelEventType.IsMoving;
                case "press space":
                    return LevelEventType.IsJumping;
                case "do midair_jump":
                    return LevelEventType.IsDoubleJumping;
                case "do block_pickup":
                    return LevelEventType.LiftingBlock;
                case "do make_block":
                    return LevelEventType.MakingBlock;
                case "do throw_block":
                    return LevelEventType.ThrowingBlock;
                case "do reach_end":
                    return LevelEventType.EndReached;
                default:
                    throw new ArgumentException("Event string not recognized: " + event_string);
            }
        }

        public bool IsTriggered(List<LevelEventType> occured_events) {

            return occured_events.Contains(event_type);

            //switch (event_type) {
            //    case LevelEventType.IsMoving:
            //        return player.IsHorizontalMovePressed();
            //    case LevelEventType.IsJumping:
            //        return player.IsJumpButtonPressed();
            //    case LevelEventType.IsDoubleJumping:
            //        return player.IsMidairJumping();
            //    case LevelEventType.LiftingBlock:
            //        return player.IsLiftingBlock();
            //    case LevelEventType.MakingBlock:
            //        return player.IsMakingBlock();
            //    case LevelEventType.ThrowingBlock:
            //        return player.IsThrowingBlock();
            //    case LevelEventType.EndReached:
            //        return false;
            //    default:
            //        throw new ArgumentException("LevelEventType instance not recognized: " + event_type);
            //}
        }
    }
}
