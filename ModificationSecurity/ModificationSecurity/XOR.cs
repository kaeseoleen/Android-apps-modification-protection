using System;
using System.Text;
namespace ModificationSecurity
{
    class XOR
    {
        static byte[] X_key;
        //Генерация ключа, шифрование
        public string X_Encrypt(string value)
        {
            X_key = Guid.NewGuid().ToByteArray();            
            return Convert.ToBase64String(Encode(Encoding.Default.GetBytes(value), X_key));
        }
        //Дешифрование
        public  string X_Decrypt(string value)
        {
            return Encoding.Default.GetString(Encode(Convert.FromBase64String(value), X_key));
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