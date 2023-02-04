using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.WeaponSkill
{
    public class ApexArrow : Skill
    {
        public ApexArrow(Pawn initiator) : base(initiator)
        {
            Range = 4;
            Cooldown = 3;
            CastTime = 0;
            APCost = 4;
            MPCost = 0;
            DamageType = DamageType.Physical;
            Damage = 12;
            EffectArea = Tuple.Create<int, int>(3, 3);
            Type = SkillType.EnemyArea;
            Name = "Apex Arrow";
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}