using System;

namespace OOAD_WarChess.Pawn
{
    public interface ICast
    {
        public Tuple<int, string> Cast(Pawn initiator, Pawn receiver, out string log);

        public Tuple<int, string> Cast(Pawn initiator, Pawn receiver);

        // This method should be only used on Move for now
        public Tuple<int, string> Cast(Pawn initiator, int value);
    }
}