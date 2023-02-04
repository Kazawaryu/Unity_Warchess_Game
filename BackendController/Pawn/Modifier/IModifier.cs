using System;

namespace OOAD_WarChess.Pawn.Modifier
{

    public interface IModifier
    {
        public Func<int, int> Apply { get; set; } // Apply modifier to the value
        public ModifierType Type { get; set; } // Type of the modifier
        public int Duration { get; set; } // How many turns the modifier will last
        public PawnAttribute Target { get; set; }

        public bool
            IsUnique
        {
            get;
            set;
        } // If the modifier is unique, it will be removed if another modifier of the same type is applied

        public string Name { get; set; }
        public Func<string> Description { get; set; }

        public Pawn Giver { get; set; }
        public int ID { get; set; } // ID is to identify the one single specific buff/debuff, used when remove

        public bool Hidden { get; set; }

        public IModifier Clone();
    }

    public enum ModifierType
    {
        Permanent,
        Temporary,
        EachTurn,
        Default
    }
}