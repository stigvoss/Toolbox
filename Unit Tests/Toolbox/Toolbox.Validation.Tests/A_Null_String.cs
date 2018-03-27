using System;
using NUnit.Framework;

namespace Toolbox.Validation.Tests
{
    [TestFixture]
    public class A_Null_String
    {
        string nullString;

        [SetUp]
        public void TestInitalize()
        {
            nullString = null;
        }

        [Test]
        public void Ensured_Null()
        {
            Ensure.IsNull(nullString);

            Assert.IsNull(nullString);
        }

        [Test]
        public void Ensured_Not_Null()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNotNull(nullString);
            });

            Assert.IsNull(nullString);
        }

        [Test]
        public void Ensured_Null_Or_Empty()
        {
            Ensure.IsNullOrEmpty(nullString);

            Assert.IsNull(nullString);
        }

        [Test]
        public void Ensured_Not_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNotNullOrEmpty(nullString);
            });

            Assert.IsNull(nullString);
        }

        [Test]
        public void Ensured_Null_Or_WhiteSpace()
        {
            Ensure.IsNullOrWhiteSpace(nullString);

            Assert.IsNull(nullString);
        }

        [Test]
        public void Ensured_Not_Null_Or_WhiteSpace()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNotNullOrWhiteSpace(nullString);
            });

            Assert.IsNull(nullString);
        }
    }
}
