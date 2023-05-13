using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public static class HelperFunctions
    {
        public static readonly Dictionary<char, string> hexaToBinaryMap = new Dictionary<char, string> {{'0',"0000"}, { '1', "0001" } ,
           { '2', "0010" } , { '3', "0011" } , { '4', "0100" },{'5',"0101"},{'6',"0110"},{'7',"0111"},
           {'8',"1000"},{'9',"1001"},{'A',"1010"},{'B',"1011"},{'C',"1100"},{'D',"1101"},{'E',"1110"},
           {'F',"1111"}};

        public static readonly Dictionary<byte, string> decimalToBinaryMap = new Dictionary<byte, string> {{0,"0000"}, { 1, "0001" } ,
           { 2, "0010" } , { 3, "0011" } , { 4, "0100" },{5,"0101"},{6,"0110"},{7,"0111"},
           {8,"1000"},{9,"1001"},{10,"1010"},{11,"1011"},{12,"1100"},{13,"1101"},{14,"1110"},
           {15,"1111"}};
        public static string hexaToBinary(ref string text)
        {
            StringBuilder binary = new StringBuilder();
            for (int index = 2; index < text.Length; index++)
            {
                binary.Append(hexaToBinaryMap[text[index]]);
            }
            return binary.ToString();
        }
        public static int fastPower(int baseNum, int power, int modulo)
        {
            int result = 1;
            int re = baseNum % modulo;

            for (int i = 0; i < power; i++)
            {

                result = (result * re) % modulo;
            }

            return result;

        }
        public static string XOR(string key, ref string text)
        {
            StringBuilder xor = new StringBuilder();
            for (int index = 0; index < text.Length; index++)
            {
                if ((text[index] == '1' && key[index] == '0') || (text[index] == '0' && key[index] == '1'))
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
        public static string AND(ref string input1,ref string input2)
        {
            StringBuilder and = new StringBuilder();
            for (int index = 0; index < input1.Length; index++)
            {
                if (input1[index] == '1' && input2[index] == '1') 
                {
                    and.Append('1');
                }
                else
                {
                    and.Append('0');
                }
            }
            return and.ToString();
        }
        public static string OR(ref string input1,ref string input2)
        {
            StringBuilder or = new StringBuilder();
            for (int index = 0; index < input1.Length; index++)
            {
                if (input1[index] == '1' || input2[index] == '1')
                {
                    or.Append('1');
                }
                else
                {
                    or.Append('0');
                }
            }
            return or.ToString();
        }

        public static string NOT(ref string input)
        {
            StringBuilder not = new StringBuilder();
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == '1')
                {
                    not.Append('0');
                }
                else
                {
                    not.Append('1');
                }
            }
            return not.ToString();
        }

        public static string ADD(string binaryStr1, string binaryStr2)
        {
            int num1, num2, sum;
            string binarySum;
            num1 = Convert.ToInt32(binaryStr1, 2);
            num2 = Convert.ToInt32(binaryStr2, 2);
            sum = num1 + num2;
            binarySum = Convert.ToString(sum, 2);
            return binarySum;
        }
    }
}
