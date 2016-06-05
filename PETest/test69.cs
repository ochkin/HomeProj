using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PETest
{
    [TestClass]
    public class test69
    {
        private static readonly IDictionary<int, int> phi = new Dictionary<int, int> {
                {1, 0},
                {2, 1},
                {3, 2},
                {4, 2},
                {5, 4},
                {6, 2},
                {7, 6},
                {8, 4},
                {9, 6},
                {10, 4},
            };

        [TestMethod]
        public void testPhi() // 35ms
        {
            foreach (var pair in phi)
                Assert.AreEqual(pair.Value, prb69.phi(pair.Key));
        }
    }
}
