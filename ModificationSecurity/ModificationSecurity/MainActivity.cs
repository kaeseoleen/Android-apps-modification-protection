using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;

namespace ModificationSecurity
{
    class MainActivity
    {
        Converter converter = new Converter();
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
            M_key = Get_Key();
            //Работа с локальным хранилищем
            switch (state)
            {
                case "open_app":  
                    if (Preferences.ContainsKey("score"))
                    {
                        //Извлечение данных
                        string ClosedText = Preferences.Get("score", string.Empty);
                        //Магма-дешифрование
                        Update_NScore(int.Parse(M_Decrypt(ClosedText)).ToString());
                    }     
                    else
                    {
                        Update_NScore("0");
                    }
                    //XOR-шифрование                                     
                    Update_NScore(MG_Encrypt(Get_NScore()));
                    break;

                case "close_app":
                    //XOR-дешифрование
                    Update_NScore(MG_Decrypt(Get_NScore()));
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
        static byte[] M_key, Msg;
        byte[] Get_Key()
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                M_key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(DeviceModel));
            }
            return M_key;
        }
        private byte[] M_EncryptionFile, M_DecryptionFile;
        //Магма шифрование 
        public byte[] M_Encrypt(string inputvalue)
        {
            if (M_key != null)
            {
                inputvalue = converter.N_toText(inputvalue);
                byte[] inputfile = Convert.FromBase64String(inputvalue);
                M_Encryption ME = new M_Encryption(inputfile, M_key);
                M_EncryptionFile = ME.GetEncryptFile;
            }
            return M_EncryptionFile;
        }
        //Магма дешифрование
        public string M_Decrypt(string inputvalue)
        {
            if (M_key != null)
            {
                byte[] inputfile = Convert.FromBase64String(inputvalue);
                M_Decryption MD = new M_Decryption(inputfile, M_key);
                M_DecryptionFile = MD.GetDecryptFile;
            }
            return Convert.ToBase64String(M_DecryptionFile);
        }        
        public string MG_Encrypt(string inputvalue)
        {
            byte[] key = Get_Key();
            if (key != null)
            {
                Random random = new Random();
                Msg = new byte[8];
                random.NextBytes(Msg);             
                byte[] inputfile = Encoding.Default.GetBytes(inputvalue);
                M_Gamma MG = new M_Gamma(inputfile, key, Msg);
                M_EncryptionFile = MG.StartGamma();
            }
            return Convert.ToBase64String(M_EncryptionFile);                      
        }
        public string MG_Decrypt(string inputvalue)
        {
            byte[] key = Get_Key();
            if (key != null)
            {
                byte[] inputfile = Convert.FromBase64String(inputvalue);
                M_Gamma MG = new M_Gamma(inputfile, key, Msg);
                M_DecryptionFile = MG.StartGamma();
            }
            return Encoding.Default.GetString(M_DecryptionFile);
        }
    }
}