using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class TripleDES : ICryptographicTechnique<string, List<string>>
    {
        private DES des = null;
        private string stage_one, stage_two,result;
        public TripleDES()
        {
            des = new DES();
        }
        public string Decrypt(string cipherText, List<string> key)
        {
            stage_one = des.Decrypt(cipherText, key[0]);
            stage_two = des.Encrypt(stage_one, key[1]);
            result = des.Decrypt(stage_two, key[0]);
            return result;
        }

        public string Encrypt(string plainText, List<string> key)
        {
            stage_one = des.Encrypt(plainText, key[0]);
            stage_two = des.Decrypt(stage_one, key[1]);
            result = des.Encrypt(stage_two, key[0]);
            return result;
        }

        public List<string> Analyse(string plainText,string cipherText)
        {
            throw new NotSupportedException();
        }

    }
}
