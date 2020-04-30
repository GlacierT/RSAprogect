using System;

namespace Key
{
    public class RSAkey
    {
        public bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;
 
            if (n == 2)
                return true;
 
            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;
 
            return true;
        }
        
        public long Calculate_e()
        {
            long d = 0;
            long[] mas = {3, 5, 7, 11, 17, 31};
 
            var ran = new Random();
            d = mas[ran.Next(0, 5)];

            return d;
            
        }
        
        public long Calculate_d(long d, long m)
        {
            long e = 10;
 
            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }
 
            return e;
        }
        
    }
}