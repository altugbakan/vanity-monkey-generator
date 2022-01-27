using Microsoft.VisualStudio.TestTools.UnitTesting;

using static VanityMonKeyGenerator.Requests;

namespace Tests
{
    [TestClass]
    public class RequestsTests
    {
        [TestMethod]
        public void GetEstimatedTimeTest()
        {
            // Seconds case.
            string expected = "10 seconds";
            string actual = GetEstimatedTime(5, 10, 10);
            Assert.AreEqual(expected, actual, "Expected time is wrong in seconds case.");

            // Minutes case.
            expected = "2 minutes";
            actual = GetEstimatedTime(5, 10, 120);
            Assert.AreEqual(expected, actual, "Expected time is wrong in minutes case.");

            // Hours case.
            expected = "5 hours";
            actual = GetEstimatedTime(5, 10, 18000);
            Assert.AreEqual(expected, actual, "Expected time is wrong in hours case.");

            // Negative case.
            expected = "Any time now";
            actual = GetEstimatedTime(11, 10, 10);
            Assert.AreEqual(expected, actual, "Expected time is wrong in negative case.");
        }

    }

}
