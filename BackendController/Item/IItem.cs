using System.Collections.Generic;
using OOAD_WarChess.Pawn;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Skill;

namespace OOAD_WarChess.Item
{
    public interface IItem : ICast
    {
        public int Weight { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public bool IsUsed { get; set; }
        public ISkill ItemEffect { get; set; }
    }
}