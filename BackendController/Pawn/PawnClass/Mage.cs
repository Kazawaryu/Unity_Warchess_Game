using OOAD_WarChess.Item.Equipment.Amour;
using OOAD_WarChess.Item.Equipment.Weapon;
using OOAD_WarChess.Pawn.Skill.Ability;
using OOAD_WarChess.Pawn.Skill.Magic;

namespace OOAD_WarChess.Pawn.PawnClass
{
    public class Mage : PawnClass
    {
        public Mage(Pawn pawn) : base(pawn)
        {
            Name = "Mage";
            STR = 2;
            DEX = 4;
            INT = 8;
            CON = 2;
            SkillSet.Add(new MagicArrow(pawn));
            SkillSet.Add(new Manaward(pawn));
            SkillSet.Add(new Fireball(pawn));
            Weapon = new Wand();
            Armor = new SampleArmor();
        }
    }
}