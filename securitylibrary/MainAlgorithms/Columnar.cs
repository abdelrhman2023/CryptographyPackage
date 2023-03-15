using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();

            bool Flag = false;
            int coulmn = 0;
            int row = 0;

            // find number of Columns and Rows
            for (int i = 0; i < plainText.Length; i++)
            {
                if (cipherText[0] == plainText[i])
                {
                    //if two letter are similer after eachother in each plain and cipher skip the second and search from next one
                    // plain  -> attackpostponeduntiltwoam
                    // cipher -> ttnaaptmtsuoaodwcoiknlpet

                    if (cipherText[1] == plainText[i + 1])
                    {
                        i++;
                    }

                    for (int j = i + 1; j < plainText.Length; j++)
                        if (cipherText[1] == plainText[j])
                        {
                            // number of column = length between the two letters 
                            coulmn = j - i;
                            row = plainText.Length / coulmn;
                            Flag = true;
                            break;
                        }
                }
                if (Flag == true)
                {
                    break;
                }
            }

            //Console.WriteLine(coulmn);

            // create matrix of plaintext
            int count = 0;
            int[,] matrix = new int[row, coulmn];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coulmn; j++)
                {
                    matrix[i, j] = plainText[count];
                    count++;
                }
            }

            //find key of cipher
            int colNum = 1;
            int[] arrKey = new int[coulmn];
            for (int i = 0; i < cipherText.Length - 1; i += row)
            {
                for (int j = 0; j < coulmn; j++)
                {
                    if (cipherText[i] == matrix[0, j] && cipherText[i + 1] == matrix[1, j])
                    {
                        arrKey[j] = colNum;
                        colNum++;
                        break;
                    }
                }

            }
            List<int> key = arrKey.ToList<int>();

            return key;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
           // Console.WriteLine(cipherText.to);
            cipherText = cipherText.Replace(" ", "").ToLower();
            String plainText = "";
            int number = (int)Math.Ceiling((double)(cipherText.Count() / (double)key.Count()));
            char[,] data = new char[number, key.Count()];
            int size = cipherText.Count();
            int cipherIndex = 0;

            
            for (int k = 0; k < key.Count(); k++)
            {
                int index = key.IndexOf(k+1);
                for(int i =0; i<number; i++)
                {
                    if (size >= (i * key.Count() + index + 1))
                    {
                        data[i, index] = cipherText[cipherIndex++];
                    }
                    else
                    {
                        cipherIndex--;
                    }
                }


            }

            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < key.Count(); j++)
                {

                    Console.Write(data[i, j]);
                    plainText += data[i, j];

                }
             

            }
            return plainText;

        }

        public string Encrypt(string plainText, List<int> key)
        {
            plainText = plainText.Replace(" ", "").ToLower();
            String cipherText = "";
            int number = (int)Math.Ceiling((double)(plainText.Count() / (double)key.Count()));
            char[,] data = new char[number, key.Count()];
            int index = 0;

            for(int i = 0; i< number; i++)
            {
                for(int j=0; j<key.Count(); j++)
                {
                    if (index < plainText.Count())
                        data[i, j] = plainText[index++];
                    else break;
              
                }

            }
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < key.Count(); j++)
                {

                    Console.Write(data[i, j]);



                }
                Console.WriteLine();

            }

            for (int i=1; i<=key.Count(); i++)
            {
               int idx = key.IndexOf(i);
               for(int j =0; j<number; j++)
                {
                    if (data[j, idx] == '\0') continue;
                    cipherText += data[j, idx];
                }

            }


            Console.WriteLine(cipherText);
            return cipherText;

        }
    }
}
