using System;
using NUnit.Framework;

namespace Toolbox.Validation.Tests
{
    [TestFixture]
    public class A_String
    {
        string candidateString;

        [SetUp]
        public void TestInitalize()
        {
            candidateString = "Candidate";
        }

        [Test]
        public void Ensured_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Ensure.IsNull(candidateString);
            });

            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Not_Null()
        {
            Ensure.IsNotNull(candidateString);

            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNullOrEmpty(candidateString);
            });

            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Not_Null_Or_Empty()
        {
            Ensure.IsNotNullOrEmpty(candidateString);

            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Null_Or_WhiteSpace()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Ensure.IsNullOrWhiteSpace(candidateString);
            });

            Assert.IsNotNull(candidateString);
        }

        [Test]
        public void Ensured_Not_Null_Or_WhiteSpace()
        {
            Ensure.IsNotNullOrWhiteSpace(candidateString);

            Assert.IsNotNull(candidateString);
        }
    }
}
