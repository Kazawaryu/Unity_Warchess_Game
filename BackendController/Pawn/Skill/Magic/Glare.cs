using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Debuff;

namespace OOAD_WarChess.Pawn.Skill.Magic
{
    public class Glare : Skill

    {
        public Glare(Pawn initiator) : base(initiator)
        {
            Range = 3;
            Cooldown = 1;
            CastTime = 0;
            APCost = 3;
            MPCost = 10;
            DamageType = DamageType.Arcane;
            Damage = 10;
            Type = SkillType.SingleEnemy;
            Name = "Glare";
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver,out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}