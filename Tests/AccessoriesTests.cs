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
            Assert.IsTrue(result);

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
                // No shoes.
                "Tails-TailSock"
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result);

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
            Assert.IsFalse(result);

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
                "Mouths-SmileBigTeeth",
            };

            result = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void ObtainedAcessoriesTest()
        {
            // Meh, Cap Carlos, Bowtie, Overalls Red, Tail Sock
            string svg = Properties.Resources.ban_16ixhfsdx8xha4btc83d3d666uq5fnf5fbeu9xoy4idua3bdifyjia4n7ewh;

            List<string> expected = new List<string>()
            {
                "Mouths-Meh", "Hats-CapCarlos", "Misc-Bowtie",
                "ShirtsPants-OverallsRed", "Tails-TailSock"
            };

            List<string> actual = Accessories.ObtainedAccessories(svg);
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void GetAccessoryChanceTest()
        {
            // Glasses case.
            string accessory = "Glasses-SunglassesAviatorCyan";
            double expected = 0.25 * 1 / 8; // Category Chance * Accessory Weight / Category Weight
            double actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Hats case.
            accessory = "Hats-Cap";
            expected = 0.35 * 0.8 / 19.4;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Misc case.
            accessory = "Misc-TieCyan";
            expected = 0.3 * 1 / 11.29;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Mouths case.
            accessory = "Mouths-Normal";
            expected = 1 * 1 / 5.56;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Shirts Pants case.
            accessory = "ShirtsPants-OverallsBlue";
            expected = 0.25 * 1 / 6;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Shoes case.
            accessory = "Shoes-SneakersBlue";
            expected = 0.22 * 1 / 6;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Tails case.
            accessory = "Tails-TailSock";
            expected = 0.2 * 1 / 1;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // Any case.
            accessory = "Misc-Any";
            expected = 1; // Always
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.

            // None case.
            accessory = "Shoes-None";
            expected = 1 - 0.22;
            actual = Accessories.GetAccessoryChance(accessory);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.
        }

        [TestMethod]
        public void GetMonKeyChanceTest()
        {
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
                1 *                 // Shirts Pants
                0.22 * 1 / 6 *      // Shoes
                (1 - 0.2);          // Tails
            double actual = Accessories.GetMonKeyChance(accessories);
            Assert.AreEqual(expected, actual, expected * 0.01); // 1% error is OK.
        }

    }
}
