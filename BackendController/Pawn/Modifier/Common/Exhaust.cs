using OOAD_WarChess.Localization;

namespace OOAD_WarChess.Pawn.Modifier.Common{

    public class Exhaust : Modifier, IAggregate
    {
        public Exhaust(int value, Pawn giver, int id) : base(giver, id)
        {
            Value = value;
            Apply = x => x - Value;
            Name = "Exhaust";
            Target = PawnAttribute.ACTPOINT;
            Type = ModifierType.Temporary;
            Duration = 1;
            Hidden = true;
        }

        public int Value { get; set; }

        public void Aggregate(IAggregate a)
        {
            Value += a.Value;
        }
    }
}