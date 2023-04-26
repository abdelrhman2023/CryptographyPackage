using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        /// 


        DiffieHellman.DiffieHellman DH = new DiffieHellman.DiffieHellman();
        AES.ExtendedEuclid inverse = new AES.ExtendedEuclid();

        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            List<long> result = new List<long>();

            long K = DH.fastPower(y, k, q);

            long C1 = DH.fastPower(alpha,k, q);

            long C2 = (K * m) % q;

            result.Add(C1);
            result.Add(C2);
            
            
            return result;
            //throw new NotImplementedException();

        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            int result ;


            int K = DH.fastPower(c1, x, q);

            K = inverse.GetMultiplicativeInverse(K, q);

            result = (c2*K) % q;

            return result;


            //throw new NotImplementedException();

        }
    }
}
