using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
                var svg = Accessories.GetAccessorySvg(accessory);
                graphics.DrawImage(svg.Draw(pictureBox.Width, pictureBox.Height), 0, 0);
            }

            pictureBox.Image = canvas;
        }

        private static List<string> ParseAccessoryList(List<string> accessoryList)
        {
            List<string> parsedAccessories = new List<string>();
            // Tail
            parsedAccessories.Add("Tail");
            // Tail Accessory
            if (accessoryList.Any(acc => acc.Contains("Tails")))
            {
                string tail = accessoryList.First(acc => acc.Contains("Tails"))
                    .Replace("Tails-", "");
                if (tail != "Any")
                {
                    parsedAccessories.Add("TailSock");
                }
            }
            // Legs
            parsedAccessories.Add("Legs");
            // Arms
            parsedAccessories.Add("Arms");
            // Body Upper
            parsedAccessories.Add("BodyUpper");
            // Shirts Pants Accessory
            if (accessoryList.Any(acc => acc.Contains("Shirt-Pants")))
            {
                string shirtsPants = accessoryList.First(acc => acc.Contains("Shirt-Pants"))
                    .Replace("Shirt-Pants-", "");
                if (shirtsPants != "Any")
                {
                    parsedAccessories.Add(shirtsPants);
                    if (shirtsPants.RemovesLegs())
                    {
                        parsedAccessories.Remove("Legs");
                    }
                }         
            }
            // Misc Above Shirts Pants
            if (accessoryList.Any(acc => acc.Contains("Misc")))
            {
                string misc = accessoryList.First(acc => acc.Contains("Misc"))
                    .Replace("Misc-", "");
                if (misc.IsAboveShirtsPants())
                {
                    if (misc != "Any")
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
                string glasses = accessoryList.First(acc => acc.Contains("Glasses"))
                    .Replace("Glasses-", "");
                if (glasses != "Any")
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
            if (accessoryList.First(acc => acc.Contains("Mouths")).Replace("Mouths-", "") != "Any")
            {
                parsedAccessories.Add(accessoryList.First(acc => acc.Contains("Mouths"))
                        .Replace("Mouths-", ""));
            }
            // Hat
            if (accessoryList.Any(acc => acc.Contains("Hats")))
            {
                string hat = accessoryList.First(acc => acc.Contains("Hats"))
                    .Replace("Hats-", "");
                if (hat != "Any")
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
                string shoes = accessoryList.First(acc => acc.Contains("Shoes"))
                    .Replace("Shoes-", "");
                if (shoes != "Any")
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
                string misc = accessoryList.First(acc => acc.Contains("Misc"))
                    .Replace("Misc-", "");
                if (misc.IsAboveHands())
                {
                    if (misc != "Any")
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
                "PantsBusinessBlue", "PantsFlower"
            };

            return removesLegsList.Contains(accessory);
        }

        private static bool RemovesEyes(this string accessory)
        {
            List<string> removesEyesList = new List<string>()
            {
                "SunglassesAviatorCyan", "SunglassesAviatorGreen", "SunglassesAviatorYellow"
            };

            return removesEyesList.Contains(accessory);
        }

        private static bool RemovesHands(this string accessory)
        {
            List<string> removesHandsList = new List<string>()
            {
                "BananaHands", "Club", "Flamethrower", "GlovesWhite"
            };

            return removesHandsList.Contains(accessory);
        }

        private static bool RemovesHandRight(this string accessory)
        {
            List<string> removesHandRightList = new List<string>()
            {
                "BananaRightHand", "Microphone", "WhiskyRight"
            };

            return removesHandRightList.Contains(accessory);
        }

        private static bool RemovesHandLeft(this string accessory)
        {
            return accessory == "Guitar";
        }

        private static bool IsAboveShirtsPants(this string accessory)
        {
            List<string> aboveShirtsPantsList = new List<string>()
            {
                "Camera", "NecklaceBoss", "TieCyan", "TiePink"
            };

            return aboveShirtsPantsList.Contains(accessory);
        }

        private static bool IsAboveHands(this string accessory)
        {
            List<string> aboveHandsList = new List<string>()
            {
                "BananaHands", "Bowtie", "Club", "Flamethrower",
                "GlovesWhite", "Guitar", "Microphone", "WhiskyRight"
            };

            return aboveHandsList.Contains(accessory);
        }
    }
}
