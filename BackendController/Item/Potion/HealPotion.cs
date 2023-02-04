using System;
using OOAD_WarChess.Battle;
using OOAD_WarChess.Pawn.Modifier.Common;
using OOAD_WarChess.Pawn.Skill.Common;

namespace OOAD_WarChess.Item.Potion
{
    public class HealPotion:Item
    {
        public HealPotion(int value):base(value)
        {
            Name = "Heal Potion";
        }
        
        public override Tuple<int, string> Cast(Pawn.Pawn initiator, Pawn.Pawn receiver,out string log)
        {
            ItemEffect.Initiator = initiator;
            ItemEffect.Effects.Add(new Heal(Value,initiator,0));
            IsUsed = true;
            var result = base.Cast(initiator, receiver,out log);
            return result;
        }
    }
}

