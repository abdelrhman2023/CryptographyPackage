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
            Console.WriteLine(lost);
            return key.ToLower();
        }

        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
