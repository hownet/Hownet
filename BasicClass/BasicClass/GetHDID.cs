using System;
using System.Text;
using System.Management;


namespace BasicClass
{
    public class GetHDID
    {
        public static byte[] Rand
        {
            get
            {
                GetHDID hd = new GetHDID();
                byte[] _rand = hd.GetRand();
                return _rand;
            }
        }
        public static bool IsReg = true;
        public string GetNumChar(int strLength)
        {
            char[] numchar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < strLength; i++)
            {
                newRandom.Append(numchar[rd.Next(10)]);
            }
            return newRandom.ToString();
        }
        public byte[] GetRand()
        {
            byte[] tem = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                tem[i] = byte.Parse(GetNumChar(2));
            }
            return tem;
        }
    }
}
