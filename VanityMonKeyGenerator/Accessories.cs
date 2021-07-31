using System.Linq;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using VanityMonKeyGenerator.Properties;

namespace VanityMonKeyGenerator
{
    public static class Accessories
    {
        public static readonly List<string> Categories = new List<string>()
        {
                "Glasses", "Hats", "Misc", "Mouths",
                "ShirtsPants", "Shoes", "Tails"
        };

        public static List<string> ObtainedAccessories(Dictionary<string, string> response)
        {
            return new List<string>()
            {
                $"Glasses-{response["glasses"].StripTags()}",
                $"Hats-{response["hat"].StripTags()}",
                $"Misc-{response["misc"].StripTags()}",
                $"Mouths-{response["mouth"].StripTags()}",
                $"ShirtsPants-{response["shirt_pants"].StripTags()}",
                $"Shoes-{response["shoes"].StripTags()}",
                $"Tails-{response["tail_accessory"].StripTags()}"
            };
        }

        private static string StripTags(this string accessory)
        {
            if (accessory == "none")
            {
                return "None";
            }
            else
            {
                accessory = accessory.Remove(accessory.IndexOf("["));
                return accessory.ReplaceCase();
            }
        }

        public static ResourceSet GetAccessoryList()
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            return rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        }

        public static bool AccessoriesMatching(List<string> requestedAccessories,
            List<string> obtainedAccessories)
        {
            foreach (string category in Categories)
            {
                if (requestedAccessories.Where(acc => acc.Contains(category)).
                        Any(acc => acc.Contains("Any")))
                {
                    continue;
                }
                else
                {
                    if (requestedAccessories.Where(acc => acc.Contains(category)).
                        Any(acc => obtainedAccessories.Contains(acc)))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static ulong GetMonKeyRarity(List<string> accessories)
        {
            return (ulong)(1.0 / GetMonKeyChance(accessories));
        }

        public static double GetMonKeyChance(List<string> accessories)
        {
            double chance = 1.0;

            foreach (string category in Categories)
            {
                double totalCategoryChance = 0.0;
                foreach (string accessory in accessories.Where(acc => acc.Contains(category)))
                {
                    totalCategoryChance += GetAccessoryChance(accessory);
                }
                if (totalCategoryChance > 0.0)
                {
                    chance *= totalCategoryChance;
                }
            }
            return chance;
        }

        public static double GetAccessoryChance(string accessory)
        {
            if (accessory.Contains("Any"))
            {
                return 1.0;
            }
            else if (accessory.Contains("None"))
            {
                return 1.0 - GetCategoryChance(accessory);
            }
            return GetCategoryChance(accessory) * GetAccessoryWeight(accessory);
        }

        private static double GetCategoryChance(string accessory)
        {
            return accessory.Category() switch
            {
                "Glasses" => 0.25,
                "Hats" => 0.35,
                "Misc" => 0.3,
                "Mouths" => 1.0,
                "ShirtsPants" => 0.25,
                "Shoes" => 0.22,
                "Tails" => 0.2,
                _ => 0.0,
            };
        }

        private static double GetAccessoryWeight(string accessory)
        {
            double accessoryWeight = 0.0;
            switch (accessory)
            {
                case "Glasses-EyePatch":
                    accessoryWeight = 0.5;
                    break;
                case "Glasses-GlassesNerdCyan":
                    accessoryWeight = 1.0;
                    break;
                case "Glasses-GlassesNerdGreen":
                    accessoryWeight = 1.0;
                    break;
                case "Glasses-GlassesNerdPink":
                    accessoryWeight = 1.0;
                    break;
                case "Glasses-Monocle":
                    accessoryWeight = 0.5;
                    break;
                case "Glasses-SunglassesAviatorCyan":
                    accessoryWeight = 1.0;
                    break;
                case "Glasses-SunglassesAviatorGreen":
                    accessoryWeight = 1.0;
                    break;
                case "Glasses-SunglassesAviatorYellow":
                    accessoryWeight = 1.0;
                    break;
                case "Glasses-SunglassesThug":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-Bandana":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-Beanie":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-BeanieBanano":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-BeanieHippie":
                    accessoryWeight = 0.125;
                    break;
                case "Hats-BeanieLong":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-BeanieLongBanano":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-Cap":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapBackwards":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapBanano":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapBebe":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapCarlos":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapHng":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapHngPlus":
                    accessoryWeight = 0.125;
                    break;
                case "Hats-CapKappa":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapPepe":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapRick":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapSmug":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapSmugGreen":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-CapThonk":
                    accessoryWeight = 0.8;
                    break;
                case "Hats-Crown":
                    accessoryWeight = 0.225;
                    break;
                case "Hats-Fedora":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-FedoraLong":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-HatCowboy":
                    accessoryWeight = 1.0;
                    break;
                case "Hats-HatJester":
                    accessoryWeight = 0.125;
                    break;
                case "Hats-HelmetViking":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-BananaHands":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-BananaRightHand":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-Bowtie":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-Camera":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-Club":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-Flamethrower":
                    accessoryWeight = 0.04;
                    break;
                case "Misc-GlovesWhite":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-Guitar":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-Microphone":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-NecklaceBoss":
                    accessoryWeight = 0.75;
                    break;
                case "Misc-TieCyan":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-TiePink":
                    accessoryWeight = 1.0;
                    break;
                case "Misc-WhiskyRight":
                    accessoryWeight = 0.5;
                    break;
                case "Mouths-Cigar":
                    accessoryWeight = 0.5;
                    break;
                case "Mouths-Confused":
                    accessoryWeight = 1.0;
                    break;
                case "Mouths-Joint":
                    accessoryWeight = 0.06;
                    break;
                case "Mouths-Meh":
                    accessoryWeight = 1.0;
                    break;
                case "Mouths-Pipe":
                    accessoryWeight = 0.5;
                    break;
                case "Mouths-SmileBigTeeth":
                    accessoryWeight = 1.0;
                    break;
                case "Mouths-SmileNormal":
                    accessoryWeight = 1.0;
                    break;
                case "Mouths-SmileTongue":
                    accessoryWeight = 0.5;
                    break;
                case "ShirtsPants-OverallsBlue":
                    accessoryWeight = 1.0;
                    break;
                case "ShirtsPants-OverallsRed":
                    accessoryWeight = 1.0;
                    break;
                case "ShirtsPants-PantsBusinessBlue":
                    accessoryWeight = 1.0;
                    break;
                case "ShirtsPants-PantsFlower":
                    accessoryWeight = 1.0;
                    break;
                case "ShirtsPants-TshirtLongStripes":
                    accessoryWeight = 1.0;
                    break;
                case "ShirtsPants-TshirtShortWhite":
                    accessoryWeight = 1.0;
                    break;
                case "Shoes-SneakersBlue":
                    accessoryWeight = 1.0;
                    break;
                case "Shoes-SneakersGreen":
                    accessoryWeight = 1.0;
                    break;
                case "Shoes-SneakersRed":
                    accessoryWeight = 1.0;
                    break;
                case "Shoes-SneakersSwagger":
                    accessoryWeight = 1.0;
                    break;
                case "Shoes-SocksHStripe":
                    accessoryWeight = 1.0;
                    break;
                case "Shoes-SocksVStripe":
                    accessoryWeight = 1.0;
                    break;
                case "Tails-TailSock":
                    accessoryWeight = 1.0;
                    break;
            }
            return accessoryWeight / GetCategoryWeight(accessory);
        }

        private static double GetCategoryWeight(string accessory)
        {
            return accessory.Category() switch
            {
                "Glasses" => 8.0,
                "Hats" => 19.4,
                "Misc" => 11.29,
                "Mouths" => 5.56,
                "ShirtsPants" => 6.0,
                "Shoes" => 6.0,
                "Tails" => 1.0,
                _ => 0.0,
            };
        }

        private static string Category(this string accessory)
        {
            return accessory.Split('-').First();
        }

        public static string OnlyAccessory(this string accessory)
        {
            return accessory.Split('-').Last();
        }

        public static string RemoveSpaces(this string accessory)
        {
            return Regex.Replace(accessory.Split('-').Last(),
                            "([A-Z])", " $1").TrimStart();
        }

        public static string ReplaceCase(this string accessory)
        {
            return Regex.Replace(accessory, @"(^\w|-\w)", m => m.ToString().ToUpper()).Replace("-", "");
        }
    }
}
