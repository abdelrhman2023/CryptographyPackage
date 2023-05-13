using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    internal static class MD5Constants
    {
        public static List<string> tConstants = new List<string>();
        public static List<uint> t = new List<uint>();
        public static int[,] circularShiftLeft = new int[4, 4]
        {
            { 7,12,17,22},{5,9,14,20},{4,11,16,23},{6,10,15,21}
        };
        public static void calculateTConstants()
        {
            int _2Power32 = (int)Math.Pow(2, 32), currentT;
            for (int iteration = 0; iteration < 64; iteration++)
            {
                currentT = (int)Math.Floor(Math.Abs(Math.Sin(iteration + 1) * _2Power32));
                tConstants.Add(Convert.ToString(currentT, 2));
            }
        }
        public static int[,] wordsOrderInIteration = new int[4,16]
        {
            {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 },
            {1,6,11,0,5,10,15,4,9,14,3,8,13,2,7,12 },
            {5,8,11,14,1,4,7,10,13,0,3,6,9,12,15,2 },
            {0,7,14,5,12,3,10,1,8,15,6,13,4,11,2,9 }
        };
        public static void calculateT()
        {
            uint _2Power32 = (uint)Math.Pow(2, 32), currentT;
            for (int iteration = 0; iteration < 64; iteration++)
            {
                currentT = (uint)Math.Floor(Math.Abs(Math.Sin(iteration + 1) * _2Power32));
                t.Add(currentT);
            }
        }
    }
}
