using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string input =Program.PrintsName("Ivan");
            Assert.AreEqual("Hello, Ivan.",input);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string input = Program.PrintsName("Gosho");
            Assert.AreEqual("Hello, Gosho.", input);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string input = Program.PrintsName("Petar");
            Assert.AreEqual("Hello, Petar.", input);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string input = Program.PrintsName("123");
            Assert.AreEqual("Invalid name.", input);
        }
    }
}
