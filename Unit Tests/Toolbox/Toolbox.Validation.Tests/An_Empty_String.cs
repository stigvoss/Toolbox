using System;
using NUnit.Framework;

namespace Toolbox.Validation.Tests
{
    [TestFixture]
    public class An_Empty_String
    {
        string candidateString;

        [SetUp]
        public void TestInitalize()
        {
            candidateString = string.Empty;
        }

        [Test]
        public void Ensured_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Ensure.IsNull(candidateString);
            });

            Assert.IsEmpty(candidateString);
        }

        [Test]
        public void Ensured_Not_Null()
        {
            Ensure.IsNotNull(candidateString);

            Assert.IsEmpty(candidateString);
        }

        [Test]
        public void Ensured_Null_Or_Empty()
        {
            Ensure.IsNullOrEmpty(candidateString);

            Assert.IsEmpty(candidateString);
        }

        [Test]
        public void Ensured_Not_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNotNullOrEmpty(candidateString);
            });

            Assert.IsEmpty(candidateString);
        }

        [Test]
        public void Ensured_Null_Or_WhiteSpace()
        {
            Ensure.IsNullOrWhiteSpace(candidateString);

            Assert.IsEmpty(candidateString);
        }

        [Test]
        public void Ensured_Not_Null_Or_WhiteSpace()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNotNullOrWhiteSpace(candidateString);
            });

            Assert.IsEmpty(candidateString);
        }
    }
}
