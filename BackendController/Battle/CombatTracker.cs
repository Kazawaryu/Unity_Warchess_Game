using System;
using System.Collections.Generic;
using System.Linq;
using OOAD_WarChess.Pawn.Modifier;

namespace OOAD_WarChess.Battle
{
    public class CombatTracker
    {
        public static CombatTracker Instance { get; set; } = new CombatTracker();

        private static List<Tuple<string, string, string, int, string, LogType>> _combatLogList = new();

        public static bool IsDebug = true;

        private int _newLogCount = 0;

        private CombatTracker()
        {
        }

        public static string CombatLogToString(Tuple<string, string, string, int, string, LogType> log)
        {
            return log.Item6 switch
            {
                LogType.Skill => $"[{log.Item1}] used {log.Item3} on [{log.Item2}]. Deal {log.Item4} damage.",
                LogType.ModifierLoss => $"[{log.Item1}] lost effect {log.Item2}",
                LogType.ModifierGain =>
                    $"[{log.Item1}] gain effect {log.Item2} from [{log.Item3}]({log.Item5})" +
                    (log.Item4 > 0 ? $"for {log.Item4} turn(s)" : ""),
                LogType.Item => $"[{log.Item1}] used Item {log.Item3} on [{log.Item2}]",
                LogType.Misc => $"{log.Item1}",
                LogType.Heal => $"[{log.Item1}] used {log.Item3} on [{log.Item2}]. Heal {log.Item4}.",
                LogType.SkillNoDamage => $"[{log.Item1}] used {log.Item3} on [{log.Item2}].",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void PrintCombatLog()
        {
            foreach (var log in _combatLogList)
            {
                Console.WriteLine(CombatLogToString(log));
            }
        }

        public string LogSkill(string initiator, string target, string skill, Tuple<int, string> result)
        {
            _combatLogList.Add(Tuple.Create(initiator, target, skill, result.Item1, result.Item2, LogType.Skill));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string LogHeal(string initiator, string target, string skill, Tuple<int, string> result)
        {
            _combatLogList.Add(Tuple.Create(initiator, target, skill, result.Item1, result.Item2, LogType.Heal));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string LogSkillNoDamage(string initiator, string target, string skill, Tuple<int, string> result)
        {
            _combatLogList.Add(
                Tuple.Create(initiator, target, skill, result.Item1, result.Item2, LogType.SkillNoDamage));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string LogItem(string initiator, string target, string item, Tuple<int, string> result)
        {
            _combatLogList.Add(Tuple.Create(initiator, target, item, result.Item1, result.Item2, LogType.Item));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string LogModifierLoss(Pawn.Pawn pawn, IModifier modifier)
        {
            _combatLogList.Add(Tuple.Create(pawn.Name, modifier.Name, modifier.Name, 0, "", LogType.ModifierLoss));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string LogModifierGain(Pawn.Pawn pawn, IModifier modifier, string skillName)
        {
            _combatLogList.Add(Tuple.Create(pawn.Name, modifier.Name, modifier.Giver.Name, modifier.Duration, skillName,
                LogType.ModifierGain));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string LogMisc(string message)
        {
            _combatLogList.Add(Tuple.Create(message, "", "", 0, "", LogType.Misc));
            if (IsDebug)
            {
                Console.WriteLine(CombatLogToString(_combatLogList.Last()));
            }

            _newLogCount++;
            return CombatLogToString(_combatLogList.Last());
        }

        public string GetNewLog()
        {
            if (_newLogCount == 0)
            {
                return "";
            }

            var result = "";
            for (var i = _combatLogList.Count - _newLogCount; i < _combatLogList.Count; i++)
            {
                result += CombatLogToString(_combatLogList[i]) + "\n";
            }

            _newLogCount = 0;
            return result;
        }

        public enum LogType
        {
            Skill,
            SkillNoDamage,
            Item,
            ModifierLoss,
            ModifierGain,
            Heal,
            Misc
        }
    }
}