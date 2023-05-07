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
        private Dictionary<string, char> binaryToHexaMap;
        public DES()
        {
            _16SubKeys = new List<string>();
            binaryToHexaMap = new Dictionary<string, char>();
            getBinaryToHexaMap();
        }
        public override string Decrypt(string cipherText, string key)
        {
            return des(ref cipherText, ref key, false);
        }

        public override string Encrypt(string plainText, string key)
        {
            return des(ref plainText, ref key, true);
        }
        private string des(ref string text,ref string key, bool isEncryption)
        {
            string binaryText = "", left = "", right = "", c = "", d = "";
            preprocessInput(ref key, ref c, ref d, DESConstants.pc1);
            generateKeys(ref c, ref d);
            preprocessInput(ref text, ref left, ref right, DESConstants.initialPermutation);
            if (isEncryption)
            {
                manglerFunction(ref right, ref left);
            }
            else
            {
                inverseManglerFunction(ref right, ref left);
            }
            binaryText = right + left;
            binaryText = permutationChoice(binaryText, DESConstants.inverseInitialPermutation);
            return binaryToHexa(ref binaryText);
        }
        private void preprocessInput(ref string input, ref string left, ref string right, byte[] permutationChoiceArray)
        {
            input = hexaToBinary(ref input);
            input = permutationChoice(input, permutationChoiceArray);
            splitString(input, ref left, ref right);
        }
        private byte binaryToDecimal(string binary)
        {
            byte number = 0;
            int pow = binary.Length - 1;
            for (int index = 0; index < binary.Length; index++)
            {
                if (binary[index] == '1')
                {
                    number += (byte)Math.Pow(2, pow);
                }
                pow--;
            }
            return number;

        }
        private void getBinaryToHexaMap()
        {
            foreach (KeyValuePair<char, string> pair in DESConstants.hexaToBinaryMap)
            {
                binaryToHexaMap.Add(pair.Value, pair.Key);
            }
        }
        private string binaryToHexa(ref string binaryCipherText)
        {
            StringBuilder hexa = new StringBuilder();
            hexa.Append("0x");
            string substring;
            for (int block = 0; block < binaryCipherText.Length; block += 4)
            {
                substring = binaryCipherText.Substring(block, 4);
                hexa.Append(binaryToHexaMap[substring]);
            }
            return hexa.ToString();
        }
        private string hexaToBinary(ref string text)
        {
            StringBuilder binary = new StringBuilder();
            for (int index = 2; index < text.Length; index++)
            {
                binary.Append(DESConstants.hexaToBinaryMap[text[index]]);
            }
            return binary.ToString();
        }
        private string permutationChoice(string input, byte[] pc)
        {
            StringBuilder permutatedChoice = new StringBuilder();
            byte position;
            for (int index = 0; index < pc.Length; index++)
            {
                position = (byte)(pc[index] - 1);
                permutatedChoice.Append(input[position]);

            }
            return permutatedChoice.ToString();
        }
        private void splitString(string str, ref string left, ref string right)
        {
            int size = str.Length / 2;
            for (int index = 0; index < size; index++)
            {
                left += str[index];
                right += str[index + size];
            }
        }
        private void generateKeys(ref string c, ref string d)
        {
            int index; string subKey;
            for (index = 0; index < 16; index++)
            {
                for (byte leftShift = 1; leftShift <= DESConstants.leftShifts[index]; leftShift++)
                {
                    c += c[0];
                    c = c.Remove(0, 1);
                    d += d[0];
                    d = d.Remove(0, 1);
                }
                subKey = c; 
                subKey += d;
                subKey = permutationChoice(subKey, DESConstants.pc2);
                _16SubKeys.Add(subKey);
            }
        }

        private string sboxReduction(string text)
        {
            StringBuilder sboxOutput32Bit = new StringBuilder();
            byte row, column, result, sboxIndex = 0;
            string strRow, strColumn;
            for (int index = 0; index < text.Length; index += 6)
            {
                strColumn = text.Substring(index + 1, 4);
                strRow = text[index] + text[index + 5].ToString();
                row = binaryToDecimal(strRow);
                column = binaryToDecimal(strColumn);
                result = DESConstants.sboxes[sboxIndex, row, column];
                sboxOutput32Bit.Append(DESConstants.decimalToBinaryMap[result]);
                sboxIndex++;
            }
            return sboxOutput32Bit.ToString();
        }
        private void manglerFunction(ref string right, ref string left)
        {
            for (int round = 0; round < 16; round++)
            {
                singleRound(ref left, ref right, _16SubKeys[round]);
            }
            
        }
        private void inverseManglerFunction(ref string right, ref string left)
        {
            for (int round = 15; round >=0; round--)
            {
                singleRound(ref left, ref right,_16SubKeys[round]);
            }
        }
        private void singleRound(ref string left, ref string right,string roundSubKey)
        {
            string expandedRight, xor, sboxes, permutatedSBoxes, leftXORRight = "";
            expandedRight = permutationChoice(right, DESConstants.expansionTable);
            xor = HelperFunctions.XOR(roundSubKey, ref expandedRight);
            sboxes = sboxReduction(xor);
            permutatedSBoxes = permutationChoice(sboxes, DESConstants.permutationTable);
            leftXORRight = HelperFunctions.XOR(left, ref permutatedSBoxes);
            left = right;
            right = leftXORRight;
        }
    }
}
