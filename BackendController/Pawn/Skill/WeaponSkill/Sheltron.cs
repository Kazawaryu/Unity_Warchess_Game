using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Buff;

namespace OOAD_WarChess.Pawn.Skill.WeaponSkill
{
    public class Sheltron : Skill
    {
        public Sheltron(Pawn initiator) : base(initiator)
        {
            Range = 0;
            Cooldown = 1;
            CastTime = 0;
            APCost = 3;
            MPCost = 0;
            DamageType = DamageType.Physical;
            Damage = 0;
            Type = SkillType.SinglePlayer;
            Name = "Sheltron";
            Effects = new List<IModifier> {new DefenceBuff(2, initiator, 0)};
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}