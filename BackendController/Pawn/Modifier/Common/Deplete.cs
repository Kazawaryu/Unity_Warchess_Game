namespace OOAD_WarChess.Pawn.Modifier.Common
{

    public class Deplete : Modifier, IAggregate
    {
        public Deplete(int value, Pawn giver, int id) : base(giver, id)
        {
            Value = value;
            Apply = x => x - Value;
            Name = "Deplete";
            Target = PawnAttribute.MP;
            Type = ModifierType.Permanent;
            Hidden = true;
        }

        public int Value { get; set; }

        public void Aggregate(IAggregate a)
        {
            Value += a.Value;
        }
    }
}