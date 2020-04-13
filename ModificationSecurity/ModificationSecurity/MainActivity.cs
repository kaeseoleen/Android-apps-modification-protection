using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace ModificationSecurity
{
    class MainActivity
    {
        XOR xor = new XOR();

        [Table("ScoreTable")]
        public class ScoreTable
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public byte[] dbValue { get; set; }
        }

        public void SQLite_Activity(string state)
        {
            string databaseFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "mobSec.db3");
            SQLiteConnection Database = new SQLiteConnection(databaseFileName);
            Database.CreateTable<ScoreTable>();            
            switch (state)
            {
                case "open_app":                    
                    try
                    {
                        //read                        
                        byte[] temp = Database.Get<ScoreTable>(1).dbValue;
                        //gost decrypt
                        trueScore = Encoding.Default.GetString(temp);
                    }
                    catch (InvalidOperationException e)
                    {
                        trueScore = "0";
                    }                   
                    //XOR encode
                    trueScore = xor.Encode(trueScore);
                    break;

                case "close_app":
                    //XOR decode
                    trueScore = xor.Decode(trueScore);
                    //gost encrypt
                    trueScore = Encoding.Default.GetString(Gost_Encrypt(trueScore));
                    //delete old
                    Database.Delete<ScoreTable>(1);
                    //insert new
                    ScoreTable table = new ScoreTable();
                    table.dbValue = Encoding.Default.GetBytes(trueScore);
                    Database.Insert(table);
                    break;
            }
        }

        private static string trueScore;
        public string Get_trueScore()
        {
            return trueScore;
        }
        public void Update_trueScore(string value)
        {
            trueScore = value;
        }



        //GOST
        byte[] byteKey = Encoding.Default.GetBytes("TextTextTextTextTextTextTextText");
        private byte[] encrByteFile, decrByteFile;
        public byte[] Gost_Encrypt(string value)
        {
            if (byteKey != null)
            {
                Converter converter = new Converter();
                value = converter.ConvertScoreToText(value);
                byte[] btFile = Encoding.Default.GetBytes(value);
                E32 e32 = new E32(btFile, byteKey);
                encrByteFile = e32.GetEncryptFile;
            }
            return encrByteFile;
        }
        public byte[] Gost_Decrypt(byte[] value)
        {
            if (byteKey != null)
            {
                D32 d32 = new D32(value, byteKey);
                decrByteFile = d32.GetDecryptFile;
            }
            return decrByteFile;
        }
    }
}
