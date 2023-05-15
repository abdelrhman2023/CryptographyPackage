using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    public class MD5
    {
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
            /*output.Append(d.ToString("X"));
            output.Append(c.ToString("X"));
            output.Append(b.ToString("X"));
            output.Append(a.ToString("X"));*/
            return postPocessText(a,b,c,d);
        }
        private string postPocessText(uint a, uint b, uint c, uint d)
        {
            byte[] hashBytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(d), 0, hashBytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(c), 0, hashBytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(b), 0, hashBytes, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(a), 0, hashBytes, 12, 4);

            string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashString;
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
