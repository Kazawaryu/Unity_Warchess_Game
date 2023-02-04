using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.Magic
{
    public class MagicArrow : Skill
    {
        public MagicArrow(Pawn initiator) : base(initiator)
        {
            Name = "Magic Arrow";
            APCost = 2;
            Damage = 10;
            MPCost = 10;
            Range = 3;
            DamageType = DamageType.Arcane;
            Type = SkillType.SingleEnemy;
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver,out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}