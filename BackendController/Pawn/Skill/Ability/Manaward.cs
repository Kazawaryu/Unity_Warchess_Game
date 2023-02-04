using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Buff;

namespace OOAD_WarChess.Pawn.Skill.Ability
{
    public class Manaward : Skill
    {
        public Manaward(Pawn initiator) : base(initiator)
        {
            Range = 2;
            Cooldown = 3;
            CastTime = 0;
            APCost = 2;
            MPCost = 20;
            DamageType = DamageType.Pure;
            Damage = 0;
            Type = SkillType.SinglePlayer;
            Name = "Manaward";
            Effects = new List<IModifier> {new ManaShield(1, initiator, 1)};
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}