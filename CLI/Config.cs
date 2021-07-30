using System;
using System.IO;
using System.Collections.Generic;

using static VanityMonKeyGenerator.Accessories;

namespace CLI
{
    public class Config
    {
        public bool IsOpened = false;
        public List<string> Accessories = new List<string>();
        public int RequestAmount = 100;
        public bool LogData = false;
        
        
        private const string configFile = "config.txt";
        private List<string> possibleAccessories = new List<string>()
        {
            "Any", "BananaHands", "BananaRightHand", "Bandana", "Beanie", "BeanieBanano",
            "BeanieHippie", "BeanieLong", "BeanieLongBanano", "Bowtie", "Camera", "Cap",
            "CapBackwards", "CapBanano", "CapBebe", "CapCarlos", "CapHng", "CapHngPlus",
            "CapKappa", "CapPepe", "CapRick", "CapSmug", "CapSmugGreen", "CapThonk",
            "Cigar", "Club", "Confused", "Crown", "EyePatch", "Fedora", "FedoraLong",
            "Flamethrower", "GlassesNerdCyan", "GlassesNerdGreen", "GlassesNerdPink",
            "GlovesWhite", "Guitar", "HatCowboy", "HatJester", "HelmetViking", "Joint",
            "Meh", "Microphone", "Monocle", "NecklaceBoss", "None", "OverallsBlue", "OverallsRed",
            "PantsBusinessBlue", "PantsFlower", "Pipe", "SmileBigTeeth", "SmileNormal",
            "SmileTongue", "SneakersBlue", "SneakersGreen", "SneakersRed", "SneakersSwagger",
            "SocksHStripe", "SocksVStripe", "SunglassesAviatorCyan", "SunglassesAviatorGreen",
            "SunglassesAviatorYellow", "SunglassesThug", "TailSock", "TshirtLongStripes",
            "TshirtShortWhite", "WhiskyRight"
        };

        public Config()
        {
            if (!File.Exists($@"{Directory.GetCurrentDirectory()}\{configFile}"))
            {
                return;
            }
            else
            {
                string[] lines = File.ReadAllLines(configFile);
                foreach (string line in lines)
                {
                    if (line.Trim().StartsWith('#'))
                    {
                        continue;
                    }
                    else if (line.Contains(':'))
                    {
                        string[] setting = line.Trim().Split(':', StringSplitOptions.RemoveEmptyEntries);
                        if (setting[0].ToLower() == "request-amount")
                        {
                            RequestAmount = int.Parse(setting[1]);
                        }
                        else if (setting[0].ToLower() == "log-data")
                        {
                            LogData = bool.Parse(setting[1]);
                        }
                        else
                        {
                            if (setting.Length == 1)
                            {
                                if (setting[0].ToLower() != "mouths")
                                {
                                    Accessories.Add($"{Capitalize(setting[0])}-None");
                                    continue;
                                }
                                else
                                {
                                    throw new Exception("Mouths cannot be none.");
                                }
                            }
                            ParseSetting(setting);
                        }
                    }
                }
            }
            IsOpened = true;
        }

        private void ParseSetting(string[] setting)
        {
            string[] items = SplitItems(setting[1]);
            switch (setting[0].ToLower())
            {
                case "glasses":
                    foreach (string item in items)
                    {
                        Accessories.Add($"Glasses-{item}");
                    }
                    return;
                case "hats":
                    foreach (string item in items)
                    {
                        Accessories.Add($"Hats-{item}");
                    }
                    return;
                case "misc":
                    foreach (string item in items)
                    {
                        Accessories.Add($"Misc-{item}");
                    }
                    return;
                case "mouths":
                    foreach (string item in items)
                    {
                        if (item == "None")
                        {
                            return;
                        }
                        Accessories.Add($"Mouths-{item}");
                    }
                    return;
                case "shirts-pants":
                    foreach (string item in items)
                    {
                        Accessories.Add($"ShirtsPants-{item}");
                    }
                    return;
                case "shoes":
                    foreach (string item in items)
                    {
                        Accessories.Add($"Shoes-{item}");
                    }
                    return;
                case "tails":
                    foreach (string item in items)
                    {
                        Accessories.Add($"Tails-{item}");
                    }
                    return;

                default:
                    throw new Exception($"{setting[0]} is not a valid entry.");
            }
        }

        private string[] SplitItems(string items)
        {
            string[] splitItems = items.Split(',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitItems.Length; i++)
            {
                string item = splitItems[i].Trim();
                splitItems[i] = item.ReplaceCase();
                if (!possibleAccessories.Contains(splitItems[i]))
                {
                    throw new Exception($"\"{item}\" is not a valid item.");
                }
            }
            return splitItems;
        }

        private string Capitalize(string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        public static void CreateSettingsFile()
        {
            StreamWriter file = new StreamWriter(configFile);
            file.WriteLine("# Set your requested accessories here. Separate items on a category using commas.");
            file.WriteLine("# You can leave a category empty for \"none\". Mouths cannot be \"none\".");
            file.WriteLine("glasses: any");
            file.WriteLine("hats: none, cap, beanie-banano");
            file.WriteLine("misc: none");
            file.WriteLine("mouths: any");
            file.WriteLine("shirts-pants: tshirt-short-white");
            file.WriteLine("shoes:");
            file.WriteLine("tails:");
            file.WriteLine();
            file.WriteLine("# Set the amount of MonKey requests in each batch. Leave empty for default (100).");
            file.WriteLine("request-amount: 100");
            file.WriteLine("# Set true if you want your MonKey data to be logged.");
            file.WriteLine("log-data: false");
            file.Close();
        }
    }
}
