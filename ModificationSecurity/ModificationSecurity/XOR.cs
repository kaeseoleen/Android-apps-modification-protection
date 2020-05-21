using System;
using System.Text;
namespace ModificationSecurity
{
    class XOR
    {
        static byte[] XOR_key;
        //Генерация ключа, шифрование
        public string EncryptXOR(string value)
        {
            XOR_key = Guid.NewGuid().ToByteArray();            
            return Convert.ToBase64String(Encode(Encoding.Default.GetBytes(value), XOR_key));
        }
        //Дешифрование
        public  string DecryptXOR(string value)
        {
            return Encoding.Default.GetString(Encode(Convert.FromBase64String(value), XOR_key));
        }
        private static byte[] Encode(byte[] inputbytes, byte[] xor_Key)
        {
            var j = 0;
            for (var i = 0; i < inputbytes.Length; i++)
            {
                inputbytes[i] ^= xor_Key[j];

                if (++j == xor_Key.Length)
                {
                    j = 0;
                }
            }
            return inputbytes;
        }
    }
}