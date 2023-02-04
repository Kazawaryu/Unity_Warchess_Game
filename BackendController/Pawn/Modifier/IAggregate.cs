namespace OOAD_WarChess.Pawn.Modifier
{

    public interface IAggregate
    {
        public int Value { get; set; }

        public void Aggregate(IAggregate a);
    }
}