using System;
using System.Collections.Generic;
using System.Text;

namespace ModificationSecurity
{
    class XOR
    {
        public static byte[] Key = Guid.NewGuid().ToByteArray();

        public  string Encode(string value)
        {
            return Convert.ToBase64String(Encode(Encoding.UTF8.GetBytes(value), Key));
        }

        public  string Decode(string value)
        {
            return Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), Key));
        }

        private static byte[] Encode(byte[] bytes, byte[] key)
        {
            var j = 0;

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= key[j];

                if (++j == key.Length)
                {
                    j = 0;
                }
            }

            return bytes;
        }
    }
}
