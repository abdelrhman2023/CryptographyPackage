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
            throw new NotImplementedException();
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
