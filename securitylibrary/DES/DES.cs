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
        private List<string> _16SubKeys;
        public DES()
        {
            _16SubKeys = new List<string>();
        }
        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            string cipherText = null;
            StringBuilder left = new StringBuilder(), right = new StringBuilder();
            generateKeys(key);
            string binaryText =hexaToBinary(plainText);
            binaryText = permutationChoice(plainText,DESConstants.initialPermutation);
            splitString(binaryText, ref left, ref right);
            return cipherText;
        }
        private string XOR (ref string key,ref string text)
        {
           StringBuilder xor = new StringBuilder();
            for (int index = 0;index < text.Length; index++)
            {
                if((text[index] == '1' && key[index] =='0')|| (text[index] == '0' && key[index] == '1'))
                {
                    xor.Append('1');
                }
                else
                {
                    xor.Append('0');
                }
            }
            return xor.ToString();
        }
        private byte binaryToDecimal(string binary)
        {
            byte number = 0;
            for (int index = binary.Length - 1; index >= 0; index--)
            {
                if (binary[index] == '1')
                {
                    number += (byte)Math.Pow(2, index);
                }
            }
            return number;

        }
        private void generateKeys(string key)
        {
            string binaryKey = hexaToBinary(key);
            string _56BitKey = permutationChoice(binaryKey,DESConstants.pc1);
            StringBuilder c = new StringBuilder(), d = new StringBuilder();
            int index; string subKey;
            splitString(_56BitKey, ref c, ref d);
            for (index = 0; index < 16; index++)
            {
                for (byte leftShift = 1; leftShift <= DESConstants.leftShifts[index]; leftShift++)
                {
                    c.Append(c[0]);
                    d.Append(d[0]);
                    c.Remove(0, 1);
                    d.Remove(0, 1);
                }
                subKey = c.ToString() + d.ToString();
                subKey = permutationChoice(subKey, DESConstants.pc2);
                _16SubKeys.Add(subKey);
            }
        }   
        
        private string hexaToBinary(string text)
        {
            StringBuilder binary = new StringBuilder();
            for (int index = 2; index < text.Length; index++)
            {
                binary.Append(DESConstants.hexaToBinaryMap[text[index]]);
            }
            return binary.ToString();
        }
        private string permutationChoice(string input,byte [] pc)
        {
            StringBuilder permutatedChoice = new StringBuilder();
            byte position;
            for (int index = 0;index < pc.Length; index++)
            {
                position = (byte)(pc[index] - 1);
                permutatedChoice.Append(input[position]);

            }
            return permutatedChoice.ToString();
        }
        private void splitString(string str,ref StringBuilder left, ref StringBuilder right)
        {
            int size = str.Length/2;
            for (int index = 0; index < size; index++)
            {
                left.Append(str[index]);
                right.Append(str[index + size]);
            }
        }
        private void manglerFunction(ref string right, ref string left, ref string subKey)
        {
            string expandedText = permutationChoice(right, DESConstants.expansionTable);
            string xor = XOR(ref subKey, ref right);
            string sboxes = null;
            string permutatedRight = permutationChoice(sboxes, DESConstants.permutationTable);
            string leftXORRight = XOR(ref left, ref right);
            left = right;
            right = leftXORRight;
        }
    }
}
