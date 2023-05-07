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
            int ya, yb, keyA, keyB;
            ya = HelperFunctions.fastPower(alpha, xa, q);
            keyB = HelperFunctions.fastPower(ya, xb, q);
            yb = HelperFunctions.fastPower(alpha, xb, q);
            keyA = HelperFunctions.fastPower(yb, xa, q);
            List<int> keys = new List<int>() { keyA, keyB };
            return keys;
        }
     
    }

}
