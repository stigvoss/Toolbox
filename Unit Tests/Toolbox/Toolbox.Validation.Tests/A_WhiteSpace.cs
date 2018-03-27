using System;
using NUnit.Framework;

namespace Toolbox.Validation.Tests
{
    [TestFixture]
    public class A_WhiteSpace
    {
        string candidateString;

        [SetUp]
        public void TestInitalize()
        {
            candidateString = " ";
        }

        [Test]
        public void Ensured_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Ensure.IsNull(candidateString);
            });

            Assert.IsNotEmpty(candidateString);
            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Not_Null()
        {
            Ensure.IsNotNull(candidateString);

            Assert.IsNotEmpty(candidateString);
            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNullOrEmpty(candidateString);
            });

            Assert.IsNotEmpty(candidateString);
            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Not_Null_Or_Empty()
        {
            Ensure.IsNotNullOrEmpty(candidateString);

            Assert.IsNotEmpty(candidateString);
            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Null_Or_WhiteSpace()
        {
            Ensure.IsNullOrWhiteSpace(candidateString);

            Assert.IsNotEmpty(candidateString);
            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Not_Null_Or_WhiteSpace()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNotNullOrWhiteSpace(candidateString);
            });

            Assert.IsNotEmpty(candidateString);
            Assert.IsNotNull(candidateString);
        }
    }
}
