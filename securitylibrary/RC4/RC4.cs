using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class RC4 : CryptographicTechnique
    {
        bool isHexa = false;
        public override string Decrypt(string cipherText, string key)
        {
            cipherText = ConvertHexaToString( cipherText);
            key = ConvertHexaToString( key);


            string result ="";
            int j=0;



            int[] S = new int[256];
            int[] T = new int[256];



            for (int i = 0; i < S.Length; i++)
            {
                S[i] = i;
                T[i] = key[i%key.Length];
            }


            
            for(int i = 0; i < 256; i++)
            {
                j= (j+ S[i] +T[i]) %256;
                swap(ref S[i],ref S[j]);
            }
            
            int z = 0, k = 0;

            
            for (int n = 0; n < cipherText.Length ; n++)
            {
                z= (z+1) % 256;
                k= (k+ S[z]) % 256;

                swap(ref S[z],ref S[k]);

                int t=(S[z] + S[k]) % 256;

                result += Convert.ToChar(cipherText[n] ^ S[t]);
            }


            if (isHexa == true)
            {
                result = StringToHexa(result);
            }




            return result;
            
            //throw new NotImplementedException();
        }

        public override  string Encrypt(string plainText, string key)
        {
            

            string result = Decrypt(plainText, key);

            return result;

            //throw new NotImplementedException();
        }


        public void swap (ref int a,ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public string ConvertHexaToString (string HexaDecimal)
        {
            string result = "";

            if (HexaDecimal[0] == '0' && HexaDecimal[1] == 'x')
            {
                isHexa = true;
                for (int i = 2; i < HexaDecimal.Length ; i +=2)
                {
                    int intValue = Convert.ToInt32(HexaDecimal[i].ToString() + HexaDecimal[i + 1].ToString(), 16);

                    result += char.ConvertFromUtf32(intValue);
                }

                return result;
            }
            else
            {
                return HexaDecimal;
            }
        }

        public string StringToHexa (string text)
        {
            string result;
            byte[] bytes = Encoding.Default.GetBytes(text);

            string hexString = BitConverter.ToString(bytes).Replace("-", "");

            result = "0x" + hexString;

            return result;
            
        }
       
    }
}
