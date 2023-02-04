using System;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.Magic
{

    public class HolySpirit : Skill
    {
        public HolySpirit(Pawn initiator) : base(initiator)
        {
            Name = "Holy Spirit";
            APCost = 4;
            Damage = 15;
            MPCost = 10;
            Range = 3;
            DamageType = DamageType.Arcane;
            Type = SkillType.SingleEnemy;
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver,out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}