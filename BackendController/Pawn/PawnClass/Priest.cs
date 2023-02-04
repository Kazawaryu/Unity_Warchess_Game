using OOAD_WarChess.Item.Equipment.Amour;
using OOAD_WarChess.Item.Equipment.Weapon;
using OOAD_WarChess.Pawn.Skill.Magic;

namespace OOAD_WarChess.Pawn.PawnClass
{
    public class Priest : PawnClass
    {
        public Priest(Pawn pawn) : base(pawn)
        {
            Name = "Priest";
            STR = 2;
            DEX = 4;
            INT = 5;
            CON = 5;
            SkillSet.Add(new Glare(pawn));
            SkillSet.Add(new Cure(pawn));
            SkillSet.Add(new Medica(pawn));
            Weapon = new Wand();
            Armor = new SampleArmor();
        }
    }
}