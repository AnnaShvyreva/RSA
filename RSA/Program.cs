using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong n = 471166331128397;
            ulong e = 12971;
            string text = "25179548613190511513070917903946655028427025822969873262852057834743091062127365909095368";

            ulong m = eiler(n);

            ulong d = d_found(e, m);
            
            Console.WriteLine("функция Эйлера(n) = " + m);
            Console.WriteLine("Открытый ключ:{ " + e + ", " + n + "}");
            Console.WriteLine("Закрытый ключ:{ " + d + ", " + n + "}");


            //Console.WriteLine("d/m = " + d / m + " d%m = " + d % m);
            // разбиение на блоки и дешифровка
            string textRes = "";
            Console.WriteLine(text);
            string s = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Convert.ToUInt64(s + text[i]) >= n || (i==text.Length-1 && s != ""))
                {
                    if (i == text.Length - 1 && s != "") s = s + text[i];
                    //Console.Write("s= " + s+ " ");
                    textRes =textRes + decryption(s, d, n);
                    //Console.WriteLine();
                    i--;
                    s = ""; 
                }
                else
                {
                    s = s + text[i];
                }
            }

            Console.WriteLine();
            Console.WriteLine(textRes);

            if (textRes.Length % 2 != 0)
            {
                textRes = "0" + textRes;
            }

            byte[] res_text = new byte[textRes.Length / 2];
            for (int i = 0; i < res_text.Length; i++)
            {
                res_text[i] = Convert.ToByte(Convert.ToString(textRes[i * 2]) + Convert.ToString(textRes[i * 2 + 1]));
            }
            string message = Encoding.Default.GetString(res_text);

            Console.WriteLine(message);
            
            Console.ReadLine();
        }

        public static ulong eiler(ulong n)
        {
            ulong x = fact(n);
            ulong y = (ulong)Math.Sqrt(Math.Pow(x, 2) - n);
            ulong p = x + y;
            ulong q = x - y;

            return (p - 1) * (q - 1);
        }

        public static ulong fact(ulong n) // Метод факторизации Ферма: x^2-y^2=n, (x-y)(x+y)=n, n=a*b, a=x+y, b=x-y
        {
            //Console.WriteLine("Факторизация начата: " + System.DateTime.Now);
            ulong k = 0;

            for (k = 0; ; k++)
            {
                if (Math.Sqrt(Math.Pow((Math.Truncate(Math.Sqrt(n)) + 1 + k), 2) - n) == 
                    Math.Truncate(Math.Sqrt(Math.Pow((Math.Truncate(Math.Sqrt(n)) + 1 + k), 2) - n)))
                    break;
            }

            //Console.WriteLine("Факторизация закончена: " + System.DateTime.Now);

            return (ulong)(Math.Truncate(Math.Sqrt(n)) + 1 + k);
        }

        /*
        public static ulong evclid(ulong x, ulong y) //алгоритм Евклида НОД
        {
            Console.WriteLine("Поиск взаимнопростого d: " + System.DateTime.Now);

            ulong r, a = x, b = y;
            do
            {
                r = a % b;
                a = b;
                b = r;
            }
            while (r != 0);

            Console.WriteLine("d найдено: " + System.DateTime.Now);

            return a;

        }*/

        public static ulong d_found(ulong e, ulong m)
        {
            ulong d=0;
            for (ulong i = 0;; i++)
            {
                if ((m * i + 1) % e == 0)
                {
                    d = (m * i + 1) / e; // d = 14856753466211
                    break;
                }
            }
            return d;
        }        
        
        public static ulong d_found1(ulong e, ulong m, ulong n)
        {
            ulong x = eiler(m);
            ulong tmp = (ulong)Math.Pow(e, x - 1); //возведение в степень не работает!
            ulong d1 = tmp % n;
            Console.WriteLine("d1 = " + d1);
            ulong d = tmp % m;
            Console.WriteLine("d = " + d);

            return d;
        }

        public static string decryption(string s, ulong d, ulong n)
        {
            // m[i] = s[i]^d mod n
            //s^(eiler(n))=1 mod n
            //[s^(eiler(n))^(d/m)] *[s^(d%m)] 
            //{[(1 mod n)^(d/m)] *[s^(d%m)] } mod n            
            //Console.Write(m);
            ulong m = (ulong)(Convert.ToUInt64(s));
            m = (ulong)BigInteger.ModPow(m,d,n);          
            return Convert.ToString(m);
        }
    }
}
