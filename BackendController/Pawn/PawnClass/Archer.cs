using OOAD_WarChess.Item.Equipment.Amour;
using OOAD_WarChess.Item.Equipment.Weapon;
using OOAD_WarChess.Pawn.Skill.Ability;
using OOAD_WarChess.Pawn.Skill.WeaponSkill;

namespace OOAD_WarChess.Pawn.PawnClass
{
    public class Archer : PawnClass
    {
        public Archer(Pawn pawn) : base(pawn)
        {
            Name = "Archer";
            STR = 3;
            DEX = 8;
            INT = 1;
            CON = 5;
            SkillSet.Add(new StraightShot(pawn));
            SkillSet.Add(new BattleVoice(pawn));
            SkillSet.Add(new ApexArrow(pawn));
            Weapon = new Bow();
            Armor = new SampleArmor();
        }
    }
}