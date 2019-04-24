using NUnit.Framework;

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
    }
}
