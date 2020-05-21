using System;
using System.Text;
using Xamarin.Essentials;

namespace ModificationSecurity
{
    class MainActivity
    {
        XOR xor = new XOR();
        //Строка для хранения защищаемого значения
        private static string trueScore;
        public string Get_trueScore()
        {
            return trueScore;
        }
        public void Update_trueScore(string value)
        {
            trueScore = value;
        }
        //Работа с локальным хранилищем
        public void Data_Activity(string state)
        {
            switch (state)
            {
                case "open_app":                    
                    if (Preferences.ContainsKey("score"))
                    {
                        //Извлечение данных
                        string ClosedText = Preferences.Get("score", string.Empty);
                        //Магма-дешифрование
                        trueScore = Convert.ToBase64String(Magma_Decrypt(Convert.FromBase64String(ClosedText)));
                    }     
                    else
                    {
                        trueScore = "0";
                    }
                    //XOR-шифрование
                    trueScore = xor.EncryptXOR(trueScore);
                    break;

                case "close_app":
                    //XOR-дешифрование
                    trueScore = xor.DecryptXOR(trueScore);
                    //Магма-шифрование
                    trueScore = Convert.ToBase64String(Magma_Encrypt(trueScore));
                    //Запись данных
                    Preferences.Set("score", trueScore);
                    break;
                default: break;
            }
        }

        //Ключ ГОСТ
        byte[] Magma_key = Encoding.ASCII.GetBytes("7c9e6679742540de944be07fc1f90ae7");
        private byte[] MagmaEncryptedFile, MagmaDecryptedFile;
        //Магма шифрование
        public byte[] Magma_Encrypt(string inputvalue)
        {
            if (Magma_key != null)
            {
                Converter converter = new Converter();
                inputvalue = converter.ConvertScoreToText(inputvalue);
                byte[] inputfile = Convert.FromBase64String(inputvalue);
                MagmaEncryption ED = new MagmaEncryption(inputfile, Magma_key);
                MagmaEncryptedFile = ED.GetEncryptFile;
            }
            return MagmaEncryptedFile;
        }
        //Магма дешифрование
        public byte[] Magma_Decrypt(byte[] inputvalue)
        {
            if (Magma_key != null)
            {
                MagmaDecryption MD = new MagmaDecryption(inputvalue, Magma_key);
                MagmaDecryptedFile = MD.GetDecryptFile;
            }
            return MagmaDecryptedFile;
        }
    }
}