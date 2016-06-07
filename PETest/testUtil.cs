using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class testUtil
    {
        [TestMethod]
        public void testSimplify()
        {
            var half = new Util.Fraction<int>(1, 2);

            var half2 = new Util.Fraction<int>(56019, 2 * 56019);

            Assert.AreEqual(half, Util.Simplify(half2));

            Assert.AreNotEqual(half, Util.Simplify(new Util.Fraction<int>(56019, 114091)));
        }
    }
}
