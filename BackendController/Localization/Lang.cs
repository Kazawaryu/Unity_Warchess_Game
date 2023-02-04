using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace OOAD_WarChess.Localization
{

    public class Lang
    {
        // Singleton
        private static Lang _Instance { get; } = new();

        public Language Language { get; set; } = Language.SimplifiedChinese;

        public static Lang Text => _Instance;

        private Dictionary<string, string> CorpusSimplifiedChinese { get; } = new();
        private Dictionary<string, string> CorpusEnglish { get; } = new();


        public string this[string key]
        {
            get
            {
                return Language switch
                {
                    Language.SimplifiedChinese => CorpusSimplifiedChinese.ContainsKey(key)
                        ? CorpusSimplifiedChinese[key]
                        : key,
                    Language.English => CorpusEnglish.ContainsKey(key)
                        ? CorpusEnglish[key]
                        : key,
                    _ => $"`{Language}` LANGUAGE SET NOT FOUND"
                };
            }
        }

        private bool GenerateCorpus(DirectoryInfo info, Language l)
        {
            if (info.GetDirectories().Length != 0)
            {
                info.GetDirectories().All(e => GenerateCorpus(e, l));
            }

            var xmlFiles = info.GetFiles($"{l.ToString()}.xml");
            var langDict = SelectLang(l);
            try
            {
                foreach (var xmlFile in xmlFiles)
                {
                    var doc = new XmlDocument();
                    doc.Load(xmlFile.FullName);
                    var root = doc.DocumentElement;
                    if (root == null) continue;
                    for (var i = 0; i < root.ChildNodes.Count; i++)
                    {
                        var name = root.ChildNodes[i]?.Name;
                        if (name == null) continue;
                        var innerText = root.ChildNodes[i]?.InnerText;
                        if (innerText != null)
                            langDict.Add(name, innerText);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        private Lang()
        {
            var langDictInfo = new DirectoryInfo(@"../../../Localization/");
            GenerateCorpus(langDictInfo, Language.SimplifiedChinese);
            GenerateCorpus(langDictInfo, Language.English);
            // If need to add more language, change here. Though not likely.
        }

        private Dictionary<string, string> SelectLang(Language l)
        {
            return l switch
            {
                Language.English => CorpusEnglish,
                Language.SimplifiedChinese => CorpusSimplifiedChinese,
                _ => throw new ArgumentOutOfRangeException(nameof(l), l, null)
            };
        }
    }

    public enum Language
    {
        English,
        SimplifiedChinese
    }
}