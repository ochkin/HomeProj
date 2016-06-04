using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.FSharp.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PETest
{
    [TestClass]
    public class test68
    {
        private static void Print<T>(IEnumerable<IEnumerable<T>> val)
        {
            foreach (var item in val)
            {
                foreach (var i in item)
                    Console.Write(" {0}", i);
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void testPermitations()
        {
            var all = prb68.permutationsOfN(2);
            Print(all);
        }

        [TestMethod]
        public void testSimplePermutations()
        {
            var init = ListModule.OfArray(new[] { 1, 2, 3 });
            var simple = prb68.simplePerm(init);
            Print(simple);
        }

        [TestMethod]
        public void testIntegration()
        {
            //foreach (var ring in prb68.result)
            //    Console.WriteLine(ring);
            Console.WriteLine(prb68.result);
        }
    }
}
