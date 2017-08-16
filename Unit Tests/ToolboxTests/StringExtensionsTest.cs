using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Extensions;

namespace ToolboxTests
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void UpperToTitleCaseTest()
        {
            string expected = "Hello World!";
            string target = "HELLO WORLD!";

            string result = target.ToTitleCase();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LowerToTitleCaseTest()
        {
            string expected = "Hello World!";
            string target = "hello world!";

            string result = target.ToTitleCase();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MixedToTitleCaseTest()
        {
            string expected = "Hello World!";
            string target = "hElLo woRLd!";

            string result = target.ToTitleCase();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EmptyToTitleCaseTest()
        {
            string expected = string.Empty;
            string target = string.Empty;

            string result = target.ToTitleCase();

            Assert.AreEqual(expected, result);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void NullToTitleCaseTest()
        {
            string expected = null;
            string target = null;

            string result = target.ToTitleCase();

            Assert.AreEqual(expected, result);
        }
    }
}
