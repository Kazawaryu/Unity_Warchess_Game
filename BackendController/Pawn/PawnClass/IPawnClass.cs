using System.Collections.Generic;
using OOAD_WarChess.Pawn.Skill;

namespace OOAD_WarChess.Pawn.PawnClass
{
    public interface IPawnClass
    {
        public string Name { get; set; }
        public int STR { get; set; }
        public int DEX { get; set; }
        public int INT { get; set; }
        public int CON { get; set; }
        public List<ISkill> SkillSet { get; set; }
    }
}

