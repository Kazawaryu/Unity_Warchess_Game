using OOAD_WarChess.Localization;

namespace OOAD_WarChess.Pawn.Modifier.Debuff
{

    public class Weak : Modifier
    {
        // Debuff example code
        public Weak(int value, Pawn giver, int id) : base(giver, id)
        {
            Apply = x => x - value;
            Target = PawnAttribute.STR;
            Description = () => $"{Lang.Text["Modifier_Debuff_Weak"]}:-{value} STR";
        }
    }
}