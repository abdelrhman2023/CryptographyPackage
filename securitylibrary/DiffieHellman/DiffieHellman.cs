using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            int ya,yb, keyA,keyB;
            ya = fastPower(alpha, xa, q);
            keyB = fastPower(ya, xb, q);
            yb = fastPower(alpha, xb, q);
            keyA = fastPower(yb, xa, q);
            List<int> keys = new List<int>() { keyA,keyB};
            return keys;
        }
        private int fastPower(int baseNum, int exponent,int modulo)
        {
            if (exponent == 0) return 1;
            if (exponent == 1) return baseNum;

            int result = fastPower(baseNum, exponent / 2, modulo);
            if (exponent % 2 == 0)
            {
                return (result * result)%modulo;
            }
            else
            {
                return (result * result * baseNum)%modulo;
            }
        }
        /*private int fastPower(int b,int p, int modulo)
        {
            if (p == 0)
            {
                return 1;
            }
            else if (p == 1)
            {
                return b;
            }
            else if (p % 2 == 0)
            {
                return fastPower(b, p / 2, modulo) % modulo;
            }
            else
            {
                return b * fastPower(b, p / 2, modulo) % modulo;
            }
        }*/
    }

}
