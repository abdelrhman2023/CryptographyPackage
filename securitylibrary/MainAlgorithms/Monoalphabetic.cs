using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            Dictionary<char, char> myDictionary = new Dictionary<char, char>();

            cipherText = cipherText.ToUpper();
            plainText = plainText.ToUpper();

            for (int i = 0; i < plainText.Length; i++)
            {
                myDictionary[plainText[i]] = cipherText[i];
            }

            string lost = "";
            
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (!cipherText.Contains(i))
                {
                    lost += i;
                }
            }


            int idx = 0;
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (!myDictionary.ContainsKey(i))
                {
                    myDictionary[i] = lost[idx++];
                }
            }

            string key = "";

            for (char i = 'A'; i <= 'Z'; i++)
            {
                key += myDictionary[i];
            }

            Console.WriteLine(key);
            return key.ToLower();
        }

        public string Decrypt(string cipherText, string key)
        {
            Dictionary<char, char> myDictionary = new Dictionary<char, char>();

            cipherText = cipherText.ToUpper();
            key = key.ToUpper();

            for (int i = 0; i < 26; i++)
            {
                char k = key[i];
                char c = Convert.ToChar(i + 65);
                myDictionary[k] = c;
            }

            string plaintext = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                plaintext += myDictionary[cipherText[i]];
            }

            return plaintext;
        }

        public string Encrypt(string plainText, string key)
        {
            Dictionary<char, char> myDictionary = new Dictionary<char, char>();
            
            plainText = plainText.ToUpper();
            key = key.ToUpper();

            for (int i = 0; i < 26; i++)
            {
                char k = key[i];    
                char c = Convert.ToChar(i + 65);
                myDictionary[c]= k;
            }

            string cipherText = "";
            for(int i = 0; i<plainText.Length; i++)
            {
                cipherText += myDictionary[plainText[i]];
            }

            return cipherText;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            cipher = cipher.ToUpper();

            //string alphabet ="ETAOINSRHLDCUMFPGWYBVKXJQZ";
            //double[] letterFrequencies = { 12.51, 9.25, 8.04, 7.60, 7.26, 7.09, 6.54, 6.12, 5.49, 4.14, 3.99, 3.06, 2.71, 2.53, 2.30, 2.00, 1.96, 1.92, 1.73, 1.54, 0.99, 0.67, 0.19, 0.16, 0.11, 0.09 };
            
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int[] ciphertextFreq = new int[26];
            double[] englishFreq = { 8.04, 1.54, 3.06, 3.99, 12.51, 2.30, 1.96, 5.49, 7.26, 0.16, 0.67, 4.14, 2.53, 7.09, 7.60, 2.00, 0.11, 6.12, 6.54, 9.25, 2.71, 0.99, 1.92, 0.19,  1.73,  0.09 };
            double[] ciphertextFreqRelative = new double[26];
            double[] englishFreqRelative = new double[26];


            foreach (char c in cipher)
            {
                if (char.IsLetter(c))
                {
                    ciphertextFreq[c - 'A']++;
                }
            }

            int ciphertextLength = cipher.Count(char.IsLetter);
            for (int i = 0; i < 26; i++)
            {
                ciphertextFreqRelative[i] = (double)ciphertextFreq[i] / ciphertextLength;
            }

            double englishFreqSum = englishFreq.Sum();
            for (int i = 0; i < 26; i++)
            {
                englishFreqRelative[i] = englishFreq[i] / englishFreqSum;
            }

            Dictionary<char, char> key = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                double min_Diff = double.MaxValue;
                int min_Idx = -1;

                // Find the English language letter with the closest frequency to the ciphertext letter
                for (int j = 0; j < 26; j++)
                {
                    double diff = Math.Abs(ciphertextFreqRelative[i] - englishFreqRelative[j]);
                    if (diff < min_Diff && !key.ContainsValue(alphabet[j]))
                    {
                        min_Diff = diff;
                        min_Idx = j;
                    }
                }

                // Assign the ciphertext letter to the English language letter with the closest frequency
                key.Add(alphabet[i], alphabet[min_Idx]);
            }

            string plaintext = "";
            foreach (char c in cipher)
            {
                if (char.IsLetter(c))
                {
                    char decryptedChar = key[c];
                    plaintext += decryptedChar;
                }
                else
                {
                    plaintext += c;
                }
            }

            return plaintext;
        }
    }
}
