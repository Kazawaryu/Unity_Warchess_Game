using OOAD_WarChess.Localization;

namespace OOAD_WarChess.Pawn.Modifier.Common
{

    public class Injury : Modifier, IAggregate
    {
        // Common modifier example code
        public Injury(int value, Pawn giver, int id) : base(giver, id)
        {
            Value = value;
            Apply = (x) => x - Value;
            Name = "Injury";
            Target = PawnAttribute.HP;
            Description = () => $"{Lang.Text["Modifier_Common_Injury"]}:-{Value} HP";
            Hidden = true;
        }

        public int Value { get; set; }

        public void Aggregate(IAggregate a)
        {
            Value += a.Value;
        }
    }
}