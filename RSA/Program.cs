using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong n = 471166331128397;
            ulong e = 12971;
            //ulong text = 25179548613190511513070917903946655028427025822969873262852057834743091062127365909095368;
            //Console.WriteLine(fact(n));
            ulong x = fact(n);
            ulong y = (ulong)Math.Sqrt(Math.Pow(x,2) - n);
            ulong p = x + y;
            ulong q = x - y;

            //Алгоритм Евклида 
            ulong d = evclid(p - 1, q - 1);

            Console.WriteLine("p=" + p + " q=" + q + " d=" + d);

            Console.ReadLine();
        }


        public static ulong fact(ulong n) // Метод факторизации Ферма: x^2-y^2=n, (x-y)(x+y)=n, n=a*b, a=x+y, b=x-y
        {
            Console.WriteLine("Факторизация начата: " + System.DateTime.Now);
            ulong k = 0;

            for (k = 0; ; k++)
            {
                if (Math.Sqrt(Math.Pow((Math.Truncate(Math.Sqrt(n)) + 1 + k), 2) - n) == 
                    Math.Truncate(Math.Sqrt(Math.Pow((Math.Truncate(Math.Sqrt(n)) + 1 + k), 2) - n)))
                    break;
            }

            Console.WriteLine("Факторизация закончена: " + System.DateTime.Now);

            return (ulong)(Math.Truncate(Math.Sqrt(n)) + 1 + k);
        }


        public static ulong evclid(ulong x, ulong y)
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

        }
    }
}
