using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        char[,] matrix;
        
        public string Analyse(string plainText, string cipherText)
        {
            constructMatrix(ref matrix);
            string keyStream = null;
            repeatingKeyVigenereDecryptin(plainText, cipherText.ToLower(), ref keyStream);
            string key = postProcessKeyStream(ref keyStream);
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            constructMatrix(ref matrix);
            string keyStream = getKeyStream(ref key, ref cipherText);
            string plainText = null;
            repeatingKeyVigenereDecryptin(keyStream, cipherText.ToLower(), ref plainText);
            return plainText;
        }
        public string Encrypt(string plainText, string key)
        {
            constructMatrix(ref matrix);
            string keyStream = getKeyStream(ref key,ref plainText);
            string cipherText = null;
            repeatingKeyVigenereEncryption(keyStream,plainText,ref cipherText);
            return cipherText.ToUpper();
        }
        private void constructMatrix(ref char [,] matrix)
        {
            matrix = new char[26,26];
            string alphabet = "";
            int index,innerIndex;
            char firstLetter;
            for (index = 0; index < 26; index++)
            {
                alphabet += (char)(index + 97);
            }
            for (index = 0; index < 26; index++)
            {
                for (innerIndex = 0;innerIndex < 26; innerIndex++)
                {
                    matrix[index,innerIndex] = alphabet[innerIndex];
                }
                firstLetter = alphabet[0];
                alphabet = alphabet.Remove(0, 1);
                alphabet = alphabet.Insert(alphabet.Length, firstLetter.ToString());
            }
            
        }
        private string getKeyStream(ref string key,ref string text)
        {
            string keyStream = key;
            int division, remainder;
            division = text.Length / key.Length;
            remainder = text.Length % key.Length;
            for (int repetition = 1; repetition < division; repetition++)
            {
                keyStream += key;
            }
            for (int r = 0;r < remainder; r++)
            {
                keyStream += key[r];
            }
            return keyStream;
        }
        private void repeatingKeyVigenereEncryption(string keyStream, string text, ref string output)
        {
            int size, firstLetter, secondLetter,index;
            output = "";
            size = text.Length;
            for (index = 0; index < size; index++)
            {
                firstLetter = (int)(text[index] - 'a');
                secondLetter = (int)(keyStream[index] - 'a');
                output += matrix[firstLetter, secondLetter];
            }
        }
        private void repeatingKeyVigenereDecryptin(string keyStream, string text, ref string output)
        {
            int size, firstLetter, index,innerIndex;
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
                        break;
                    }
                }
            }
        }
        
        private string postProcessKeyStream(ref string keyStream)
        {
            string key = "";
            int index;
            var foundInexes = new List<int>();
            bool isMatched;
            for (index = keyStream.IndexOf(keyStream[0]); index > -1; index = keyStream.IndexOf(keyStream[0], index + 1))
            {
                foundInexes.Add(index);
            }
            for (index=1;index<foundInexes.Count;index++)
            {
                isMatched = true;
                for(int firstLetter = 0; firstLetter < foundInexes[index]; firstLetter++)
                {
                    if (keyStream[firstLetter] != keyStream[foundInexes[index] + firstLetter])
                    {
                        isMatched = false;
                    }
                }
                if (isMatched)
                {
                    for(int firstLetter = 0; firstLetter < foundInexes[index]; firstLetter++)
                    {
                        key+= keyStream[firstLetter];
                    }
                    break;
                }
            }
            return key;
        }
    }
}