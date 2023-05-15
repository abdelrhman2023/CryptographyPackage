using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    public class MD5
    {
        /*private string a,b,c,d;
        private List<string> _16Words;
        public MD5()
        {
            a = "0x01234567"; b = "0x89ABCDEF"; c = "0xFEDCBA98"; d = "0x76543210";
            _16Words = new List<string>();
            MD5Constants.calculateTConstants();
        }
        public string GetHash(string text)
        {
            a = HelperFunctions.hexaToBinary(ref a);
            b = HelperFunctions.hexaToBinary(ref b);
            c = HelperFunctions.hexaToBinary(ref c);
            d = HelperFunctions.hexaToBinary(ref d);
            processText(ref text);
            string g, gAndA, gAndAAndX, t, cls;
            int iteration = 0;
            for (int i = 0; i < 16; i++)
            {
                g = F(b, c, d);
                gAndA = HelperFunctions.ADD(g, a);
                gAndAAndX = HelperFunctions.ADD(gAndA, _16Words[i]);
                t = HelperFunctions.ADD(MD5Constants.tConstants[iteration],gAndAAndX);
                circularShiftLeft(ref t, MD5Constants.wordsOrderInIteration[0, i % 5]);
                cls = HelperFunctions.ADD(b, t);
                updateBuffers(ref a, ref b, ref c, ref d);
                iteration++;
            }
            for (int i = 0; i < 16; i++)
            {
                g = G(b, c, d);
                gAndA = HelperFunctions.ADD(g, a);
                gAndAAndX = HelperFunctions.ADD(gAndA, _16Words[MD5Constants.wordsOrderInIteration[1,i]]);
                t = HelperFunctions.ADD(MD5Constants.tConstants[iteration], gAndAAndX);
                circularShiftLeft(ref t, MD5Constants.circularShiftLeft[1, i % 5]);
                cls = HelperFunctions.ADD(b, t);
                updateBuffers(ref a, ref b, ref c, ref d);
                iteration++;
            }
            for (int i = 0; i < 16; i++)
            {
                g = H(b, c, d);
                gAndA = HelperFunctions.ADD(g, a);
                gAndAAndX = HelperFunctions.ADD(gAndA, _16Words[MD5Constants.wordsOrderInIteration[2, i]]);
                t = HelperFunctions.ADD(MD5Constants.tConstants[iteration], gAndAAndX);
                circularShiftLeft(ref t, MD5Constants.circularShiftLeft[2, i % 5]);
                cls = HelperFunctions.ADD(b, t);
                updateBuffers(ref a, ref b, ref c, ref d);
                iteration++;
            }
            for (int i = 0; i < 16; i++)
            {
                g = I(b, c, d);
                gAndA = HelperFunctions.ADD(g, a);
                gAndAAndX = HelperFunctions.ADD(gAndA, _16Words[MD5Constants.wordsOrderInIteration[3, i]]);
                t = HelperFunctions.ADD(MD5Constants.tConstants[iteration], gAndAAndX);
                circularShiftLeft(ref t, MD5Constants.circularShiftLeft[3, i % 5]);
                cls = HelperFunctions.ADD(b, t);
                updateBuffers(ref a, ref b, ref c, ref d);
                iteration++;
            }
            string final = a.ToString() + b.ToString() + c.ToString() + d.ToString();
            return final;
        }
        private void processText(ref string text)
        {
            int originalLength = text.Length;
            StringBuilder binaryText = getASCIIEncoding(ref text);
            padText(ref binaryText);
            appendLeastSignificant64Bits(ref binaryText, originalLength);
            string updatedBinaryText = binaryText.ToString();
            for(int index = 0; index < updatedBinaryText.Length; index+=32)
            {
                _16Words.Add(updatedBinaryText.Substring(index, 32));
            }

            //sb.Append(hashBytes[i].ToString("x2")) -> converts to decimal
        }
        private StringBuilder getASCIIEncoding(ref string text)
        {
            StringBuilder asciiEncoding = new StringBuilder();
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            string currentByte;
            for (int index = 0; index < inputBytes.Length; index++)
            {
                currentByte = Convert.ToString(inputBytes[index],2);
                while (currentByte.Length != 8)
                {
                    currentByte.Insert(0, "0");
                }
                asciiEncoding.Append(currentByte);
            }
            return asciiEncoding;
        }
        private void padText(ref StringBuilder text)
        {
            if (text.Length < 448)
            {
                text.Append("1");
                int remainingLength = text.Length - 448;
                for (int index = 0; index < remainingLength; index++)
                {
                    text.Append("0");
                }
            }
        }
        private void appendLeastSignificant64Bits(ref StringBuilder text, int originalTextLength)
        {
            string binaryLength = Convert.ToString(originalTextLength, 2);
            int remainingLength = 64 - binaryLength.Length;
            for (int index = 0;index < remainingLength; index++)
            {
                binaryLength = "0" + binaryLength;
            }
            text.Append(binaryLength);
        }
        private string F(string b,string c, string d)
        {
            string notB, notBAndD, bAndC, or;
            notB = HelperFunctions.NOT(ref b);
            notBAndD = HelperFunctions.AND(ref notB,ref d);
            bAndC = HelperFunctions.AND(ref b,ref c);
            or = HelperFunctions.OR(ref bAndC,ref notBAndD);
            return or;
        }
        private string G(string b, string c, string d)
        {
            string bAndD, notD, cAndNotD, or;
            bAndD = HelperFunctions.AND(ref b,ref d);
            notD = HelperFunctions.NOT(ref d);
            cAndNotD = HelperFunctions.AND(ref c,ref notD);
            or = HelperFunctions.OR(ref bAndD,ref cAndNotD);
            return or;
        }
        private string H(string b, string c, string d)
        {
            string bXorC, xor;
            bXorC = HelperFunctions.XOR(b, ref c);
            xor = HelperFunctions.XOR(bXorC,ref d);
            return xor;
        }
        private string I(string b, string c, string d)
        {
            string notD, bOrNotD, xor;
            notD = HelperFunctions.NOT(ref d);
            bOrNotD = HelperFunctions.OR(ref b,ref notD);
            xor = HelperFunctions.XOR(c, ref bOrNotD);
            return xor;
        }
        private void updateBuffers(ref string a, ref string b, ref string c, ref string d)
        {
            string temporary = d;
            d = c;
            c = b;
            b = a;
            a = temporary;
        }
        private void circularShiftLeft(ref string input, int shiftLefts)
        {
            StringBuilder sb = new StringBuilder(input);
            sb.Append(sb.ToString(0, shiftLefts));
            sb.Remove(0, shiftLefts);
            input = sb.ToString();
        }*/
        
        private uint a = 0x67452301, b = 0xEFCDAB89, c = 0x98BADCFE, d = 0x10325476;
        private List<uint> words = new List<uint>();
        public string GetHash(string text)
        {
            preprocessText(ref text);
            MD5Constants.calculateT();
            int shiftLeftAmountRow = 0, wordIndexRow = 0, tConstantIndex = 0;
            singleRound(F,ref shiftLeftAmountRow,ref wordIndexRow,ref tConstantIndex);
            singleRound(G, ref shiftLeftAmountRow, ref wordIndexRow, ref tConstantIndex);
            singleRound(H, ref shiftLeftAmountRow, ref wordIndexRow, ref tConstantIndex);
            singleRound(I, ref shiftLeftAmountRow, ref wordIndexRow, ref tConstantIndex);
            StringBuilder output =  new StringBuilder();
            output.Append(d.ToString("X"));
            output.Append(c.ToString("X"));
            output.Append(b.ToString("X"));
            output.Append(a.ToString("X"));
            return output.ToString();
        }
        private byte[] ToLittleEndian(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }
        private void preprocessText(ref string text) 
        {
            int index;uint value;
            string inputSize= Convert.ToString(text.Length,2),binaryTextString,substring;
            StringBuilder binaryText = new StringBuilder();
            //TODO-> encoding ?
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            for (index = 0; index < inputBytes.Length; index++)
            {
                binaryText.Append(Convert.ToString(inputBytes[index],2));
            }
            bool isFirst = true;
            while (binaryText.Length < 448)
            {
                if (!isFirst)
                {
                    binaryText.Append("0");
                }
                else
                {
                    binaryText.Append("1");
                    isFirst = false;
                }
            }
            int remainingLength = 64 - inputSize.Length;
            for (index = 0; index < remainingLength; index++)
            {
                binaryText.Append("0");
            }
            binaryText.Append(inputSize);
            binaryTextString = binaryText.ToString();
            for (index=0;index<binaryText.Length; index+=32)
            {
                substring = binaryTextString.Substring(index, 32);
                value = Convert.ToUInt32(substring,2);
                words.Add(value);
            }
        }


        private uint F(uint b, uint c, uint d)
        {
            return ((b & c) | ((~b) & d));
        }
        private uint G(uint b, uint c, uint d)
        {
            return ((b & d) | c & (~d));
        }
        private uint H(uint b, uint c, uint d)
        {
            return (b ^ c ^ d);
        }
        private uint I(uint b, uint c, uint d)
        {
            return (c ^ (b | (~d)));
        }
        private uint circularShiftLeft(ref uint input, ref int shiftAmount)
        {
            return (input << shiftAmount) | (input >> (32 - shiftAmount));
        }
        private void swap(ref uint a, ref uint b, ref uint c,ref uint d)
        {
            uint temp = d;
            d = c;
            c = b;
            b = a;
            a = temp;
        }
        private void singleRound(Func<uint, uint, uint, uint> roundFunction,ref int shiftLeftAmountRow,ref int wordIndexRow,ref int tConstantIndex)
        {
            uint g, gPlusA, gPlusAPlusX, gPlusAPlusXPlusT, circularShift;
            for (int round = 0; round < 16; round++)
            {
                g = roundFunction(b, c, d);
                gPlusA = (uint)(g + a);
                //TODO
                gPlusAPlusX = (uint)(gPlusA + (uint)words[MD5Constants.wordsOrderInIteration[wordIndexRow,round]]);
                gPlusAPlusXPlusT = (uint)(gPlusAPlusX + MD5Constants.t[tConstantIndex]);
                tConstantIndex++;
                circularShift = circularShiftLeft(ref gPlusAPlusXPlusT,ref MD5Constants.circularShiftLeft[shiftLeftAmountRow,round%4]);
                a = (uint)(circularShift + b);
                // END TODO
                swap(ref a, ref b, ref c, ref d);
            }
            shiftLeftAmountRow++;
            wordIndexRow++;
        }

    }
}
