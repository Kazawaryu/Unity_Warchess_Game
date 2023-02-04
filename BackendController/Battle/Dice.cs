using System;

namespace OOAD_WarChess.Battle
{

    public static class Dice
    {
        private static Random _random = new(Guid.NewGuid().GetHashCode());

        public static Func<int, int> D6 = (x) => Roll(6, x);

        public static Func<int, int> D20 = (x) => Roll(20, x);

        public static Func<int, int> D100 = (x) => Roll(100, x);

        public static Func<int, int> Roll(DiceType type)
        {
            return type switch
            {
                DiceType.Six => D6,
                DiceType.Twenty => D20,
                DiceType.Hundred => D100,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static int Roll(int sides)
        {
            return _random.Next(1, sides + 1);
        }

        public static int Roll(int sides, int count)
        {
            var total = 0;
            for (var i = 0; i < count; i++)
            {
                total += Roll(sides);
            }

            return total;
        }


    }

    public enum DiceType
    {
        Six = 6,
        Twenty = 20,
        Hundred = 100
    }
}