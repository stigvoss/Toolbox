using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Cryptography.Base;

namespace Toolbox.Cryptography
{
    public class HashedPassword : IPassword<byte[]>
    {
        private const byte GENERATION = 1;

        private const byte DEFAULT_SALT_SIZE = 16;
        private const byte DEFAULT_COMPLEXITY = 14;
        private const byte DEFAULT_OUTPUT_SIZE = 16;

        private const byte COMPLEXITY_MIN_VALUE = 8;
        private const byte COMPLEXITY_MAX_VALUE = 24;

        private const byte HEADER_BYTE_COUNT = 5;

        private const byte HEADER_BYTE_GENERATION = 0;
        private const byte HEADER_BYTE_ALGORITHM = 1;
        private const byte HEADER_BYTE_SALT_SIZE = 2;
        private const byte HEADER_BYTE_COMPLEXITY = 3;
        private const byte HEADER_BYTE_OUTPUT_SIZE = 4;

        private byte[] _hash;
        private byte[] _salt;
        private byte _complexity;
        private PasswordHashing.ALGORITHM _algorithm;

        public HashedPassword(byte hashSize, byte[] salt, byte complexity = DEFAULT_COMPLEXITY,
            PasswordHashing.ALGORITHM algorithm = PasswordHashing.ALGORITHM.DEFAULT) : this(new byte[hashSize], salt, complexity, algorithm) { }

        public HashedPassword(byte[] salt, byte complexity = DEFAULT_COMPLEXITY,
            PasswordHashing.ALGORITHM algorithm = PasswordHashing.ALGORITHM.DEFAULT) : this(new byte[DEFAULT_OUTPUT_SIZE], salt, complexity, algorithm) { }

        public HashedPassword(byte[] hash, byte[] salt, byte complexity = DEFAULT_COMPLEXITY,
            PasswordHashing.ALGORITHM algorithm = PasswordHashing.ALGORITHM.DEFAULT)
        {
            if (complexity > COMPLEXITY_MAX_VALUE)
                throw new ArgumentException("Complexity exceeds maximum allowed value!");

            if (complexity < COMPLEXITY_MIN_VALUE)
                throw new ArgumentException("Complexity exceeds minimum allowed value!");

            _hash = hash;
            _salt = salt;
            _complexity = complexity;
            _algorithm = algorithm;
        }

        public byte[] Content
        {
            get { return _hash; }
            set { _hash = value; }
        }

        public int Iterations
        {
            get
            {
                checked
                {
                    return (int)Math.Pow(2, _complexity);
                }
            }
        }

        public byte Complexity
        {
            get { return _complexity; }
        }

        public byte[] Salt
        {
            get { return _salt; }
        }

        public PasswordHashing.ALGORITHM Algorithm
        {
            get { return _algorithm; }
        }

        public byte Generation
        {
            get { return GENERATION; }
        }

        public static explicit operator byte[] (HashedPassword password)
        {
            byte[] salt = password.Salt;
            byte[] hash = password.Content;

            int productSize = HEADER_BYTE_COUNT + DEFAULT_OUTPUT_SIZE + salt.Length;

            byte[] product = new byte[productSize];

            product[HEADER_BYTE_GENERATION] = GENERATION;
            product[HEADER_BYTE_ALGORITHM] = (byte)password.Algorithm;
            product[HEADER_BYTE_SALT_SIZE] = (byte)salt.Length;
            product[HEADER_BYTE_COMPLEXITY] = password.Complexity;
            product[HEADER_BYTE_OUTPUT_SIZE] = (byte)hash.Length;

            for (int i = 0; i < hash.Length; i++)
            {
                int position = HEADER_BYTE_COUNT + i;

                product[position] = hash[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                int position = HEADER_BYTE_COUNT + DEFAULT_OUTPUT_SIZE + i;

                product[position] = salt[i];
            }

            return product;
        }

        public static explicit operator string(HashedPassword password)
        {
            byte[] hashBytes = (byte[])password;
            string hashString = Convert.ToBase64String(hashBytes);

            return hashString;
        }

        public static explicit operator HashedPassword(byte[] hashBytes)
        {
            byte generation = hashBytes[HEADER_BYTE_GENERATION];
            PasswordHashing.ALGORITHM algorithm = (PasswordHashing.ALGORITHM)hashBytes[HEADER_BYTE_ALGORITHM];
            byte saltSize = hashBytes[HEADER_BYTE_SALT_SIZE];
            byte complexity = hashBytes[HEADER_BYTE_COMPLEXITY];
            byte hashSize = hashBytes[HEADER_BYTE_OUTPUT_SIZE];

            byte[] hash = new byte[hashSize];
            Array.Copy(hashBytes, HEADER_BYTE_COUNT, hash, 0, hashSize);

            byte[] salt = new byte[saltSize];
            Array.Copy(hashBytes, HEADER_BYTE_COUNT + hashSize, salt, 0, saltSize);

            HashedPassword hashedPassword = new HashedPassword(hash, salt, complexity, algorithm);

            return hashedPassword;
        }

        public static explicit operator HashedPassword(string hashString)
        {
            byte[] hashBytes = Convert.FromBase64String(hashString);
            HashedPassword password = (HashedPassword)hashBytes;

            return password;
        }
    }
}
