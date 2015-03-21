using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTrack.Helpers;

namespace ServerTrack1.Tests
{
    [TestClass]
    public class TestUtilities
    {
        [TestMethod]
        public void Test_GetAverageSimple()
        {
            int result = Utilities.GetAverage(100, 5, 40);
            Assert.AreEqual(90, result);
        }

        [TestMethod]
        public void Test_GetAverageLarge()
        {
            int result = Utilities.GetAverage(10000000, 5, 40);
            Assert.AreEqual(8333340, result);
        }
    }
}
