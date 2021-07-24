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
    }
}
