using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    public class MD5
    {
        private string a,b,c,d;
        uint [] tConstants;
        public MD5()
        {
            a = "01234567"; b = "89abcdef"; c = "fedcba98"; d = "76543210";
            tConstants = new uint[64];
            calculateTConstants();
        }
        public string GetHash(string text)
        {

            return text;
        }
        /*private string processText(ref string text)
        {
            
        }*/
        private void appendLeastSignificant64Bits(ref StringBuilder text, int originalTextLength)
        {
            string binaryLength = Convert.ToString(originalTextLength, 2);
            int remainingLength = 64 - binaryLength.Length;
            for (int index = 0;index < remainingLength; index++)
            {
                binaryLength.Insert(0, "0");
            }
            text.Append(binaryLength);
        }
        private void padText(ref StringBuilder text)
        {
            if (text.Length < 448)
            {
                text.Append("1");
                int remainingLength = text.Length - 448;
                for(int index = 0; index < remainingLength; index++)
                {
                   text.Append("0");
                }
            }
        }
        private void calculateTConstants()
        {
            uint _2Power32 = (uint)Math.Pow(2, 32);
            for (int iteration =0;iteration < 64; iteration++)
            {
                tConstants[iteration] = (uint)Math.Floor(Math.Abs(Math.Sin(iteration + 1) * _2Power32));
            }
        }
    }
}
