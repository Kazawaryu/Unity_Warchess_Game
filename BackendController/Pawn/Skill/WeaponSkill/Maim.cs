using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.WeaponSkill
{
    public class Maim:Skill
    {
        public Maim(Pawn initiator) : base(initiator)
        {
            Range = 2;
            Cooldown = 1;
            CastTime = 0;
            APCost = 3;
            MPCost = 0;
            DamageType = DamageType.Physical;
            Damage = 15;
            Name = "Maim";
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

