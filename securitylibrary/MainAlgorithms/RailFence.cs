using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.Replace(" ", "").ToLower();
            plainText = plainText.Replace(" ", "").ToLower();
            int key = -1;
            for(int i=0; i<plainText.Count(); i++)
            {
                if(Encrypt(plainText,i) == cipherText)
                {
                    key = i;
                    break;
                    
                }
            }
            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            cipherText = cipherText.Replace(" ", "").ToLower();
            String planText = "";


            int number = (int)Math.Ceiling((double)(cipherText.Count() / (double)key));

            for (int n = 0; n < number; n++)
            {
                for (int i = n; i < cipherText.Count(); i += number)
                {
                    if (i >= cipherText.Count())
                    {
                        continue;
                    }
                    else
                    {
                        planText += cipherText[i];
                    }

                }
            }
            return planText;
        }

        public string Encrypt(string plainText, int key)
        {
            
            plainText = plainText.Replace(" ", "").ToLower();
            String cipherText = "";

            for (int k = 0; k < key; k++)
            {
                for (int j = k; j < plainText.Count(); j += key)
                {
                    if (j >= plainText.Count()) 
                    {
                        continue;
                    }
                    else
                    {
                        cipherText += plainText[j];
                    }

                }
            }

            return cipherText;
        }
    }
 }

