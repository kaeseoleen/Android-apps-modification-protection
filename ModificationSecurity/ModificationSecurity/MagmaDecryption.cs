namespace ModificationSecurity
{
    class MagmaDecryption : Converter
    {
        byte[] DecryptedFile;
        uint[] uintKey;
        ulong[] ulongFile;

        private MagmaDecryption() { }

        public MagmaDecryption(byte[] file, byte[] key)
        {
            uintKey = GetUIntKeyArray(key);
            ulongFile = GetULongDataArray(file);

            DecryptedFile = ConvertToByte(DecryptFile());
        }

        public byte[] GetDecryptFile
        {
            get { return DecryptedFile; }
        }

        private ulong[] DecryptFile()
        {
            BasicStep[] K = new BasicStep[8];
            ulong[] ulongDecrFile = new ulong[ulongFile.Length];

            for (int k = 0; k < ulongFile.Length; k++)
            {
                ulongDecrFile[k] = ulongFile[k];

                for (int i = 0; i < K.Length; i++)
                {
                    K[i] = new BasicStep(ulongDecrFile[k], uintKey[i]);
                    ulongDecrFile[k] = K[i].BasicEncrypt(false);
                }

                for (int j = 0; j < 3; j++)
                {
                    for (int i = K.Length - 1; i >= 0; i--)
                    {
                        K[i] = new BasicStep(ulongDecrFile[k], uintKey[i]);

                        if ((j == 2) && (i == 0))
                            ulongDecrFile[k] = K[i].BasicEncrypt(true);
                        else
                            ulongDecrFile[k] = K[i].BasicEncrypt(false);
                    }
                }
            }

            return ulongDecrFile;
        }
    }
}
