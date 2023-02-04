namespace OOAD_WarChess.Pawn.Modifier.Common
{

    public class Excitement:Modifier,IAggregate
    {
        public Excitement(int value, Pawn giver, int id) : base(giver, id)
        {
            Value = value;
            Apply = x => x + Value;
            Name = "Excitement";
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