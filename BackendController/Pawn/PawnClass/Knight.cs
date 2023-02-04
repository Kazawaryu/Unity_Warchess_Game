using OOAD_WarChess.Item.Equipment.Amour;
using OOAD_WarChess.Item.Equipment.Weapon;
using OOAD_WarChess.Pawn.Skill.Magic;
using OOAD_WarChess.Pawn.Skill.WeaponSkill;

namespace OOAD_WarChess.Pawn.PawnClass
{

    public class Knight : PawnClass
    {
        public Knight(Pawn pawn) : base(pawn)
        {
            Name = "Knight";
            STR = 5;
            DEX = 4;
            INT = 2;
            CON = 15;
            SkillSet.Add(new HolySpirit(pawn));
            SkillSet.Add(new Sheltron(pawn));
            SkillSet.Add(new CircleOfScorn(pawn));
            Weapon = new SwordAndShield();
            Armor = new SampleArmor();
        }
    }
}