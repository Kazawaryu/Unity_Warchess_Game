namespace OOAD_WarChess.Pawn.Modifier.Buff
{

    public class DefenceBuff : Modifier

    {
        public DefenceBuff(int value,Pawn giver, int id) : base(giver, id)
        {
            Duration = value;
            Name = "Defence Increase";
            Apply = x => (int) (4 * x);
            Target = PawnAttribute.PHY_DEF;
            Type = ModifierType.Temporary;
        }
    }
}