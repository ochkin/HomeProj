using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class test65
    {
        [TestMethod]
        public void testSumDigits()
        {
            Assert.AreEqual(14, Util.sumOfDigits(new System.Numerics.BigInteger(365)));
            Assert.AreEqual(1, Util.sumOfDigits(new System.Numerics.BigInteger(1)));
            Assert.AreEqual(45, Util.sumOfDigits(new System.Numerics.BigInteger(123456789000)));
        }
    }
}
