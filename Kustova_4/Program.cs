using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kustova_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //В зависимости от языка системы дробные числа с точкой не конвертируются из строк
            //поэтому точки вручную заменяем на запятые
            //.Replace(".", ",")

            Console.WriteLine("Выполнила Кустова Юлия Сергеевна, АЭМ-111");

            Console.Write("Введите Z: ");
            double z = double.Parse(Console.ReadLine().Replace(".", ","));
            Console.Write("Введите X: ");
            double x = double.Parse(Console.ReadLine().Replace(".", ","));
            Console.Write("Введите A: ");
            double a = double.Parse(Console.ReadLine().Replace(".", ","));

            //На больших N увеличивается погрешность и теряется точность,
            //а значения мы всё равно проверить не можем
            //Проверить можно на WolframAlpha командой
            //Table[((1.5 + 0.2 * k) * z + sin(x^k + 3 * (k - 1) * pi / 2 * ((k - 1) mod 2))) / (sqrt(x^(2k) + a^(k+1))), {k, 1, 5}]
            for (int n = 1; n <= 5; n++)
            {
                double J = 0; //результат вычисления J
                int sign = 1; //знак перед дробью
                double x_i = x;//X в текущей степени
                double x_2i = x * x;//X в текущей двойной степени
                double a_i_1 = a * a;//A в текущей степени + 1
                for (int i = 1; i <= n; i++)
                {
                    //1. -sin(a + 3 * 0 * pi / 2 * 1) = -sin(a)
                    //2. -sin(a + 3 * 1 * pi / 2 * 0) = cos(a)
                    //3. -sin(a + 3 * 2 * pi / 2 * 1) = -sin(a)
                    //4. -sin(a + 3 * 3 * pi / 2 * 0) = cos(a)
                    //...
                    //i. -sin(a + (3 * (i - 1) * pi / 2) * ((i - 1) % 2)) = -sin(a)..cos(a)..-sin(a)

                    J +=
                        sign * ((1.5 + 0.2 * i) * z + Math.Sin(x_i + 3 * (i - 1) * Math.PI / 2) * ((i - 1) % 2) /
                            (Math.Sqrt(x_2i + a_i_1)));

                    sign = -sign;
                    x_i *= x;
                    x_2i *= x * x;
                    a_i_1 *= a;
                }

                Console.WriteLine("При N={0:00}, J={1:00.000}", n, J);
            }

            Console.ReadKey(); //ожидание любой клавиши
        }
    }
}