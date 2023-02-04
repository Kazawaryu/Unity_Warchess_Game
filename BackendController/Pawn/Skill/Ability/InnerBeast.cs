using System;
using System.Collections.Generic;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Buff;

namespace OOAD_WarChess.Pawn.Skill.Ability
{
    public class InnerBeast:Skill
    {
        
            public InnerBeast(Pawn initiator) : base(initiator)
            {
                Range = 0;
                Cooldown = 3;
                CastTime = 0;
                APCost = 2;
                MPCost = 0;
                DamageType = DamageType.Pure;
                Type = SkillType.SinglePlayer;
                Damage = 0;
                Name = "InnerBeast";
                Effects = new List<IModifier> {new AttackBuff(2, initiator, 1)};
            }

            public override Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log)
            {
                var result = SettleAction.Instance.SettleSkill(this, initiator, receiver);
                log = CombatTracker.Instance.GetNewLog();
                return result;
            }
        }
    
}

