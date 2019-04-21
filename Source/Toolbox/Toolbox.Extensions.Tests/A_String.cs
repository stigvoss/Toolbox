using NUnit.Framework;
using System;

namespace Toolbox.Extensions.Tests
{
    [TestFixture]
    public class A_String
    {
        [Test]
        public void Can_Convert_To_Title_Case()
        {
            var expected = "This Is Uppercase";

            var upperCaseString = "THIS IS UPPERCASE";
            var titleCaseString = upperCaseString.ToTitleCase();

            Assert.AreEqual(expected, titleCaseString);
        }

        [Test]
        public void Can_Convert_To_Byte_Array()
        {
            var expected = new byte[] { 65, 65, 65, 65 };

            var bytes = "AAAA".ToBytes();

            Assert.AreEqual(expected, bytes);
        }

        [Test]
        public void Can_ToTitleCase_Handle_Null()
        {
            var actual = (null as string).ToTitleCase();
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void Can_ToBytes_Handle_Null()
        {
            var actual = (null as string).ToBytes();
            Assert.AreEqual(null, actual);
        }
    }
}
