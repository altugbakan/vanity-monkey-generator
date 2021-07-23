using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Xml;
using System.Linq;
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
            accessories.RemoveAll(accessory => !accessory.Contains("-"));
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
            foreach (string accessory in requestedAccessories.Where(
                accessory => !accessory.Contains("Any") && !accessory.Contains("None")))
            {
                if (!obtainedAccessories.Contains(accessory))
                {
                    return false;
                }
            }
            foreach (string accessory in requestedAccessories.Where(
                accessory => accessory.Contains("None")))
            {
                if (obtainedAccessories.Any(accessory => 
                    accessory.StartsWith(accessory.Remove(accessory.IndexOf('-')))))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
