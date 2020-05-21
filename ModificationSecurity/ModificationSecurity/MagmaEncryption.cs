namespace ModificationSecurity
{
    class MagmaEncryption : Converter
    {
        byte[] EncryptedFile;
        uint[] uintKey;
        ulong[] ulongFile;

        private MagmaEncryption() { }

        public MagmaEncryption(byte[] file, byte[] key)
        {
            uintKey = GetUIntKeyArray(key);
            ulongFile = GetULongDataArray(file);

            EncryptedFile = ConvertToByte(EncryptFile());
        }

        public byte[] GetEncryptFile
        {
            get { return EncryptedFile; }
        }

        private ulong[] EncryptFile()
        {
            BasicStep[] K = new BasicStep[8];
            ulong[] ulongEncrFile = new ulong[ulongFile.Length];

            for (int k = 0; k < ulongFile.Length; k++)
            {
                ulongEncrFile[k] = ulongFile[k];

                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < K.Length; i++)
                    {
                        K[i] = new BasicStep(ulongEncrFile[k], uintKey[i]);
                        ulongEncrFile[k] = K[i].BasicEncrypt(false);
                    }
                }

                for (int i = K.Length - 1; i >= 0; i--)
                {
                    K[i] = new BasicStep(ulongEncrFile[k], uintKey[i]);

                    if (i != 0)
                        ulongEncrFile[k] = K[i].BasicEncrypt(false);
                    else
                        ulongEncrFile[k] = K[i].BasicEncrypt(true);
                }
            }

            return ulongEncrFile;
        }
    }
}
