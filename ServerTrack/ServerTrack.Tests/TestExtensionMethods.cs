using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTrack;

namespace ServerTrack1.Tests
{
    /// <summary>
    /// Summary description for TestExtensionMethods
    /// </summary>
    [TestClass]
    public class TestExtensionMethods
    {
        [TestMethod]
        public void Test_TopOfTheMinute()
        {
            DateTime input = new DateTime(2015, 2, 2, 11, 11, 11);
            DateTime expected = new DateTime(2015, 2, 2, 11, 11, 0);

            DateTime result = input.TopOfTheMinute();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_TopOfTheHour()
        {
            DateTime input = new DateTime(2015, 2, 2, 11, 11, 11);
            DateTime expected = new DateTime(2015, 2, 2, 11, 0, 0);

            DateTime result = input.TopOfTheHour();
            Assert.AreEqual(expected, result);
        }
    }
}
