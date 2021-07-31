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
            "Glasses-Any", "Hats-Any", "Misc-Any", "Mouths-Any", "ShirtsPants-Any", "Shoes-Any", "Tails-Any",
            "Misc-BananaHands", "Misc-BananaRightHand", "Hats-Bandana", "Hats-Beanie", "Hats-BeanieBanano",
            "Hats-BeanieHippie", "Hats-BeanieLong", "Hats-BeanieLongBanano", "Misc-Bowtie", "Misc-Camera", "Hats-Cap",
            "Hats-CapBackwards", "Hats-CapBanano", "Hats-CapBebe", "Hats-CapCarlos", "Hats-CapHng", "Hats-CapHngPlus",
            "Hats-CapKappa", "Hats-CapPepe", "Hats-CapRick", "Hats-CapSmug", "Hats-CapSmugGreen", "Hats-CapThonk",
            "Mouths-Cigar", "Misc-Club", "Mouths-Confused", "Hats-Crown", "Glasses-EyePatch", "Hats-Fedora", "Hats-FedoraLong",
            "Misc-Flamethrower", "Glasses-GlassesNerdCyan", "Glasses-GlassesNerdGreen", "Glasses-GlassesNerdPink",
            "Misc-GlovesWhite", "Misc-Guitar", "Hats-HatCowboy", "Hats-HatJester", "Hats-HelmetViking", "Mouths-Joint",
            "Mouths-Meh", "Misc-Microphone", "Glasses-Monocle", "Misc-NecklaceBoss", "Glasses-None", "Hats-None", "Misc-None",
            "ShirtsPants-None", "Shoes-None", "Tails-None", "ShirtsPants-OverallsBlue", "ShirtsPants-OverallsRed",
            "ShirtsPants-PantsBusinessBlue", "ShirtsPants-PantsFlower", "Mouths-Pipe", "Mouths-SmileBigTeeth", "Mouths-SmileNormal",
            "Mouths-SmileTongue", "Shoes-SneakersBlue", "Shoes-SneakersGreen", "Shoes-SneakersRed", "Shoes-SneakersSwagger",
            "Shoes-SocksHStripe", "Shoes-SocksVStripe", "Glasses-SunglassesAviatorCyan", "Glasses-SunglassesAviatorGreen",
            "Glasses-SunglassesAviatorYellow", "Glasses-SunglassesThug", "Tails-TailSock", "ShirtsPants-TshirtLongStripes",
            "ShirtsPants-TshirtShortWhite", "Misc-WhiskyRight"
        };

        private List<string> possibleCategories = new List<string>()
        {
            "glasses", "hats", "misc", "mouths", "shirts-pants", "shoes", "tails"
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
            string[] items = SplitItems(setting[0], setting[1]);
            foreach (string item in items)
            {
                Accessories.Add(item);
            }
        }

        private string[] SplitItems(string category, string items)
        {
            if (!possibleCategories.Contains(category))
            {
                throw new Exception($"\"{category}\" is not a valid category.");
            }
            string[] splitItems = items.Split(',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitItems.Length; i++)
            {
                string item = splitItems[i].Trim();
                splitItems[i] = $"{category.ReplaceCase()}-{item.ReplaceCase()}";
                if (!possibleAccessories.Contains(splitItems[i]))
                {
                    throw new Exception($"\"{item}\" is not a valid item for {category}.");
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
            file.WriteLine("# Set true if you want your MonKey data to be saved to a .txt file.");
            file.WriteLine("log-data: true");
            file.Close();
        }
    }
}
