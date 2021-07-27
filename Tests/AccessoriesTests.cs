using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using VanityMonKeyGenerator;

namespace Tests
{
    [TestClass]
    public class AccessoriesTests
    {
        [TestMethod]
        public void AccessoriesMatchingTest()
        {
            // Matching case.
            List<string> requestedAccessories = new List<string>()
            {
                "Glasses-SunglassesAviatorGreen",
                "Hats-Crown",
                "Misc-NecklaceBoss",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-TshirtShortWhite",
                "Shoes-SneakersGreen",
                "Tails-TailSock"
            };

            List<string> obtainedAccessories = new List<string>()
            {
                "Glasses-SunglassesAviatorGreen",
                "Hats-Crown",
                "Misc-NecklaceBoss",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-TshirtShortWhite",
                "Shoes-SneakersGreen",
                "Tails-TailSock"
            };

            bool result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result, "Matching case does not match.");

            // Any case.
            requestedAccessories = new List<string>()
            {
                "Glasses-Any",
                "Hats-Crown",
                "Misc-NecklaceBoss",
                "Mouths-Any",
                "ShirtsPants-TshirtShortWhite",
                "Shoes-Any",
                "Tails-TailSock"
            };

            obtainedAccessories = new List<string>()
            {
                "Glasses-SunglassesAviatorGreen",
                "Hats-Crown",
                "Misc-NecklaceBoss",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-TshirtShortWhite",
                "Shoes-None",
                "Tails-TailSock"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result, "Any case does not match.");

            // None case.
            requestedAccessories = new List<string>()
            {
                "Glasses-None",
                "Hats-Crown",
                "Misc-NecklaceBoss",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-TshirtShortWhite",
                "Shoes-Any",
                "Tails-TailSock"
            };

            obtainedAccessories = new List<string>()
            {
                "Glasses-SunglassesAviatorGreen",
                "Hats-Crown",
                "Misc-NecklaceBoss",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-TshirtShortWhite",
                "Shoes-SneakersGreen",
                "Tails-TailSock"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsFalse(result, "None case does match.");

            // Single case.
            requestedAccessories = new List<string>()
            {
                "Glasses-None",
                "Hats-None",
                "Misc-None",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-None",
                "Tails-None"
            };

            obtainedAccessories = new List<string>()
            {
                "Glasses-None",
                "Hats-None",
                "Misc-None",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-None",
                "Tails-None"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result, "Single case does not match.");

            // Multiple case.
            requestedAccessories = new List<string>()
            {
                "Glasses-GlassesNerdCyan", "Glasses-GlassesNerdPink",
                "Hats-None",
                "Misc-Bowtie", "Misc-Camera",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-SneakersBlue", "Shoes-SneakersGreen", "Shoes-SneakersRed",
                "Tails-None"
            };

            obtainedAccessories = new List<string>()
            {
                "Glasses-GlassesNerdPink",
                "Hats-None",
                "Misc-Bowtie",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-SneakersGreen",
                "Tails-None"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result, "Multiple case does not match.");

            // Multiple missing case.
            requestedAccessories = new List<string>()
            {
                "Glasses-GlassesNerdCyan", "Glasses-GlassesNerdPink",
                "Hats-Fedora", "Hats-FedoraLong",
                "Misc-Bowtie", "Misc-Camera",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-SneakersBlue", "Shoes-SneakersGreen", "Shoes-SneakersRed",
                "Tails-None"
            };

            obtainedAccessories = new List<string>()
            {
                "Glasses-GlassesNerdPink",
                "Hats-None",
                "Misc-Bowtie",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-SneakersGreen",
                "Tails-None"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsFalse(result, "Multiple missing case does match.");

            // Multiple none case.
            requestedAccessories = new List<string>()
            {
                "Glasses-GlassesNerdCyan", "Glasses-GlassesNerdPink",
                "Hats-None","Hats-Crown",
                "Misc-Bowtie", "Misc-Camera", "Misc-None",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-SneakersBlue", "Shoes-SneakersGreen", "Shoes-SneakersRed",
                "Tails-None"
            };

            obtainedAccessories = new List<string>()
            {
                "Glasses-GlassesNerdPink",
                "Hats-None",
                "Misc-Bowtie",
                "Mouths-SmileBigTeeth",
                "ShirtsPants-None",
                "Shoes-SneakersGreen",
                "Tails-None"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result, "Multiple none case does not match.");
        }


        [TestMethod]
        public void ObtainedAcessoriesTest()
        {
            // No Glasses, Cap Carlos, Bowtie, Meh, Overalls Red, No Shoes, Tail Sock
            string svg = Properties.Resources.ban_16ixhfsdx8xha4btc83d3d666uq5fnf5fbeu9xoy4idua3bdifyjia4n7ewh;

            List<string> expected = new List<string>()
            {
                "Glasses-None", "Hats-CapCarlos", "Misc-Bowtie", "Mouths-Meh",
                "ShirtsPants-OverallsRed", "Shoes-None", "Tails-TailSock"
            };

            List<string> actual = Accessories.ObtainedAccessories(svg);
            CollectionAssert.AreEquivalent(expected, actual, "Obtained accessories are not equal.");
        }

        [TestMethod]
        public void GetAccessoryChanceTest()
        {
            // Glasses case.
            string accessory = "Glasses-SunglassesAviatorCyan";
            double expected = 0.25 * 1 / 8; // Category Chance * Accessory Weight / Category Weight
            double actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Glasses chance is wrong."); // 1% error is OK.

            // Hats case.
            accessory = "Hats-Cap";
            expected = 0.35 * 0.8 / 19.4;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Hats chance is wrong."); // 1% error is OK.

            // Misc case.
            accessory = "Misc-TieCyan";
            expected = 0.3 * 1 / 11.29;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Misc chance is wrong."); // 1% error is OK.

            // Mouths case.
            accessory = "Mouths-SmileNormal";
            expected = 1 * 1 / 5.56;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Mouths chance is wrong."); // 1% error is OK.

            // Shirts-Pants case.
            accessory = "ShirtsPants-OverallsBlue";
            expected = 0.25 * 1 / 6;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Shirts-Pants chance is wrong."); // 1% error is OK.

            // Shoes case.
            accessory = "Shoes-SneakersBlue";
            expected = 0.22 * 1 / 6;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Shoes chance is wrong."); // 1% error is OK.

            // Tails case.
            accessory = "Tails-TailSock";
            expected = 0.2 * 1 / 1;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Tails chance is wrong."); // 1% error is OK.

            // Any case.
            accessory = "Misc-Any";
            expected = 1; // Always
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "Any chance is wrong."); // 1% error is OK.

            // None case.
            accessory = "Shoes-None";
            expected = 1 - 0.22;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01, "None chance is wrong."); // 1% error is OK.
        }

        [TestMethod]
        public void GetMonKeyChanceTest()
        {
            // Simple case.
            List<string> accessories = new List<string>()
            {
                "Glasses-None", "Hats-Any", "Misc-Club", "Mouths-Meh",
                "ShirtsPants-Any", "Shoes-SneakersRed", "Tails-None"
            };

            double expected =
                0.75 *              // Glasses
                1 *                 // Hats
                0.3 * 1 / 11.29 *   // Misc
                1 * 1 / 5.56 *      // Mouths
                1 *                 // Shirts-Pants
                0.22 * 1 / 6 *      // Shoes
                (1 - 0.2);          // Tails
            double actual = Accessories.GetMonKeyChance(accessories);
            Assert.AreEqual(expected, actual, expected * 0.01, "MonKey chance is wrong."); // 1% error is OK.          

            // Expert case.
            accessories = new List<string>()
            {
                "Glasses-None", "Hats-Any", "Misc-None", "Misc-Club", "Mouths-Meh", "ShirtsPants-Any",
                "Shoes-SneakersRed", "Shoes-SneakersBlue", "Tails-None"
            };

            expected =
                0.75 *              // Glasses
                1 *                 // Hats
                (0.7 + 0.3 * 1 / 11.29) *   // Misc
                1 * 1 / 5.56 *      // Mouths
                1 *                 // Shirts-Pants
                0.22 * (1 + 1) / 6 *      // Shoes
                (1 - 0.2);          // Tails
            actual = Accessories.GetMonKeyChance(accessories);
            Assert.AreEqual(expected, actual, expected * 0.01, "MonKey chance is wrong."); // 1% error is OK.
        }

        [TestMethod]
        public void GetMonKeyRarityTest()
        {
            // Rarest case.
            List<string> accessories = new List<string>()
            {
                "Glasses-Monocle", "Hats-BeanieHippie", "Misc-Flamethrower", "Mouths-Joint",
                "ShirtsPants-BlueOveralls", "Shoes-SneakersBlue", "Tails-TailSock"
            };
            ulong actual = Accessories.GetMonKeyRarity(accessories);
            Assert.AreNotEqual(0, actual, "Rarity is 0");
        }

    }
}
