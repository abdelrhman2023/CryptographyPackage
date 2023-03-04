using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        private char[,] matrix;
        private List<KeyValuePair<byte, byte>> matrixInverse;
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            string cipherText = "";
            constructMatrix(ref key, ref matrix, ref matrixInverse);
            return cipherText.ToUpper();
        }
        private void constructMatrix(ref string key, ref char[,] matrix, ref List<KeyValuePair<byte, byte>> matrixInverse)
        {
            byte row = 0, col = 0;
            int index;
            bool[] alphabet = new bool[26];
            matrix = new char[5, 5];
            matrixInverse = new List<KeyValuePair<byte, byte>>(25);
            for (int size = 0; size < key.Length; size++)
            {
                index = (int)key[size] - 97;
                if (alphabet[index] == false)
                {
                    alphabet[index] = true;
                    matrix[row, col] = key[size];
                    matrixInverse[index] = new KeyValuePair<byte, byte>(row, col);
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
                    matrixInverse[index] = new KeyValuePair<byte, byte>(row, col);
                    alphabet[index] = true;

                    if (index == 8)
                    {
                        alphabet[9] = true;
                    }
                    col++;
                }
                reset(ref row, ref col);

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
        private string getIntersection(ref KeyValuePair<byte, byte> firstLetter, ref KeyValuePair<byte, byte> secondLetter)
        {
            string intersection = "";
            intersection += matrix[firstLetter.Key, secondLetter.Value];
            intersection += matrix[secondLetter.Key, firstLetter.Value];
            return intersection;
        }
        private char verticalCircularArray(byte x, byte y)
        {
            char value = matrix[((x % 4) + 4) % 4, y];
            return value;
        }
        private char horizontalCircularArray(byte x, byte y)
        {
            char value = matrix[x, ((y%4)+4)%4];
            return value;
        }
        private string getValues(ref KeyValuePair<byte, byte> firstLetter, ref KeyValuePair<byte, byte> secondLetter, Func<byte, byte, char> circularArray)
        {
            string value = "";
            value += circularArray(firstLetter.Key, firstLetter.Value);
            value += circularArray(secondLetter.Key, secondLetter.Value);
            return value;
        }

    }
}
