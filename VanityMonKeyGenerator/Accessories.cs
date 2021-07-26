using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Xml;
using System.Text.RegularExpressions;
using VanityMonKeyGenerator.Properties;

using Svg;

namespace VanityMonKeyGenerator
{
    public static class Accessories
    {
        public static List<string> ObtainedAccessories(string monKeySvg)
        {
            List<string> accessories = new List<string>();
            foreach (DictionaryEntry accessory in GetAccessoryList())
            {
                if (monKeySvg.ContainsAccessory(accessory))
                {
                    accessories.Add((string)accessory.Key);
                }
            }
            return PruneExtraAccessories(accessories);
        }

        private static List<string> PruneExtraAccessories(List<string> accessories)
        {
            if (accessories.Contains("Mouths-SmileTongue"))
            {
                accessories.Remove("Mouths-SmileNormal");
            }
            if (accessories.Contains("Misc-BananaHands"))
            {
                accessories.Remove("Misc-BananaRightHand");
            }
            // Remove body parts.
            accessories.RemoveAll(accessory => !accessory.Contains("-"));
            // Add none accessories.
            foreach (string category in categories)
            {
                if (!accessories.Any(acc => acc.Contains(category)))
                {
                    accessories.Add($"{category}-None");
                }
            }
            return accessories;
        }

        private static ResourceSet GetAccessoryList()
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            return rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        }

        public static SvgDocument GetAccessorySvg(string accessory)
        {
            ResourceSet accessoryList = GetAccessoryList();
            string svgString = (string)accessoryList.GetObject(accessory);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(svgString);
            return SvgDocument.Open(doc);
        }

        public static bool ContainsAccessory(this string monKeySvg, DictionaryEntry accessory)
        {
            XmlDocument monKeyDocument = new XmlDocument();
            XmlDocument accessoryDocument = new XmlDocument();
            monKeyDocument.LoadXml(monKeySvg);
            accessoryDocument.LoadXml((string)accessory.Value);

            foreach (XmlNode accessoryNode in accessoryDocument.DocumentElement.ChildNodes)
            {
                if (!monKeyDocument.DocumentElement.ChildNodes.Contains(accessoryNode.OuterXml,
                    ((string)accessory.Key).IsColorable()))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsColorable(this string accessory)
        {
            switch (accessory)
            {
                case "Hats-BeanieLong":
                case "Hats-BeanieLongBanano":
                case "ShirtsPants-TshirtLongStripes":
                case "Shoes-SocksVStripe":
                case "Tails-TailSock":
                    return true;
                default:
                    return false;
            }
        }

        private static bool Contains(this XmlNodeList nodeList, string value, bool discardFill)
        {
            // Discard fill values of items.
            if (discardFill && value.Contains("fill"))
            {
                value = value.Remove(value.IndexOf("fill") - 1);
            }
            foreach (XmlNode node in nodeList)
            {
                if (node.InnerXml.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AccessoriesMatching(List<string> requestedAccessories,
            List<string> obtainedAccessories)
        {
            

            foreach (string category in categories)
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
            List<string> categories = new List<string>()
            {
                "Glasses", "Hats", "Misc", "Mouths",
                "ShirtsPants", "Shoes", "Tails"
            };

            foreach (string category in categories)
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
            switch (accessory.Category())
            {
                case "Glasses":
                    return 0.25;
                case "Hats":
                    return 0.35;
                case "Misc":
                    return 0.3;
                case "Mouths":
                    return 1.0;
                case "ShirtsPants":
                    return 0.25;
                case "Shoes":
                    return 0.22;
                case "Tails":
                    return 0.2;
                default:
                    return 0.0;
            }

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
            switch (accessory.Category())
            {
                case "Glasses":
                    return 8.0;
                case "Hats":
                    return 19.4;
                case "Misc":
                    return 11.29;
                case "Mouths":
                    return 5.56;
                case "ShirtsPants":
                    return 6.0;
                case "Shoes":
                    return 6.0;
                case "Tails":
                    return 1.0;
                default:
                    return 0.0;
            }
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

        private static List<string> categories = new List<string>()
        {
                "Glasses", "Hats", "Misc", "Mouths",
                "ShirtsPants", "Shoes", "Tails"
        };
    }
}
