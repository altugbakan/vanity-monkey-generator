using System.Xml;
using System.Linq;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Collections.Generic;

using Svg;

namespace VanityMonKeyGenerator
{
    public static class Drawing
    {
        public static void DrawMonKey(List<string> accessoryList, PictureBox pictureBox)
        {
            accessoryList = ParseAccessoryList(accessoryList);
            Image canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(canvas);

            foreach (string accessory in accessoryList)
            {
                var svg = GetAccessorySvg(accessory);
                graphics.DrawImage(svg.Draw(pictureBox.Width, pictureBox.Height), 0, 0);
            }

            pictureBox.Image = canvas;
        }

        private static SvgDocument GetAccessorySvg(string accessory)
        {
            ResourceSet accessoryList = Accessories.GetAccessoryList();
            string svgString = (string)accessoryList.GetObject(accessory);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(svgString);
            return SvgDocument.Open(doc);
        }

        private static List<string> ParseAccessoryList(List<string> accessoryList)
        {
            List<string> parsedAccessories = new List<string>();
            // Tail
            parsedAccessories.Add("Tail");
            // Tail Accessory
            if (accessoryList.Any(acc => acc.Contains("Tails")))
            {
                string tail = accessoryList.First(acc => acc.Contains("Tails"));
                if (!tail.Contains("Any") && !tail.Contains("None"))
                {
                    parsedAccessories.Add("Tails-TailSock");
                }
            }
            // Legs
            parsedAccessories.Add("Legs");
            // Arms
            parsedAccessories.Add("Arms");
            // Body Upper
            parsedAccessories.Add("BodyUpper");
            // Shirts-Pants Accessory
            if (accessoryList.Any(acc => acc.Contains("ShirtsPants")))
            {
                string shirtsPants = accessoryList.First(acc => acc.Contains("ShirtsPants"));
                if (!shirtsPants.Contains("Any") && !shirtsPants.Contains("None"))
                {
                    parsedAccessories.Add(shirtsPants);
                    if (shirtsPants.RemovesLegs())
                    {
                        parsedAccessories.Remove("Legs");
                    }
                }
            }
            // Misc Above Shirts-Pants
            if (accessoryList.Any(acc => acc.Contains("Misc")))
            {
                string misc = accessoryList.First(acc => acc.Contains("Misc"));
                if (misc.IsAboveShirtsPants())
                {
                    if (!misc.Contains("Any") && !misc.Contains("None"))
                    {
                        parsedAccessories.Add(misc);
                    }
                }
            }
            // Ears
            parsedAccessories.Add("Ears");
            // Face
            parsedAccessories.Add("Face");
            // Eyes
            parsedAccessories.Add("Eyes");
            // Glasses Accessory
            if (accessoryList.Any(acc => acc.Contains("Glasses")))
            {
                string glasses = accessoryList.First(acc => acc.Contains("Glasses"));
                if (!glasses.Contains("Any") && !glasses.Contains("None"))
                {
                    parsedAccessories.Add(glasses);
                    if (glasses.RemovesEyes())
                    {
                        parsedAccessories.Remove("Eyes");
                    }
                }
            }
            // Nose
            parsedAccessories.Add("Nose");
            // Mouth
            if (accessoryList.Any(acc => acc.Contains("Mouths")))
            {
                if (!accessoryList.First(acc => acc.Contains("Mouths")).Contains("Any") &&
                    !accessoryList.First(acc => acc.Contains("Mouths")).Contains("None"))
                {
                    parsedAccessories.Add(accessoryList.First(acc => acc.Contains("Mouths")));
                }
            }
            // Hat
            if (accessoryList.Any(acc => acc.Contains("Hats")))
            {
                string hat = accessoryList.First(acc => acc.Contains("Hats"));
                if (!hat.Contains("Any") && !hat.Contains("None"))
                {
                    parsedAccessories.Add(hat);
                }
            }
            // Foot Left
            parsedAccessories.Add("FootLeft");
            // Foot Right
            parsedAccessories.Add("FootRight");
            // Shoes Accessory
            if (accessoryList.Any(acc => acc.Contains("Shoes")))
            {
                string shoes = accessoryList.First(acc => acc.Contains("Shoes"));
                if (!shoes.Contains("Any") && !shoes.Contains("None"))
                {
                    parsedAccessories.Add(shoes);
                    parsedAccessories.Remove("FootLeft");
                    parsedAccessories.Remove("FootRight");
                }
            }
            // Hand Left
            parsedAccessories.Add("HandLeft");
            // Hand Right
            parsedAccessories.Add("HandRight");
            // Misc Above Hands
            if (accessoryList.Any(acc => acc.Contains("Misc")))
            {
                string misc = accessoryList.First(acc => acc.Contains("Misc"));
                if (misc.IsAboveHands())
                {
                    if (!misc.Contains("Any") && !misc.Contains("None"))
                    {
                        parsedAccessories.Add(misc);
                        if (misc.RemovesHands())
                        {
                            parsedAccessories.Remove("HandLeft");
                            parsedAccessories.Remove("HandRight");
                        }
                        else if (misc.RemovesHandRight())
                        {
                            parsedAccessories.Remove("HandRight");
                        }
                        else if (misc.RemovesHandLeft())
                        {
                            parsedAccessories.Remove("HandLeft");
                        }
                    }
                }
            }

            return parsedAccessories;
        }

        private static bool RemovesLegs(this string accessory)
        {
            List<string> removesLegsList = new List<string>()
            {
                "ShirtsPants-PantsBusinessBlue", "ShirtsPants-PantsFlower"
            };

            return removesLegsList.Contains(accessory);
        }

        private static bool RemovesEyes(this string accessory)
        {
            List<string> removesEyesList = new List<string>()
            {
                "Glasses-SunglassesAviatorCyan", "Glasses-SunglassesAviatorGreen", "Glasses-SunglassesAviatorYellow",
                "Glasses-SunglassesThug"
            };

            return removesEyesList.Contains(accessory);
        }

        private static bool RemovesHands(this string accessory)
        {
            List<string> removesHandsList = new List<string>()
            {
                "Misc-BananaHands", "Misc-Club", "Misc-Flamethrower", "Misc-GlovesWhite"
            };

            return removesHandsList.Contains(accessory);
        }

        private static bool RemovesHandRight(this string accessory)
        {
            List<string> removesHandRightList = new List<string>()
            {
                "Misc-BananaRightHand", "Misc-Microphone", "Misc-WhiskyRight"
            };

            return removesHandRightList.Contains(accessory);
        }

        private static bool RemovesHandLeft(this string accessory)
        {
            return accessory == "Misc-Guitar";
        }

        private static bool IsAboveShirtsPants(this string accessory)
        {
            List<string> aboveShirtsPantsList = new List<string>()
            {
                "Misc-Camera", "Misc-NecklaceBoss", "Misc-TieCyan", "Misc-TiePink"
            };

            return aboveShirtsPantsList.Contains(accessory);
        }

        private static bool IsAboveHands(this string accessory)
        {
            List<string> aboveHandsList = new List<string>()
            {
                "Misc-BananaHands", "Misc-BananaRightHand", "Misc-Bowtie", "Misc-Club", "Misc-Flamethrower",
                "Misc-GlovesWhite", "Misc-Guitar", "Misc-Microphone", "Misc-WhiskyRight"
            };

            return aboveHandsList.Contains(accessory);
        }
    }
}
