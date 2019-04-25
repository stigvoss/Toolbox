using NUnit.Framework;

namespace Toolbox.Extensions.Tests
{
    class A_ByteArray
    {
        [Test]
        public void Can_Convert_To_Hex()
        {
            var bytes = new byte[] { 222, 173, 190, 239 };
            Assert.That(bytes.ToHex(), Is.EqualTo("deadbeef"));
        }
    }
}
