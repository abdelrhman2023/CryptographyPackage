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
        public MD5()
        {
            a = "0x01234567"; b = "0x89ABCDEF"; c = "0xFEDCBA98"; d = "0x76543210";
        }
        public string GetHash(string text)
        {

            return text;
        }
        private void processText(ref string text)
        {
            int originalLength = text.Length;

        }
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
        private void F(string b,string c, string d)
        {
            string notB, notBAndD, bAndC, or;
            notB = HelperFunctions.NOT(b);
            notBAndD = HelperFunctions.AND(notB, d);
            bAndC = HelperFunctions.AND(b, c);
            or = HelperFunctions.OR(bAndC,notBAndD);
        }
        private void G(string b, string c, string d)
        {
            string bAndD, notD, cAndNotD, or;
            bAndD = HelperFunctions.AND(b, d);
            notD = HelperFunctions.NOT(d);
            cAndNotD = HelperFunctions.AND(c,notD);
            or = HelperFunctions.OR(bAndD,cAndNotD);
        }
        private void H(string b, string c, string d)
        {
            string bXorC, xor;
            bXorC = HelperFunctions.XOR(b, ref c);
            xor = HelperFunctions.XOR(bXorC,ref d);
        }
        private void I(string b, string c, string d)
        {
            string notD, bOrNotD, xor;
            notD = HelperFunctions.NOT(d);
            bOrNotD = HelperFunctions.OR(b, notD);
            xor = HelperFunctions.XOR(c, ref bOrNotD);
        }

    }
}
