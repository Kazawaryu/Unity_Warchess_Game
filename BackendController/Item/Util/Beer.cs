using System;
using OOAD_WarChess.Pawn.Modifier.Common;
using OOAD_WarChess.Pawn.Skill;

namespace OOAD_WarChess.Item.Util
{

    public class Beer : Item
    {
        public Beer(int value) : base(value)
        {
            Name = "Beer";
        }
        
        public override Tuple<int, string> Cast(Pawn.Pawn initiator, Pawn.Pawn receiver,out string log)
        {
            ItemEffect.Initiator = initiator;
            ItemEffect.Name = "Beer";
            ItemEffect.Effects.Add(new Excitement(Value,initiator,0));
            IsUsed = true;
            var result = base.Cast(initiator, receiver,out log);
            return result;
        }
    }
}