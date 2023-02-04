using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Common;
using OOAD_WarChess.Pawn.Modifier.Debuff;

namespace OOAD_WarChess.Pawn.Skill.Magic
{
    public class Cure : Skill
    {
        public Cure(Pawn initiator) : base(initiator)
        {
            Range = 3;
            Cooldown = 1;
            CastTime = 0;
            APCost = 3;
            MPCost = 30;
            DamageType = DamageType.Pure;
            Type = SkillType.SinglePlayer;
            Damage = 0;
            Name = "Cure";
            Effects = new List<IModifier> {new Heal(50, initiator, 1)};
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}