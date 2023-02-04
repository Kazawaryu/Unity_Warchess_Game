using System;

namespace OOAD_WarChess.Util
{

    public class RandomId
    {
        private static readonly Random Random = new(new Guid().GetHashCode());

        public static int NewId()
        {
            return Random.Next();
        }
    }
}