using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class test60
    {
        [TestMethod]
        public void TestHasDivisors()
        {
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.hasDivisors(1));
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.hasDivisors(5));
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.hasDivisors(67));
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.hasDivisors(83));

            Assert.IsTrue(Ochkin.ProjectEuler.prb60.hasDivisors(4));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60.hasDivisors(49));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60.hasDivisors(100));
        }

        [TestMethod]
        public void TestIsPrime()
        {
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.isPrime(1));

            Assert.IsTrue(Ochkin.ProjectEuler.prb60.isPrime(5));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60.isPrime(67));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60.isPrime(83));

            Assert.IsFalse(Ochkin.ProjectEuler.prb60.isPrime(4));
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.isPrime(49));
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.isPrime(100));
        }

        [TestMethod]
        public void TestTraverse5N()
        {
            var actual = Ochkin.ProjectEuler.prb60.traverse5N.ElementAt(5);
            Assert.IsTrue(0 < actual.Item1);
            Assert.IsTrue(actual.Item1 < actual.Item2);
            Assert.IsTrue(actual.Item2 < actual.Item3);
            Assert.IsTrue(actual.Item3 < actual.Item4);
            Assert.IsTrue(actual.Item4 < actual.Item5);

            Func<Tuple<int,int,int,int,int>, int> sumUp =
                tuple => tuple.Item1+tuple.Item2 + tuple.Item3+tuple.Item4+tuple.Item5;
            var second = Ochkin.ProjectEuler.prb60.traverse5N.ElementAt(6);
            Assert.IsTrue(sumUp(actual) < sumUp(second));
        }

        private static bool isPrime(int n)
        {
            for (int i = 2; i < n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        [TestMethod]
        public void TestTraverse5Prime()
        {
            var a = Ochkin.ProjectEuler.prb60.traverse5Prime.ElementAt(25);
            Assert.IsTrue(isPrime(a.Item1));
            Assert.IsTrue(isPrime(a.Item2));
            Assert.IsTrue(isPrime(a.Item3));
            Assert.IsTrue(isPrime(a.Item4));
            Assert.IsTrue(isPrime(a.Item5));
        }
        
        [TestMethod]
        public void TestIsGoodPair()
        {
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.isGoodPair(5, 100));
            Assert.IsTrue(Ochkin.ProjectEuler.prb60.isGoodPair(109, 673));
        }

        [TestMethod]
        public void TestIsGoodSet()
        {
            //Assert.IsFalse(Ochkin.ProjectEuler.prb60.isGoodSet(Tuple.Create(3,7,109,673,1000)));
            Assert.IsFalse(Ochkin.ProjectEuler.prb60.isGoodSet(3, 7, 109, 673, 1000));
        }
    }
}
