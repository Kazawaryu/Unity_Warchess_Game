using OOAD_WarChess.Battle.Terran;

namespace OOAD_WarChess.Battle.Map
{

    public class Tile
    {
        public static readonly Pawn.Pawn DefaultPawn = new();
        public TerranType Terran { get; set; }

        public Pawn.Pawn Unit { get; set; } = DefaultPawn;

        public Tile(TerranType terran)
        {
            Terran = terran;
        }
    }
}