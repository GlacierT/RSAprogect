using System;
using System.Numerics;
using System.Collections.Generic;

namespace ED
{
    public class EncodeDecode
    {
        char[] characters = new char[] {'а',' ', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й', 'к', 
            'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я', '-', ',',
            '.',  'А', 'Б', 'В', 'Г', 'Ґ', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 
            'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ю', 'Я', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '!', '?' };
        
        public List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();
 
            BigInteger bi;
            
            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);
 
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);
 
                BigInteger n_ = new BigInteger((int)n);
 
                bi = bi % n_;
 
                result.Add(bi.ToString());
            }
 
            return result;
        }
        
        public string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";
 
            BigInteger bi;
 
            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);
 
                BigInteger n_ = new BigInteger((int)n);
 
                bi = bi % n_;
 
                int index = Convert.ToInt32(bi.ToString());
 
                result += characters[index].ToString();
            }
 
            return result;
        }
    }
}