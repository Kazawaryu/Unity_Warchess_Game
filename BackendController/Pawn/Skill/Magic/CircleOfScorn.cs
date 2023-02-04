using System;
using OOAD_WarChess.Battle;

namespace OOAD_WarChess.Pawn.Skill.Magic
{

    public class CircleOfScorn:Skill
    {
        public CircleOfScorn(Pawn initiator) : base(initiator)
        {
            Range = 4;
            Cooldown = 1;
            CastTime = 0;
            APCost = 4;
            MPCost = 0;
            DamageType = DamageType.Arcane;
            Damage = 15;
            EffectArea = Tuple.Create(3, 3);
            Type = SkillType.EnemyArea;
            Name = "Circle of Scorn";
        }

        public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
        {
            var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
            log = CombatTracker.Instance.GetNewLog();
            return result;
        }
    }
}