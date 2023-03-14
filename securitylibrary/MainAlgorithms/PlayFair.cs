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
            cipherText = cipherText.ToLower();
            cipherText = cipherText.Replace('j', 'i');
            string plainText = playFair(cipherText, key, horizontalDecryptionCircularArray, verticalDecryptionCircularArray);
            postProcessText(ref plainText);
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            plainText = preProcessText(plainText);
            string cipherText = playFair(plainText, key, horizontalEncryptionCircularArray, verticalEncryptionCircularArray);
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
                    matrixInverse.Add(key[size], new KeyValuePair<byte, byte>(row, col));
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
        private string preProcessText(string text)
        {
            text = text.ToLower();
            text = text.Replace('j', 'i');

            string x = "x";
            for (int index = 0; index < text.Length - 1; index += 2)
            {
                if (text[index] == text[index + 1])
                {
                    text = text.Insert(index + 1, x);
                }
            }
            if (text.Length % 2 != 0)
            {
                text += x;
            }

            return text;
        }
        private string postProcessText(ref string plainText)
        {
            for (int index = 0; index < plainText.Length - 2; index += 2)
            {
                if (plainText[index] == plainText[index + 2] && plainText[index + 1] == 'x')
                {
                    plainText = plainText.Remove(index + 1, 1);
                    index--;
                }
            }
            if (plainText[plainText.Length - 1] == 'x')
            {
                plainText = plainText.Remove(plainText.Length - 1, 1);
            }
            return plainText;
        }
 
        private string getIntersection(KeyValuePair<byte, byte> firstLetter,KeyValuePair<byte, byte> secondLetter)
        {
            string intersection = "";
            intersection += matrix[firstLetter.Key, secondLetter.Value];
            intersection += matrix[secondLetter.Key, firstLetter.Value];
            return intersection;
        }
        private char verticalEncryptionCircularArray(byte x, byte y)
        {
            char value;
            value = matrix[(x+1)%5, y];
            return value;
        }
        private char horizontalEncryptionCircularArray(byte x, byte y)
        {
            char value;
            value = matrix[x, (y+1)%5];
            return value;
        }
        private char verticalDecryptionCircularArray(byte x, byte y)
        {
            char value;
            if (x - 1 < 0)
            {
                value = matrix[4, y];
            }
            else
            {
                value = matrix[x - 1, y];
            }
            return value;
        }
        private char horizontalDecryptionCircularArray(byte x, byte y)
        {
            char value;
            if (y - 1 < 0)
            {
                value = matrix[x, 4];
            }
            else
            {
                value = matrix[x, y - 1];
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
        private string playFair(string text, string key, Func<byte, byte, char> horizontalCircularArray, Func<byte, byte, char> verticalCircularArray)
        {
            constructMatrix(ref key, ref matrix, ref matrixInverse);
            string playFairText = "";
            int index;
            char firstLetter, secondLetter;
            for (index = 0; index < text.Length - 1; index += 2)
            {
                firstLetter = text[index];
                secondLetter = text[index + 1];
                if (matrixInverse[firstLetter].Key == matrixInverse[secondLetter].Key)
                {
                    playFairText += getValues(matrixInverse[firstLetter], matrixInverse[secondLetter], horizontalCircularArray);
                }
                else if (matrixInverse[firstLetter].Value == matrixInverse[secondLetter].Value)
                {
                    playFairText += getValues(matrixInverse[firstLetter], matrixInverse[secondLetter], verticalCircularArray);
                }
                else
                {
                    playFairText += getIntersection(matrixInverse[firstLetter], matrixInverse[secondLetter]);
                }

            }
            return playFairText;
        }
    }
}
