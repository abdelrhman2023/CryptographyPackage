using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        private char[,] matrix;
        public AutokeyVigenere()
        {
            constructMatrix(ref matrix);
        }

        public string Analyse(string plainText, string cipherText)
        {
            string keyStream = null;
            autoKeyVigenereDecryptin(plainText, cipherText.ToLower(), ref keyStream);
            string key = postProcessKeyStream(ref keyStream,ref plainText);
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string plainText = null;
            autoKeyVigenereDecryptin(key, cipherText.ToLower(), ref plainText);
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            string keyStream = getKeyStream(ref key, ref plainText);
            string cipherText = null;
            autoKeyVigenereEncryption(keyStream, plainText, ref cipherText);
            return cipherText.ToUpper();
        }
        private void constructMatrix(ref char[,] matrix)
        {
            matrix = new char[26, 26];
            string alphabet = "";
            int index, innerIndex;
            char firstLetter;
            for (index = 0; index < 26; index++)
            {
                alphabet += (char)(index + 97);
            }
            for (index = 0; index < 26; index++)
            {
                for (innerIndex = 0; innerIndex < 26; innerIndex++)
                {
                    matrix[index, innerIndex] = alphabet[innerIndex];
                }
                firstLetter = alphabet[0];
                alphabet = alphabet.Remove(0, 1);
                alphabet = alphabet.Insert(alphabet.Length, firstLetter.ToString());
            }

        }
        private string getKeyStream(ref string key, ref string text)
        {
            string keyStream = key;
            int division, remainder;
            division = text.Length / key.Length;
            remainder = text.Length % key.Length;
            for (int repetition = 1; repetition < division; repetition++)
            {
                keyStream += text;
            }
            for (int r = 0; r < remainder; r++)
            {
                keyStream += text[r];
            }
            return keyStream;
        }
        private void autoKeyVigenereEncryption(string keyStream, string text, ref string output)
        {
            int size, firstLetter, secondLetter, index;
            output = "";
            size = text.Length;
            for (index = 0; index < size; index++)
            {
                firstLetter = (int)(text[index] - 'a');
                secondLetter = (int)(keyStream[index] - 'a');
                output += matrix[firstLetter, secondLetter];
            }

        }
        private void autoKeyVigenereDecryptin(string keyStream, string text, ref string output)
        {
            int size, firstLetter, index, innerIndex;
            output = "";
            size = text.Length;
            for (index = 0; index < size; index++)
            {
                firstLetter = (int)(keyStream[index] - 'a');
                for (innerIndex = 0; innerIndex < 26; innerIndex++)
                {
                    if (matrix[firstLetter, innerIndex] == text[index])
                    {
                        output += (char)(innerIndex + 97);
                        if (keyStream.Length != text.Length)
                        {
                            keyStream += (char)(innerIndex + 97);
                        }
                        break;
                    }
                }
            }
        }
        private string postProcessKeyStream(ref string keyStream, ref string plainText)
        {
            string key = "";
            int index;
            var foundInexes = new List<int>();
            bool isMatched;
            for (index = keyStream.IndexOf(plainText[0]); index > -1; index = keyStream.IndexOf(plainText[0], index + 1))
            {
                foundInexes.Add(index);
            }
            for (index = 0; index < foundInexes.Count; index++)
            {
                isMatched = true;
                for (int firstLetter = 0; firstLetter < foundInexes[index]; firstLetter++)
                {
                    if (plainText[firstLetter] != keyStream[foundInexes[index] + firstLetter])
                    {
                        isMatched = false;
                    }
                }
                if (isMatched)
                {
                    for (int firstLetter = 0; firstLetter < foundInexes[index]; firstLetter++)
                    {
                        key += keyStream[firstLetter];
                    }
                    break;
                }
            }
            return key;
        }
    }
}
