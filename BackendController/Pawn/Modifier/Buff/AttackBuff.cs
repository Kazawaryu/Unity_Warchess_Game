namespace OOAD_WarChess.Pawn.Modifier.Buff
{

    public class AttackBuff : Modifier
    {
        public AttackBuff(int value, Pawn giver, int id) : base(giver, id)
        {
            Duration = value;
            Name = "Attack Increase";
            Apply = x => (int)(1.5 * x);
            Target = PawnAttribute.PHY_ATK;
            Type = ModifierType.Temporary;
        }
    }
}