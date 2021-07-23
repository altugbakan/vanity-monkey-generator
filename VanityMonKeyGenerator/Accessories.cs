using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Xml;
using VanityMonKeyGenerator.Properties;

namespace VanityMonKeyGenerator
{
    public static class Accessories
    {
        public static List<string> MatchingAccessories(string monKeySvg)
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
            if (accessories.Contains("SmileTongue"))
            {
                accessories.Remove("SmileNormal");
            }
            if (accessories.Contains("BananaHands"))
            {
                accessories.Remove("BananaRightHand");
            }
            return accessories;
        }

        private static ResourceSet GetAccessoryList()
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            return rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
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
                case "BeanieLong":
                case "BeanieLongBanano":
                case "TshirtLongStripes":
                case "SocksVStripe":
                case "TailSock":
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
    }
}
