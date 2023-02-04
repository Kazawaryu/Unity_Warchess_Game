using System;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.Common
{
    public class Move : Skill
    {
        public Move(Pawn initiator) : base(initiator)
        {
            APCost = 1;
            Name = "Move";
            MPCost = 0;
        }

        public override Tuple<int, string> Cast(Pawn initiator, int value)
        {
            APCost = value;
            var temp = SettleAction.Instance.SettleSkill(this, initiator, initiator);
            APCost = 1;
            CombatTracker.Instance.GetNewLog();
            return temp;
        }
        
    }
}

