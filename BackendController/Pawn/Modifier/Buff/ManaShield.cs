namespace OOAD_WarChess.Pawn.Modifier.Buff
{
    public class ManaShield:Modifier
    {
        public ManaShield(int value,Pawn giver, int id) : base(giver, id)
        {
            Duration = value;
            Name = "Mana Shield";
            Type = ModifierType.Temporary;
            Apply = x => x + 100;
            Target = PawnAttribute.MAG_DEF;
        }
    }
}

