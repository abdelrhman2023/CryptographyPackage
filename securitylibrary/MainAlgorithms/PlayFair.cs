using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }
        private void constructMatrix(ref string key)
        {
            byte row=0, col=0;
            int index;
            bool[] alphabet = new bool[26]; 
            char[,]matrix = new char[5,5];
            for (int size = 0; size < key.Length; size++)
            {
                index = (int)key[size]-97;
                if (alphabet[index] == false)
                {
                    alphabet[index] = true;
                    matrix[row, col] = key[size];
                    if (key[size] == 'i')
                    {
                        alphabet[9] = true;
                    }
                    col++;
                }
                reset(ref row, ref col);
            }
            for (index = 0; index < 26; index++)
            {
                if (alphabet[index] == false)
                {
                    matrix[row, col] = (char)(index + 97);
                    alphabet[index] = true;
                    if (index == 8)
                    {
                        alphabet[9] = true;
                    }
                    col++;
                }
                reset(ref row,ref col);
            }
        }
        private void reset(ref byte row, ref byte col)
        {
            if (col == 5)
            {
                row += 1;
                col = 0;
            }
        }

    }
}
