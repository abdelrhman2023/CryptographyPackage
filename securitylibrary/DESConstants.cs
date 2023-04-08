using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    internal static class DESConstants
    {
        public static readonly int[,] pc1Matrix = new int[8, 7] {
           { 57, 49, 41, 33, 25, 17, 9 },
           { 1, 58, 50, 42, 34, 26, 18 },
           { 10, 2, 59, 51, 43, 35, 27 },
           { 19, 11, 3, 60, 52, 44, 36 },
           { 63, 55, 47, 39, 31, 23, 15 },
           { 7, 62, 54, 46, 38, 30, 22 },
           { 14, 6, 61, 53, 45, 37, 29 },
           { 21, 13, 5, 28, 20, 12, 4 }
           };
        public static readonly int[] leftShifts = new int[16] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        public static readonly Dictionary<char, string> hexaToBinaryMap = new Dictionary<char, string> {{'0',"0000"}, { '1', "0001" } , 
           { '2', "0010" } , { '3', "0011" } , { '4', "0100" },{'5',"0101"},{'6',"0110"},{'7',"0111"},
           {'8',"1000"},{'9',"1001"},{'A',"1010"},{'B',"1011"},{'C',"1100"},{'D',"1101"},{'E',"1110"},
           {'F',"1111"}};
        
        public static readonly int[] initialPermutation = new int[64] {
           58, 50, 42, 34, 26, 18, 10, 2,
           60, 52, 44, 36, 28, 20, 12, 4,
           62, 54, 46, 38, 30, 22, 14, 6,
           64, 56, 48, 40, 32, 24, 16, 8,
           57, 49, 41, 33, 25, 17, 9, 1,
           59, 51, 43, 35, 27, 19, 11, 3,
           61, 53, 45, 37, 29, 21, 13, 5,
           63, 55, 47, 39, 31, 23, 15, 7
           };

        public static readonly int[] inverseInitialPermutation = new int[64] {
           40, 8, 48, 16, 56, 24, 64, 32,
           39, 7, 47, 15, 55, 23, 63, 31,
           38, 6, 46, 14, 54, 22, 62, 30,
           37, 5, 45, 13, 53, 21, 61, 29,
           36, 4, 44, 12, 52, 20, 60, 28,
           35, 3, 43, 11, 51, 19, 59, 27,
           34, 2, 42, 10, 50, 18, 58, 26,
           33, 1, 41, 9, 49, 17, 57, 25
           };
    }
}
