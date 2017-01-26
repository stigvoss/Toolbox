using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Cryptography
{
    public class PasswordHashing
    {
        private const byte DEFAULT_SALT_SIZE = 16;
        private const byte DEFAULT_COMPLEXITY = 14;

        private readonly byte _saltSize;
        private readonly byte _complexity;
        private readonly ALGORITHM _algorithm;

        public enum ALGORITHM
        {
            DEFAULT = 0,
            PBKDF2 = 0,
            PBKDF1 = 1
        }
        public PasswordHashing(
            byte saltSize = DEFAULT_SALT_SIZE,
            byte complexity = DEFAULT_COMPLEXITY,
            ALGORITHM algorithm = ALGORITHM.DEFAULT)
        {
            _saltSize = saltSize;
            _algorithm = algorithm;
            _complexity = complexity;
        }

        private DeriveBytes GetHashingProvider(string password, byte[] salt, int iterations, ALGORITHM algorithm)
        {
            DeriveBytes provider = null;

            switch (algorithm)
            {
                case ALGORITHM.PBKDF1:
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    provider = new PasswordDeriveBytes(passwordBytes, salt, "SHA1", iterations);
                    break;
                default:
                case ALGORITHM.PBKDF2:
                    provider = new Rfc2898DeriveBytes(password, salt, iterations);
                    break;
            }

            return provider;
        }

        public string Compute(string password)
        {
            byte[] salt = GenerateSalt(_saltSize);
            
            HashedPassword hashedPassword = Compute(password, salt, _complexity, _algorithm);

            return (string)hashedPassword;
        }

        private HashedPassword Compute(string password, byte[] salt, byte complexity, ALGORITHM algorithm)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Plain text password cannot be null, empty or whitespace");

            HashedPassword hashedPassword = new HashedPassword(salt, complexity, algorithm);

            using (DeriveBytes provider = GetHashingProvider(password,
                   hashedPassword.Salt, hashedPassword.Iterations, hashedPassword.Algorithm))
            {
                hashedPassword.Content = provider.GetBytes(hashedPassword.Content.Length);
            }

            return hashedPassword;
        }

        private byte[] GenerateSalt(byte saltSize)
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltSize];
                provider.GetBytes(salt);
                return salt;
            }
        }

        public bool Compare(string password, string hash)
        {
            bool matches = true;

            HashedPassword target = (HashedPassword)hash;
            HashedPassword source = Compute(password, target.Salt, target.Complexity, target.Algorithm);
            
            for(int i = 0; i < target.Content.Length; i++)
            {
                matches = matches && target.Content[i] == source.Content[i];
            }

            return matches;
        }
    }
}
