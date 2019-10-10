using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;

namespace laba1
{
    public static partial class Taylor
    {
        static ArrayList sum = new ArrayList();
        static int step = 0;
        static long n = 0;

        static double factorial(double number)
        {
            if (number == 1 || number == 0)
                return 1;
            else
                return number * factorial(number - 1);
        }
        static double factorialF(float number)
        {
            if (number == 1 || number == 0)
                return 1;
            else
                return number * factorial(number - 1);
        }
        /*!
         This function allows us to calculate the function
         of the number e by expanding it into a Taylor series.
         Sum = from i=0 to (n->inf) calculate sum of (x^n)/(n!)
            
         Function: 
            * Name = calcTaylorSeries, Type = void
         Parameters:
            * Name = isRevers, Type = boolean, Default = False
            
         If isRevers is True than we calculate for e^(-x)
         If isRevers if False than we calculate for e^x
        */
        //TODO Work on difference calculation in function calcTaylorSeries
        static void calcTaylorSeries(bool isRevers = false)
        {
            string input = "";
            double s = 0;
            float s1 = 0;

            Console.Write("Enter n: ");
            input = Console.ReadLine();
            if (long.TryParse(input, out n))
                n = Convert.ToInt64(input);
            else
            {
                n = 100;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("~ An invalid argument\n~ Try: integer number\n~ Now will calculate with n = 100");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (!isRevers)
            {
                for (long i = 0; i < n; i++)
                {
                    s += Math.Pow(x, i) / factorial(i);
                    s1 +=(float)(Math.Pow(xF, i) / factorialF(i));
                }
            }
            else
            {
                for (long i = 0; i < n; i++)
                {
                    if (i % 2 == 0)
                    {
                        s += (1 / factorial(i)) * Math.Pow(x, i);
                        s1 += (float)((1 / factorialF(i)) * Math.Pow(xF, i));
                    }
                    else
                    {
                        s += (-1 / factorial(i)) * Math.Pow(x, i);
                        s1 += (float)((-1 / factorialF(i)) * Math.Pow(xF, i));
                    }
                }
            }
            sum.Add(s);
            sum.Add(s1);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nSum Double: {s.ToString("n10")}\nSum Float: {s1.ToString("n10")} \nReversed: {isRevers}\nN: {n}\nX: {x}");
            Console.ForegroundColor = ConsoleColor.White;
            step++;
        }
    }
}