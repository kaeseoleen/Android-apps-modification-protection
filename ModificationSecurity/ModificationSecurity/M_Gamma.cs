using System;
namespace ModificationSecurity
{
    class M_Gamma : Converter
    {
        private byte[] SWork;
        byte[] inputfile;

        internal M_Gamma(byte[] fl, byte[] key, byte[] S)
        {
            M_Encryption ME = new M_Encryption(S, key);
            SWork = ME.GetEncryptFile;
            inputfile = new byte[fl.Length];

            for (int i = 0; i < fl.Length; i++)
                inputfile[i] = fl[i];
        }

        internal byte[] StartGamma()
        {
            byte[] resultfile = new byte[inputfile.Length];
            uint[] tempS = new uint[2];
            uint S0, S1;
            int C1, C2;

            if (inputfile.Length % 8 == 0)
                C1 = inputfile.Length / 8;
            else
                C1 = inputfile.Length / 8 + 1;

            C2 = 8;

            for (int i = 0; i < C1; i++)
            {
                if (i == (C1 - 1))
                    C2 = inputfile.Length % 8;

                for (int j = 0; j < C2; j++)
                {
                    tempS = G_GetUIntKeyArray(SWork);
                    S0 = tempS[0];
                    S1 = tempS[1];

                    S0 = (uint)((S0 + 0x1010101) % (Convert.ToUInt64(Math.Pow(2, 32))));
                    S1 = (uint)(((S1 + 0x1010104 - 1) % (Convert.ToUInt64(Math.Pow(2, 32) - 1))) + 1);

                    tempS[0] = S0;
                    tempS[1] = S1;

                    SWork = ConvertToByte(tempS);

                    resultfile[j + i * 8] = (byte)(inputfile[j + i * 8] ^ SWork[j]);
                }
            }
            return resultfile;
        }
    }
}
