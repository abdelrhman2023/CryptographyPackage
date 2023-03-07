using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            string C_T = "";

            plainText = plainText.ToUpper();

            for (int i = 0; i < plainText.Length; i++)
            {
                if (char.IsLetter(plainText[i]))
                {
                    int P_T_index = ((plainText[i] - 65) + key);
                    P_T_index = P_T_index % 26;
                    char c = Convert.ToChar(P_T_index + 65);
                    C_T += c;
                }
                else
                {
                    C_T += plainText[i];
                }

            }

            return C_T;
        }

        public string Decrypt(string cipherText, int key)
        {
            string P_T = "";
            cipherText = cipherText.ToUpper();

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (char.IsLetter(cipherText[i]))
                {
                    int P_T_index = ((cipherText[i] + 65) - key);
                    P_T_index = P_T_index % 26;
                    char c = Convert.ToChar(P_T_index + 65);
                    P_T += c;
                }
                else
                {
                    P_T += cipherText[i];
                }
            }
            
            return P_T;
        }

        public int Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            
            int num = cipherText[0] - plainText[0];
            
            if (num < 0)
            {
                while (true)
                {
                    num+=26;
                    if(num >= 0) { break; }
                }
                return num;
            }

            return num;
        }
    }
}
