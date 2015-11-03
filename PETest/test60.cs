using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class test60
    {
        [TestMethod]
        public void TestTraverse5N()
        {
            var Ns = Ochkin.ProjectEuler.prb60dp5.traverse5N(1, 2, 3, 4, 5);
            var actual = Ns.ElementAt(5);
            Assert.IsTrue(0 < actual.Item1);
            Assert.IsTrue(actual.Item1 < actual.Item2);
            Assert.IsTrue(actual.Item2 < actual.Item3);
            Assert.IsTrue(actual.Item3 < actual.Item4);
            Assert.IsTrue(actual.Item4 < actual.Item5);
        }

        private static bool isPrime(int n)
        {
            for (int i = 2; i < n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }
        
        [TestMethod]
        public void TestIsGoodPair()
        {
            Assert.IsFalse(Ochkin.ProjectEuler.prb60dp5.isGoodPair(5, 100));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60dp5.isGoodPair(109, 673));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60dp5.isGoodPair(7, 3));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60dp5.isGoodPair(3, 673));
        }

        [TestMethod]
        public void TestConcatInt()
        {
            Assert.AreEqual<int>(3456, Ochkin.ProjectEuler.prb60dp5.concatInt(34, 56));
            Assert.AreEqual<int>(5690, Ochkin.ProjectEuler.prb60dp5.concatInt(56, 90));
        }
        
    }
}
