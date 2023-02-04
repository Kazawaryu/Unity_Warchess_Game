using OOAD_WarChess.Item.Equipment.Amour;
using OOAD_WarChess.Item.Equipment.Weapon;
using OOAD_WarChess.Pawn.Skill.Ability;
using OOAD_WarChess.Pawn.Skill.WeaponSkill;

namespace OOAD_WarChess.Pawn.PawnClass
{
    public class Warrior : PawnClass
    {
        public Warrior(Pawn pawn) : base(pawn)
        {
            Name = "Warrior";
            STR = 10;
            DEX = 5;
            INT = 2;
            CON = 10;
            SkillSet.Add(new Maim(pawn));
            SkillSet.Add(new InnerBeast(pawn));
            SkillSet.Add(new PrimalRend(pawn));
            Weapon = new GreatBlade();
            Armor = new SampleArmor();
        }
    }
}