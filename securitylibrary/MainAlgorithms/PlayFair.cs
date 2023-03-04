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
        private Dictionary<char, KeyValuePair<byte, byte>>matrixInverse;
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            string cipherText = "";
            int index;
            char firstLetter, secondLetter;
            constructMatrix(ref key, ref matrix, ref matrixInverse);
            plainText = plainText.Replace('j', 'i');
            for (index = 0; index < plainText.Length - 1; index+=2)
            {
                firstLetter = plainText[index];
                secondLetter = plainText[index + 1];
                if (plainText[index] == plainText[index + 1])
                {
                    secondLetter = 'x';
                }
                if (matrixInverse[firstLetter].Key== matrixInverse[secondLetter].Key)
                {
                    cipherText += getValues(matrixInverse[firstLetter],matrixInverse[secondLetter], horizontalCircularArray);
                }
                else if (matrixInverse[firstLetter].Value == matrixInverse[secondLetter].Value)
                {
                    cipherText += getValues(matrixInverse[firstLetter], matrixInverse[secondLetter], verticalCircularArray);
                }
                else
                {
                    cipherText += getIntersection(matrixInverse[firstLetter], matrixInverse[secondLetter]);
                }
                
            }
            return cipherText.ToUpper();
        }
        private void constructMatrix(ref string key, ref char[,] matrix, ref Dictionary<char, KeyValuePair<byte, byte>> matrixInverse)
        {
            byte row = 0, col = 0;
            int index;
            bool[] alphabet = new bool[26];
            matrix = new char[5, 5];
            matrixInverse = new Dictionary<char, KeyValuePair<byte, byte>>();
            for (int size = 0; size < key.Length; size++)
            {
                index = (int)key[size] - 97;
                if (alphabet[index] == false)
                {
                    alphabet[index] = true;
                    matrix[row, col] = key[size];
                    matrixInverse.Add(key[size],new KeyValuePair<byte, byte>(row, col));
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
                    matrixInverse.Add((char)(index + 97), new KeyValuePair<byte, byte>(row, col));
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
        private string getIntersection(KeyValuePair<byte, byte> firstLetter,KeyValuePair<byte, byte> secondLetter)
        {
            string intersection = "";
            intersection += matrix[firstLetter.Key, secondLetter.Value];
            intersection += matrix[secondLetter.Key, firstLetter.Value];
            return intersection;
        }
        private char verticalCircularArray(byte x, byte y)
        {
            char value;
            if (x + 1 > 4)
            {
                value = matrix[0, y];
            }
            else
            {
                value = matrix[x + 1, y];
            }
            return value;
        }
        private char horizontalCircularArray(byte x, byte y)
        {
            char value;
            if (y + 1 > 4)
            {
               value = matrix[x, 0];
            }
            else
            {
               value = matrix[x, y+1];
            }
           return value;
        }
        private string getValues(KeyValuePair<byte, byte> firstLetter, KeyValuePair<byte, byte> secondLetter, Func<byte, byte, char> circularArray)
        {
            string value = "";
            value += circularArray(firstLetter.Key, firstLetter.Value);
            value += circularArray(secondLetter.Key, secondLetter.Value);
            return value;
        }

    }
}
