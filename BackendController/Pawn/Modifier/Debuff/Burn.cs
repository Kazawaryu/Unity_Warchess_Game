using OOAD_WarChess.Localization;

namespace OOAD_WarChess.Pawn.Modifier.Debuff
{

    public class Burn : Modifier
    {
        public Burn(int value, Pawn giver, int id) : base(giver, id)
        {
            Duration = value;
            IsUnique = true;
            Type = ModifierType.EachTurn;
            Apply = x => Duration * 10;
            Target = PawnAttribute.DEFAULT;
            Description = () => $"{Lang.Text["Modifier_Debuff_Burn"]}:Burn {value} Turn";
            Name = "Burn";
        }
    }
}