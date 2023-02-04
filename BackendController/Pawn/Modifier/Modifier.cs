using System;

namespace OOAD_WarChess.Pawn.Modifier
{

    public abstract class Modifier : IModifier
    {
        public Func<int, int> Apply { get; set; }

        public ModifierType Type { get; set; } = ModifierType.Default;

        public int Duration { get; set; }
        public PawnAttribute Target { get; set; }
        public bool IsUnique { get; set; } = false;

        public string Name { get; set; }
        public Func<string> Description { get; set; }

        public Pawn Giver { get; set; }
        public int ID { get; set; }
        public bool Hidden { get; set; } = false;

        public IModifier Clone()
        {
            return (Modifier) MemberwiseClone();
        }

        protected Modifier(Pawn giver, int id)
        {
            Giver = giver;
            ID = id;
            Target = PawnAttribute.DEFAULT;
        }
    }
}