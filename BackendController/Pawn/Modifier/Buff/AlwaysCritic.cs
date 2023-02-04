using OOAD_WarChess.Localization;

namespace OOAD_WarChess.Pawn.Modifier.Buff
{

    public class AlwaysCritic : Modifier
    {
        // Buff example code
        public AlwaysCritic(int value, Pawn giver, int id) : base(giver, id)
        {
            Duration = value;
            Name = "Always Critic";
            Type = ModifierType.Temporary;
            Apply = x => 100;
            Target = PawnAttribute.CRIT;
            Description = () => string.Format(Lang.Text["Modifier_Buff_AlwaysCritic"], Duration);
        }
    }
}