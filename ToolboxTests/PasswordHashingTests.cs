using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toolbox.Cryptography;
using System.Diagnostics;

namespace ToolboxTests
{
    [TestClass]
    public class PasswordHashingTests
    {
        [TestMethod]
        public void PositiveCompareTest()
        {
            string password = "test";

            PasswordHashing provider = new PasswordHashing();

            string hash = provider.Compute(password);
            bool result = provider.Compare(password, hash);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PositivePBKDF1CompareTest()
        {
            string password = "test";

            PasswordHashing provider = new PasswordHashing(algorithm: PasswordHashing.ALGORITHM.PBKDF1);

            string hash = provider.Compute(password);
            bool result = provider.Compare(password, hash);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NegativeCompareTest()
        {
            string password = "test";
            string wrongPassword = "nest";

            PasswordHashing provider = new PasswordHashing();

            string hash = provider.Compute(password);
            bool result = provider.Compare(wrongPassword, hash);

            Assert.IsFalse(result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void EmptyPasswordTest()
        {
            string password = "";

            PasswordHashing provider = new PasswordHashing();
            
            string hash = provider.Compute(password);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullPasswordTest()
        {
            string password = null;

            PasswordHashing provider = new PasswordHashing();

            string hash = provider.Compute(password);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhitespacePasswordTest()
        {
            string password = " ";

            PasswordHashing provider = new PasswordHashing();

            string hash = provider.Compute(password);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TooLowComplexityPasswordTest()
        {
            string password = "test";

            PasswordHashing provider = new PasswordHashing(complexity: 7);

            string hash = provider.Compute(password);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TooHighComplexityPasswordTest()
        {
            string password = "test";

            PasswordHashing provider = new PasswordHashing(complexity: 25);

            string hash = provider.Compute(password);
        }

        [TestMethod]
        public void MinimumComplexityPasswordTest()
        {
            int expected = 256;
            string password = "test";

            PasswordHashing provider = new PasswordHashing(complexity: 8);

            string hash = provider.Compute(password);
            HashedPassword hashed = (HashedPassword)hash;

            Assert.AreEqual(expected, hashed.Iterations);
        }

        [TestMethod]
        public void MaximumComplexityPasswordTest()
        {
            int expected = 16777216;
            string password = "test";

            PasswordHashing provider = new PasswordHashing(complexity: 24);

            string hash = provider.Compute(password);
            HashedPassword hashed = (HashedPassword)hash;

            Assert.AreEqual(expected, hashed.Iterations);
        }


        [TestMethod]
        public void ComplexityTimeIncreasePasswordTest()
        {
            string password = "test";

            PasswordHashing provider;
            string hash;
            Stopwatch sw;

            long first, second, third;

            provider = new PasswordHashing(complexity: 10);

            sw = Stopwatch.StartNew();
            hash = provider.Compute(password);
            sw.Stop();

            first = sw.ElapsedTicks;
            
            provider = new PasswordHashing(complexity: 11);

            sw = Stopwatch.StartNew();
            hash = provider.Compute(password);
            sw.Stop();

            second = sw.ElapsedTicks;

            provider = new PasswordHashing(complexity: 12);

            sw = Stopwatch.StartNew();
            hash = provider.Compute(password);
            sw.Stop();

            third = sw.ElapsedTicks;

            Assert.IsTrue(first * 1.5 < second);
            Assert.IsTrue(second * 1.5 < third);
        }
    }
}
