using System;
using OOAD_WarChess.Pawn.Modifier.Common;
using OOAD_WarChess.Pawn.Skill;
using OOAD_WarChess.Pawn.Skill.WeaponSkill;

namespace OOAD_WarChess.Item.Util
{

    public class Knife:Item
    {
        public Knife(int value) : base(value)
        {
            Name = "Knife";
        }
        
        public override Tuple<int, string> Cast(Pawn.Pawn initiator, Pawn.Pawn receiver,out string log)
        {
            ItemEffect.Initiator = initiator;
            ItemEffect.Damage = Value;
            ItemEffect.DamageType = DamageType.Physical;
            IsUsed = true;
            var result = base.Cast(initiator, receiver,out log);
            return result;
        }
    }
}