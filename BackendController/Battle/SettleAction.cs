using System;
using OOAD_WarChess.Pawn;
using OOAD_WarChess.Pawn.Modifier;
using OOAD_WarChess.Pawn.Modifier.Common;
using OOAD_WarChess.Pawn.Skill;

namespace OOAD_WarChess.Battle
{
    public class SettleAction
    {
        private readonly CombatTracker _combatTracker = CombatTracker.Instance;

        public static SettleAction Instance { get; private set; } = new SettleAction();

        public Tuple<int, string> SettleSkill(ISkill skill, Pawn.Pawn initiator, Pawn.Pawn target)
        {
            var damageModifier = 0;
            if (!RuleSet.IsHit(initiator, target)) return Tuple.Create(0, "Miss");
            damageModifier = RuleSet.IsCriticalHit(initiator) ? RuleSet.CriticalDamageMultiplier() : 1;
            var rawDamage = skill.DamageType switch
            {
                DamageType.Physical => RuleSet.DefendPhysicalDamage(target,
                    RuleSet.DealPhysicalDamage(initiator, skill.Damage)),
                DamageType.Pure => RuleSet.DefendTrueDamage(target,
                    RuleSet.DealTureDamage(initiator, skill.Damage)),
                _ => RuleSet.DefendMagicalDamage(target,
                    RuleSet.DealMagicalDamage(initiator, skill.Damage))
            };
            if (rawDamage < 0) rawDamage = 0;
            var realDamage = rawDamage * damageModifier;
            if (realDamage > 0)
            {
                var damage = new Injury(damageModifier * rawDamage, initiator, 0);
                SettleModifier(target, damage, skill.Name);
            }

            foreach (var modifier in skill.Effects)
            {
                if (modifier is Heal)
                {
                    if (modifier.Apply(target.HP) > target.GetAttribute(PawnAttribute.HP))
                    {
                        SettleModifier(target,
                            new Heal(target.GetAttribute(PawnAttribute.HP) - target.HP, initiator, 0), skill.Name);
                        CombatTracker.Instance.LogHeal(initiator.Name, target.Name, skill.Name,
                            Tuple.Create(target.GetAttribute(PawnAttribute.HP) - target.HP, "Heal"));
                    }
                    else
                    {
                        SettleModifier(target, modifier, skill.Name);
                        CombatTracker.Instance.LogHeal(initiator.Name, target.Name, skill.Name,
                            Tuple.Create<int, string>(modifier.Apply(0), "Heal"));
                    }
                }
                else
                {
                    SettleModifier(target, modifier.Clone(), skill.Name);
                }
            }

            if (skill.APCost != 0)
            {
                var exhaust = new Exhaust(skill.APCost, initiator, 0);
                SettleModifier(initiator, exhaust, skill.Name);
            }

            if (skill.MPCost != 0)
            {
                var deplete = new Deplete(skill.MPCost, initiator, 0);
                SettleModifier(initiator, deplete, skill.Name);
            }

            var result = Tuple.Create(realDamage, skill.Name + (damageModifier > 0 ? " Critical!!" : ""));
            if (damageModifier > 1 && realDamage > 0)
            {
                _combatTracker.LogMisc("Critical!!");
            }

            if (skill.Damage == 0)
            {
                _combatTracker.LogSkillNoDamage(initiator.Name, target.Name, skill.Name, result);
            }
            else
            {
                _combatTracker.LogSkill(initiator.Name, target.Name, skill.Name, result);
            }
            return result;
        }

        private void SettleModifier(Pawn.Pawn pawn, IModifier modifier, string skillName)
        {
            if (modifier.IsUnique)
            {
                var temp = pawn.Modifiers.Find(m => m.Name == modifier.Name);
                if (temp != null)
                {
                    pawn.Modifiers.Remove(temp);
                    _combatTracker.LogModifierLoss(pawn, temp);
                }
            }

            pawn.Modifiers.Add(modifier);
            _combatTracker.LogModifierGain(pawn, modifier, skillName);
        }

        public Tuple<int, string> SettlePawn(Pawn.Pawn pawn)
        {
            var result = Tuple.Create<int, string>(0, "");
            for (var i = 0; i < pawn.Modifiers.Count; i++)
            {
                var modifier = pawn.Modifiers[i];
                if (modifier.Type == ModifierType.EachTurn)
                {
                    if (modifier.Apply(0) > 0)
                    {
                        SettleModifier(pawn, new Injury(modifier.Apply(0), modifier.Giver, 0), modifier.Name);
                    }
                    else
                    {
                        SettleModifier(pawn, new Heal(modifier.Apply(0), modifier.Giver, 0), modifier.Name);
                    }
                }

                if (modifier.Type is ModifierType.Temporary or ModifierType.EachTurn &&
                    modifier.Duration <= 1)
                {
                    pawn.Modifiers.Remove(modifier);
                    _combatTracker.LogModifierLoss(pawn, modifier);
                    result = Tuple.Create(modifier.Apply(0), modifier.Name);
                    i--;
                }
                else
                {
                    modifier.Duration--;
                }
            }

            return result;
        }

        public Tuple<int, string> SettleExpGain(Pawn.Pawn pawn, int value)
        {
            var previousLevel = pawn.LVL;
            pawn.EXP += value;
            return Tuple.Create<int, string>(0, "");
        }
    }
}