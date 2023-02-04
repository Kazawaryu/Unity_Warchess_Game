using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Common;

namespace OOAD_WarChess.Pawn.Skill.Magic
{
    public class Medica : Skill
    {
        public Medica(Pawn initiator) : base(initiator)
        {
            Range = 3;
            Cooldown = 3;
            CastTime = 0;
            APCost = 3;
            MPCost = 50;
            DamageType = DamageType.Pure;
            Damage = 0;
            EffectArea = Tuple.Create<int, int>(3, 3);
            Type = SkillType.PlayerArea;
            Name = "Medica";
            Effects = new List<IModifier> {new Heal(30, initiator, 1)};
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}