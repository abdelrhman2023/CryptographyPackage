using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    public class MD5
    {
        private string a = "01234567", b="89abcdef", c="fedcba98", d="76543210";
        public string GetHash(string text)
        {
            return text;
        }
        private string processText(ref string text)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            StringBuilder processedText = new StringBuilder();
            string binaryString;
            for (int index = 0; index < inputBytes.Length; index++) {
                binaryString = Convert.ToString(inputBytes[index], 2);
                while (binaryString.Length != 8) {
                    binaryString.Insert(0, "0");
                }
                processedText.Append(binaryString);
            }
            int paddingLength = processedText.Length %512;
            for (int index = 0; index < paddingLength; index++) {
                if (index != 0)
                {
                    processedText.Append("0");
                }
                else
                {
                    processedText.Append("1");
                }
            }
            return processedText.ToString();
        }
    }
}
