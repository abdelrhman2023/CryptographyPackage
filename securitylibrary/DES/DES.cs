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
            string left = "", right = "";
            string preCipher = "";
            generateKeys(key);
            string binaryText =hexaToBinary(plainText);
            binaryText = permutationChoice(binaryText,DESConstants.initialPermutation);
            splitString(binaryText, ref left, ref right);
             
            for (int round = 0; round < 16; round++)
            {
                manglerFunction(ref right, ref left,ref round);
            }
            preCipher = right;
            preCipher += left;
            cipherText = permutationChoice(preCipher, DESConstants.inverseInitialPermutation);
            return cipherText;
        }
        private string XOR (string key,ref string text)
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
            int pow = binary.Length - 1;
            for (int index = 0; index <binary.Length; index++)
            {
                if (binary[index] == '1')
                {
                    number += (byte)Math.Pow(2, pow);
                }
                pow--;
            }
            return number;

        }
        private void generateKeys(string key)
        {
            string binaryKey = hexaToBinary(key);
            string _56BitKey = permutationChoice(binaryKey,DESConstants.pc1);
            string c = "", d = "";
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
        private void splitString(string str,ref string left, ref string right)
        {
            int size = str.Length/2;
            for (int index = 0; index < size; index++)
            {
                left += str[index];
                right += str[index + size];
            }
        }
        private void manglerFunction(ref string right, ref string left, ref int subKeyIndex)
        {
            string expandedRight = permutationChoice(right, DESConstants.expansionTable);
            string xor = XOR(_16SubKeys[subKeyIndex], ref expandedRight);
            string sboxes = sboxReduction(xor);
            string permutatedRight = permutationChoice(sboxes, DESConstants.permutationTable);
            string leftXORRight = XOR(left, ref permutatedRight);
            left = right;
            right = leftXORRight;
        }
        private string sboxReduction(string text)
        {
            StringBuilder sboxOutput32Bit = new StringBuilder();
            byte row, column,result,sboxIndex=0;
            string strRow,strColumn;
            for (int index=0;index< text.Length; index += 8)
            {
                strColumn = text.Substring(index+1, 4);
                strRow = text[index] + text[index+5].ToString();
                row = binaryToDecimal(strRow);
                column = binaryToDecimal(strColumn);
                result = DESConstants.sboxes[sboxIndex,row,column];
                sboxOutput32Bit.Append(DESConstants.decimalToBinaryMap[result]);
                sboxIndex++;
            }
            return sboxOutput32Bit.ToString();
        }
    }
}
