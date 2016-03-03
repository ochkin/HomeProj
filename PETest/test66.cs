using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class test66
    {
        [TestMethod]
        public void testFindMinX2()
        {
            Assert.AreEqual(3ul, prb66.findMinX2(2));
            Assert.AreEqual(2ul, prb66.findMinX2(3));
            Assert.AreEqual(9ul, prb66.findMinX2(5));
            Assert.AreEqual(5ul, prb66.findMinX2(6));
            Assert.AreEqual(8ul, prb66.findMinX2(7));
            Assert.AreEqual(649ul, prb66.findMinX2(13));

            Assert.AreEqual(1766319049ul, prb66.findMinX2(61));
        }
    }
}
