using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Common;
using OOAD_WarChess.Pawn.Modifier.Debuff;

namespace OOAD_WarChess.Pawn.Skill.Magic
{
    public class Fireball : Skill
    {
        public Fireball(Pawn initiator) : base(initiator)
        {
            Range = 3;
            Cooldown = 2;
            CastTime = 0;
            APCost = 3;
            MPCost = 30;
            DamageType = DamageType.Fire;
            Damage = 10;
            EffectArea = Tuple.Create(3, 3);
            Type = SkillType.EnemyArea;
            Name = "Fireball";
            Effects = new List<IModifier> {new Burn(3, initiator, 1)};
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}