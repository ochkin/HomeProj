using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PETest
{
    [TestClass]
    public class testPrime
    {
        [TestMethod]
        public void TestIsPrime()
        {
            Assert.IsTrue(Prime.IsPrime(2));
            Assert.IsTrue(Prime.IsPrime(3));
            Assert.IsTrue(Prime.IsPrime(5));
            Assert.IsTrue(Prime.IsPrime(17));
            Assert.IsTrue(Prime.IsPrime(109673));
            Assert.IsTrue(Prime.IsPrime(673109));
            Assert.IsTrue(Prime.IsPrime(7109));
            Assert.IsTrue(Prime.IsPrime(1097));

            Assert.IsFalse(Prime.IsPrime(4));
            Assert.IsFalse(Prime.IsPrime(111));
            Assert.IsFalse(Prime.IsPrime(198));
            Assert.IsFalse(Prime.IsPrime(10000));
            Assert.IsFalse(Prime.IsPrime(20001));
            Assert.IsFalse(Prime.IsPrime(1));
        }

        [TestMethod]
        public void TestGetPrimeN()
        {
            var snd = Prime.GetPrimeN(50);
            var fst = Prime.GetPrimeN(49);
            Assert.IsTrue(100 < fst);
            Assert.IsTrue(fst < snd);
            Assert.IsTrue(Prime.IsPrime(snd));
        }

        [TestMethod]
        public void TestBinarySearch()
        {
            var array = new[] { 2, 4, 5, 8, 12, 34, 60, 61, 62, 90 };
            var r1 = Prime.BinarySearch(13, array);
            Assert.IsTrue(Microsoft.FSharp.Core.FSharpOption<int>.get_IsNone(r1));
            var r2 = Prime.BinarySearch(90, array);
            Assert.IsTrue(Microsoft.FSharp.Core.FSharpOption<int>.get_IsSome(r2));
        }

        [TestMethod]
        public void TestAtkin()
        {
            var candidates = new[] {
                2,  3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83,
                89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179,
                181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277,
                281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389,
                397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
                503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617,
                619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739,
                743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859,
                863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991,
                997 };

            var primesTPL = Prime.ListAtkinTPL(1000);
            Assert.AreEqual(primesTPL.Length, candidates.Length);
            for (int i = 0; i < primesTPL.Length; i++)
                Assert.AreEqual(candidates[i], primesTPL[i]);
        }

        [TestMethod]
        public void TestSpeed()
        {
            int topCandidate = 10000000;

            DateTime start, end;

            //start = DateTime.Now;
            //var x = Prime.IsPrime(topCandidate);
            //end = DateTime.Now;
            //Assert.IsFalse(x);
            //Console.WriteLine("Time for classic = " + (end - start));
            
            start = DateTime.Now;
            var y = Prime.ListAtkinTPL(topCandidate);
            end = DateTime.Now;
            Assert.IsTrue(
               Microsoft.FSharp.Core.FSharpOption<int>.get_IsNone(
                   Prime.BinarySearch(topCandidate - 1, y)
                   )
               );
            Console.WriteLine("Time for Atkin - TPL = " + (end - start));

            start = DateTime.Now;
            var zPub = Publish.ListAtkin(topCandidate);
            end = DateTime.Now;
            Assert.IsTrue(
               Microsoft.FSharp.Core.FSharpOption<int>.get_IsNone(
                   Prime.BinarySearch(topCandidate - 1, zPub)
                   )
               );
            Assert.AreEqual(y.Length, zPub.Length);
            Console.WriteLine("Time for Atkin - Async again = " + (end - start));
        }
    }
}
