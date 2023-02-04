namespace OOAD_WarChess.Battle
{
    public static class RuleSet
    {
        public static bool IsHit(Pawn.Pawn attacker, Pawn.Pawn defender)
        {
            return Dice.D6(attacker.ACC) > Dice.D6(defender.DOGE);
        }

        public static bool IsCriticalHit(Pawn.Pawn attacker)
        {
            return Dice.D100(1) <= attacker.CRIT;
        }

        public static int CriticalDamageMultiplier()
        {
            return 2;
        }

        public static int DealPhysicalDamage(Pawn.Pawn pawn, int damage)
        {
            return damage / 5 * pawn.PHY_ATK *( pawn.Weapon.PHY_ATK>0?pawn.PHY_ATK:10) / 10;
        }

        public static int DealTureDamage(Pawn.Pawn pawn, int damage)
        {
            return damage;
        }

        public static int DealMagicalDamage(Pawn.Pawn pawn, int damage)
        {
            return damage / 5 * pawn.MAG_ATK * (pawn.Weapon.MAG_ATK>0?pawn.MAG_ATK:10 / 10);
        }

        public static int DefendPhysicalDamage(Pawn.Pawn pawn, int damage)
        {
            var _damage = damage - (pawn.PHY_DEF * pawn.Armor.PHY_DEF / 12);
            return _damage > 0 ? _damage : (int)(_damage * 0.05);
        }

        public static int DefendTrueDamage(Pawn.Pawn pawn, int damage)
        {
            return damage;
        }

        public static int DefendMagicalDamage(Pawn.Pawn pawn, int damage)
        {
            var _damage = damage - (pawn.MAG_DEF * pawn.Armor.MAG_DEF / 12);
            return _damage > 0 ? _damage : (int)(_damage * 0.05);

        }
    }
}