using NUnit.Framework;

namespace Toolbox.Extensions.Tests
{
    class A_HexString
    {
        [Test]
        public void Are_0x_At_Start_Of_HexString_Ignored()
        {
            Assert.AreEqual("aa".FromHex(), "0xaa".FromHex());
        }

        [Test]
        public void Can_Convert_To_Bytes_No_Leading_0x()
        {
            var hex = "deadbeef";
            var expected = new byte[] { 222, 173, 190, 239 };
            Assert.That(hex.FromHex(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Can_Convert_To_Bytes_With_Leading_0x()
        {
            var hex = "0xdeadbeef";
            var expected = new byte[] { 222, 173, 190, 239 };
            Assert.That(hex.FromHex(), Is.EquivalentTo(expected));
        }
    }
}
