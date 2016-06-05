using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class test70
    {
        [TestMethod]
        public void testArePerm()
        {
            Assert.IsTrue(prb70.arePerm(79180, 87109));

            Assert.IsFalse(prb70.arePerm(78180, 87109));
        }
    }
}
