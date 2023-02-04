using System;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.Common
{
    public class UseItem : Skill
    {
        public UseItem(Pawn initiator) : base(initiator)
        {
        }

        public UseItem()
        {
            APCost = 2;
            MPCost = 0;
            Cooldown = 1;
            Range = 9999;
            Name = "Item";
            Damage = 0;
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}