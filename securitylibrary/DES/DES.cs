using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            string cipherText = null;
            generateKeys(key);
            return cipherText;
        }
        /*private string XOR (string key, string text)
        {

        }*/
        private void generateKeys(string key)
        {
            StringBuilder binaryKey = new StringBuilder();
            for (int index = 2; index < key.Length; index++)
            {
                binaryKey.Append(DESConstants.hexaToBinaryMap[key[index]]);
            }
        }   
        private string hexaToBinary(string hexa)
        {
            StringBuilder binary = new StringBuilder();
          
            return binary.ToString();
        }
    }
}
