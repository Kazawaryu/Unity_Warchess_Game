using OOAD_WarChess.Localization;

namespace OOAD_WarChess.Pawn.Modifier.Common
{

    public class Heal : Modifier, IAggregate
    {
        public Heal(int value, Pawn giver, int id) : base(giver, id)
        {
            Value = value;
            Apply = x => x + Value;
            Target = PawnAttribute.HP;
            Description = () => $"{Lang.Text["Modifier_Common_Heal"]}:+{Value} HP";
            Name = "Heal";
            Hidden = true;
        }

        public int Value { get; set; }

        public void Aggregate(IAggregate a)
        {
            Value += a.Value;
        }
    }
}