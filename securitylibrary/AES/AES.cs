using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }


        // helper function convert from Hex To Bin.
        static string hex2binary(string hexvalue)
        {
            string binarystring = String.Join(String.Empty, hexvalue.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            return binarystring;
        }

        // 1- subBytes
        static public string subBytes(string plainText)
        {
            string[,] sBox = new string[16, 16] {
                {"63", "7c", "77", "7b", "f2", "6b", "6f", "c5", "30", "01", "67", "2b", "fe", "d7", "ab", "76"},
                {"ca", "82", "c9", "7d", "fa", "59", "47", "f0", "ad", "d4", "a2", "af", "9c", "a4", "72", "c0"},
                {"b7", "fd", "93", "26", "36", "3f", "f7", "cc", "34", "a5", "e5", "f1", "71", "d8", "31", "15"},
                {"04", "c7", "23", "c3", "18", "96", "05", "9a", "07", "12", "80", "e2", "eb", "27", "b2", "75"},
                {"09", "83", "2c", "1a", "1b", "6e", "5a", "a0", "52", "3b", "d6", "b3", "29", "e3", "2f", "84"},
                {"53", "d1", "00", "ed", "20", "fc", "b1", "5b", "6a", "cb", "be", "39", "4a", "4c", "58", "cf"},
                {"d0", "ef", "aa", "fb", "43", "4d", "33", "85", "45", "f9", "02", "7f", "50", "3c", "9f", "a8"},
                {"51", "a3", "40", "8f", "92", "9d", "38", "f5", "bc", "b6", "da", "21", "10", "ff", "f3", "d2"},
                {"cd", "0c", "13", "ec", "5f", "97", "44", "17", "c4", "a7", "7e", "3d", "64", "5d", "19", "73"},
                {"60", "81", "4f", "dc", "22", "2a", "90", "88", "46", "ee", "b8", "14", "de", "5e", "0b", "db"},
                {"e0", "32", "3a", "0a", "49", "06", "24", "5c", "c2", "d3", "ac", "62", "91", "95", "e4", "79"},
                {"e7", "c8", "37", "6d", "8d", "d5", "4e", "a9", "6c", "56", "f4", "ea", "65", "7a", "ae", "08"},
                {"ba", "78", "25", "2e", "1c", "a6", "b4", "c6", "e8", "dd", "74", "1f", "4b", "bd", "8b", "8a"},
                {"70", "3e", "b5", "66", "48", "03", "f6", "0e", "61", "35", "57", "b9", "86", "c1", "1d", "9e"},
                {"e1", "f8", "98", "11", "69", "d9", "8e", "94", "9b", "1e", "87", "e9", "ce", "55", "28", "df"},
                {"8c", "a1", "89", "0d", "bf", "e6", "42", "68", "41", "99", "2d", "0f", "b0", "54", "bb", "16"}
            };

            string newPlain = "0x";

            for (int i = 2; i < plainText.Length; i += 2)
            {
                int row = int.Parse("0" + plainText[i], System.Globalization.NumberStyles.HexNumber);
                int col = int.Parse("0" + plainText[i + 1], System.Globalization.NumberStyles.HexNumber);
                newPlain += sBox[row, col];
            }

            return newPlain;
        }

        // 2- shiftRows
        static public string shiftRows(string plainText)
        {

            string r1 = "", r2 = "", r3 = "", r4 = "";
            string c1 = "", c2 = "", c3 = "", c4 = "";

            //divide the matrix into rows
            for (int j = 0; j < 4; j++)
            {
                r1 += plainText.Substring((j * 8) + (0 * 2), 2);
                r2 += plainText.Substring((j * 8) + (1 * 2), 2);
                r3 += plainText.Substring((j * 8) + (2 * 2), 2);
                r4 += plainText.Substring((j * 8) + (3 * 2), 2);
            }

            string s = r1 + r2 + r3 + r4;


            string shiftedPlain = "";
            shiftedPlain += s.Substring(0, 8);

            for (int i = 1, j = 8; i < 4; i++, j += 8)
            {
                string temp = s.Substring(j, 8);
                shiftedPlain += temp.Substring(i * 2, 8 - (i * 2));
                shiftedPlain += temp.Substring(0, i * 2);
            }

            string newpalin = "0x";

            for (int j = 0; j < 4; j++)
            {
                c1 += shiftedPlain.Substring((j * 8) + (0 * 2), 2);
                c2 += shiftedPlain.Substring((j * 8) + (1 * 2), 2);
                c3 += shiftedPlain.Substring((j * 8) + (2 * 2), 2);
                c4 += shiftedPlain.Substring((j * 8) + (3 * 2), 2);
            }
            newpalin += (c1 + c2 + c3 + c4);

            return newpalin;
        }

        // 3- mixColumns
        static public string mixColumns(string plainText)
        {
            string[] state = new string[4];

            int[][] arr = {
                new int[] { 2, 3, 1, 1 },
                new int[] { 1, 2, 3, 1 },
                new int[] { 1, 1, 2, 3 },
                new int[] { 3, 1, 1, 2 }
            };

            string s = "";
            for (int j = 0; j < 4; j++)
            {
                s = plainText.Substring(j * 8, 8);
                state[j] = s;
            }


            string newplain = "";
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    byte res = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        string Byte = state[k].Substring(j * 2, 2);
                        string Bin_Byte = hex2binary(Byte);

                        bool XOR = false;

                        if (Bin_Byte[0] == '1' && arr[i][j] != 1)
                        {
                            XOR = true;
                        }

                        byte num = Convert.ToByte(Byte, 16);
                        byte temp = num;

                        if (arr[i][j] != 1)
                        {
                            num <<= 1;
                        }
                        if (XOR)
                        {
                            num = (byte)(num ^ Convert.ToByte("0x1b", 16));
                        }
                        if (arr[i][j] == 3)
                        {
                            num = (byte)(num ^ temp);
                        }

                        res = (byte)(res ^ num);
                    }

                    if (res.ToString("x").Length == 1)
                    {
                        newplain += '0' + res.ToString("x");
                    }
                    else
                    {
                        newplain += res.ToString("x");
                    }
                }
            }

            return "0x" + newplain;
        }

        // 4- addRoundKey
        static public string addRoundKey(string plainText, string key)
        {
            string Res_hex = "";

            for (int i = 0; i < 4; i++)
            {
                long sub_plain = Convert.ToInt64(plainText.Substring(i * 8, 8), 16);
                long sub_key = Convert.ToInt64(key.Substring(i * 8, 8), 16);
                long res_XOR = sub_plain ^ sub_key;

                int Length = res_XOR.ToString("x").Length;

                // To ensure that it is exactly 8 hexadecimal characters long
                while (Length != 8)
                {
                    Res_hex += '0';
                    Length++;
                }

                Res_hex += res_XOR.ToString("x");
            }
            return "0x" + Res_hex;
        }
        public static string generateNewKey(string key, string rcon)
        {
            string Col_rotated = key.Substring(28, 6);
            Col_rotated += key.Substring(26, 2);

            string converted_col = "";
            key = key.Substring(2);

            converted_col += subBytes("0x" + Col_rotated);
            converted_col = converted_col.Substring(2);



            string newKey = "0x";
            for (int i = 0; i < 4; i++)
            {
                string s = "";
                for (int j = 0; j < 4; j++)
                {
                    byte Key_Col = Convert.ToByte(key.Substring((i * 8) + (j * 2), 2), 16);
                    byte last_ColByte = Convert.ToByte(converted_col.Substring(j * 2, 2), 16);

                    byte res = (byte)(Key_Col ^ last_ColByte);

                    if (i == 0)
                    {
                        byte rcon_byte = Convert.ToByte(rcon.Substring(j * 2, 2), 16);
                        res = (byte)(res ^ rcon_byte);
                    }

                    if (res.ToString("x").Length == 1)
                    {
                        s += '0';
                    }
                    s += res.ToString("x");
                }

                converted_col = s;
                newKey += s;
            }
            return newKey;
        }
        public override string Encrypt(string plainText, string key)
        {
            string newPlain = "";

            string[] rcon = { "01000000",
                              "02000000",
                              "04000000",
                              "08000000",
                              "10000000",
                              "20000000",
                              "40000000",
                              "80000000",
                              "1b000000",
                              "36000000"
            };

            // Init
            newPlain += addRoundKey(plainText.Substring(2), key.Substring(2));

            plainText = newPlain;


            for (int i = 0; i < 10; i++)
            {
                // 1- SubBytes
                newPlain = subBytes(plainText);

                // 2- shiftRows
                plainText = shiftRows(newPlain.Substring(2));

                // 3- mixColumns
                if (i != 9)
                {
                    newPlain = mixColumns(plainText.Substring(2));
                }
                else
                {
                    newPlain = plainText;
                }

                key = generateNewKey(key, rcon[i]);

                // 4- addRoundKey
                plainText = addRoundKey(newPlain.Substring(2), key.Substring(2));
            }

            return plainText;
        }




    }
}