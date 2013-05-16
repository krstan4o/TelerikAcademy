using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] array = {1,2,3,4};
            int input = Program.CountNumber(array,4);
            Assert.AreEqual(1, input);
        }
        [TestMethod]
        public void TestMethod2()
        {
            int[] array = { 1, 1, 1, 1 };
            int input = Program.CountNumber(array, 1);
            Assert.AreEqual(4, input);
        }
        [TestMethod]
        public void TestMethod3()
        {
            int[] array = { 1, 2, 3, 4 };
            int input = Program.CountNumber(array, 5);
            Assert.AreEqual(0, input);
        }
        [TestMethod]
        public void TestMethod4()
        {
            int[] array = { 3, 2, 3, 4 };
            int input = Program.CountNumber(array, 3);
            Assert.AreEqual(2, input);
        }
    }
}
