using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class test62
    {
        [TestMethod]
        public void TestXor()
        {
            var arr1 = new byte[] { 8,1,4,6,9,0,1,2,2,2,5 };
            var arr2 = arr1.Reverse().ToArray();
            var arr3 = new byte[] {  9, 0, 6, 2, 1, 2, 8, 1, 4, 2, 5 };
            var arr4 = new byte[] { 9, 0, 5, 2, 1, 2, 8, 1, 4, 2, 5 };

            Assert.AreEqual(prb62.xor(arr1), prb62.xor(arr1));
            Assert.AreEqual(prb62.xor(arr1), prb62.xor(arr2));
            Assert.AreEqual(prb62.xor(arr1), prb62.xor(arr3));
            Assert.AreNotEqual(prb62.xor(arr1), prb62.xor(arr4));
            Assert.AreNotEqual(prb62.xor(arr3), prb62.xor(arr4));
        }

        [TestMethod]
        public void TestArePerm()
        {
            var arr1 = new byte[] { 8, 1, 4, 6, 9, 0, 1, 2, 2, 2, 5 };
            var arr2 = arr1.Reverse().ToArray();
            var arr3 = new byte[] { 9, 0, 6, 2, 1, 2, 8, 1, 4, 2, 5 };
            var arr4 = new byte[] { 9, 0, 5, 2, 1, 2, 8, 1, 4, 2, 5 };

            Assert.IsTrue(prb62.arePerm(arr1, arr1));
            Assert.IsTrue(prb62.arePerm(arr1, arr2));
            Assert.IsTrue(prb62.arePerm(arr1, arr3));
            Assert.IsTrue(prb62.arePerm(arr1, arr2));
            Assert.IsFalse(prb62.arePerm(arr1, arr4));
            Assert.IsFalse(prb62.arePerm(arr3, arr4));
        }

        [TestMethod]
        public void TestgroupsByNumDigits()
        {
            var x = prb62.groupsByNumDigits(150);
            int? size = null;
            foreach (var group in x.Skip(1).Take(5))
            {
                if (size.HasValue)
                    Assert.IsTrue(size.Value < group.First().Length);
                size = group.First().Length;

                foreach (var element in group.Skip(1))
                    Assert.AreEqual(size.Value, element.Length);
            }
        }

        [TestMethod]
        public void TestChar2Byte()
        {
            for (byte b = 0; b < 10; b++)
                Assert.AreEqual(b, prb62.char2byte(b.ToString()[0]));
        }

        [TestMethod]
        public void TestCubes()
        {
            int n = 765;
            var n3 = prb62.cubes(n).First();
            Assert.IsNotNull(n3);
            Assert.IsTrue(n3.Any());

            var number = n3
                .Select((b, i) => b * (int)Math.Pow(10.0, n3.Length - i - 1))
                .Sum();
            //var root = (int)(Math.Pow(number, 1.0 / 3));
            //Console.WriteLine("root of {0} = {1}",number, root);
            Assert.AreEqual(n * n * n, number);
        }
    }
}
