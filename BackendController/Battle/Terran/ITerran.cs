using System;
using OOAD_WarChess.Pawn.Modifier;

namespace OOAD_WarChess.Battle.Terran
{

    public interface ITerran
    {
        public TerranType Type { get; set; }

       

        public Pawn.Pawn _pawn { get; set; }

        
    }

    public enum TerranType
    {
        Plain,
        Forest,
        Mountain
    }
}