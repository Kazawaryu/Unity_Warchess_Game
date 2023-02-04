using System;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.WeaponSkill
{
    public class ThrowKnife:Skill
    {
        public ThrowKnife(Pawn initiator) : base(initiator)
        {
            Range = 8;
            Cooldown = 1;
            CastTime = 0;
            APCost = 2;
            MPCost = 0;
            DamageType = DamageType.Physical;
            Damage = 8;
            Name = "Throw Knife";
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