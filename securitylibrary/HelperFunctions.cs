using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public static class HelperFunctions
    {
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
        public static string AND(string input1, string input2)
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
        public static string OR(string input1, string input2)
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

        public static string NOT(string input)
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
    }
}
