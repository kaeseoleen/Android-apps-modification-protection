using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;

namespace ModificationSecurity
{
    class MainActivity
    {
        XOR xor = new XOR();
        //Строка для хранения защищаемого значения
        private static string N_Score;
        public string Get_NScore()
        {
            return N_Score;
        }
        public void Update_NScore(string value)
        {
            N_Score = value;
        }        
        public void Preferences_Activity(string state)
        {
            //Генерация ключа
            using (SHA256 mySHA256 = SHA256.Create())
            {
                M_key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(DeviceModel));
            }
            //Работа с локальным хранилищем
            switch (state)
            {
                case "open_app":                    
                    if (Preferences.ContainsKey("score"))
                    {
                        //Извлечение данных
                        string ClosedText = Preferences.Get("score", string.Empty);
                        //Магма-дешифрование
                        Update_NScore(Convert.ToBase64String(M_Decrypt(Convert.FromBase64String(ClosedText))));
                    }     
                    else
                    {
                        Update_NScore("0");
                    }
                    //XOR-шифрование
                    Update_NScore(xor.X_Encrypt(Get_NScore()));                                      
                    break;

                case "close_app":
                    //XOR-дешифрование
                    Update_NScore(xor.X_Decrypt(Get_NScore()));
                    //Магма-шифрование
                    Update_NScore(Convert.ToBase64String(M_Encrypt(Get_NScore())));
                    //Запись данных
                    Preferences.Set("score", Get_NScore());
                    break;
                default: break;
            }
        }

        //Ключ ГОСТ
        string DeviceModel = DeviceInfo.Model;
        byte[] M_key;
        //byte[] M_key = Encoding.ASCII.GetBytes("7c9e6679742540de944be07fc1f90ae7");
        private byte[] M_EncryptionFile, M_DecryptionFile;
        //Магма шифрование
        public byte[] M_Encrypt(string inputvalue)
        {
            if (M_key != null)
            {
                Converter converter = new Converter();
                inputvalue = converter.N_toText(inputvalue);
                byte[] inputfile = Convert.FromBase64String(inputvalue);
                M_Encryption ME = new M_Encryption(inputfile, M_key);
                M_EncryptionFile = ME.GetEncryptFile;
            }
            return M_EncryptionFile;
        }
        //Магма дешифрование
        public byte[] M_Decrypt(byte[] inputvalue)
        {
            if (M_key != null)
            {
                M_Decryption MD = new M_Decryption(inputvalue, M_key);
                M_DecryptionFile = MD.GetDecryptFile;
            }
            return M_DecryptionFile;
        }
    }
}