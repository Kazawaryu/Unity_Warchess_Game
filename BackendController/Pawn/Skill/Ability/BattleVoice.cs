using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Buff;

namespace OOAD_WarChess.Pawn.Skill.Ability
{
    public class BattleVoice : Skill
    {
        public BattleVoice(Pawn initiator) : base(initiator)
        {
            Range = 0;
            Cooldown = 3;
            CastTime = 0;
            APCost = 2;
            MPCost = 0;
            DamageType = DamageType.Pure;
            Type = SkillType.PlayerArea;
            Damage = 0;
            EffectArea = Tuple.Create(3, 3);
            Name = "Battle Voice";
            Effects = new List<IModifier> {new AlwaysCritic(1, initiator, 1)};
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}