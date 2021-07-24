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

            bool expected = true;
            bool actual = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.AreEqual(expected, actual);

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

            expected = true;
            actual = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.AreEqual(expected, actual);

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

            expected = false;
            actual = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.AreEqual(expected, actual);

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

            expected = true;
            actual = Accessories.AccessoriesMatching(requestedAccessories, obtainedAccessories);
            Assert.AreEqual(expected, actual);
        }
    }
}
