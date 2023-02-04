using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.WeaponSkill
{
    public class StraightShot : Skill
    {
        public StraightShot(Pawn initiator) : base(initiator)
        {
            Range = 4;
            Cooldown = 1;
            CastTime = 0;
            APCost = 2;
            MPCost = 0;
            DamageType = DamageType.Physical;
            Damage = 8;
            Name = "Straight Shot";
            Type = SkillType.SingleEnemy;
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}