﻿using System;
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
        /*! 
         This function allows us to calculate the function
         of the number e by expanding it into a Taylor series.
         sum = from i=0 to n = inf calculate sum of (x^n)/(n!)

            We use 1 parameter:
                * Type = boolean, name = isRevers, default = False
            If isRevers is True than we calculate for e^(-x)
            If isRevers if False than we calculate for e^x
        */
        static double factorial(double number)
        {
            if (number == 1 || number == 0)
                return 1;
            else
                return number * factorial(number - 1);
        }
        static void runFunc(bool isRevers = false) {
            double s = 0;
            long k = 0;
            Console.Write("Enter n: ");
            n = Convert.ToInt64(Console.ReadLine());
            if (!isRevers)
            {
                for (long i = 0; i < n; i++)
                {
                    s += Math.Pow(x, k) / factorial(k);
                    k++;
                }
            }
            else {
                for (long i = 0; i < n; i++)
                {
                    if (k % 2 == 0)
                        s += (1 / factorial(k)) * Math.Pow(x, k);
                    else
                        s += (-1 / factorial(k)) * Math.Pow(x, k);
                    k++;
                }
            }
            sum.Add(s);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nSum: {s.ToString("n10")} \nReversed: {isRevers}\n");
            if (sum.Count > 1) {
                Console.WriteLine($"Difference: |{Convert.ToDouble(sum[step-1]).ToString("n10")} - {Convert.ToDouble(sum[step]).ToString("n10")}| = {Math.Abs((Convert.ToDouble(sum[step-1])- Convert.ToInt64(sum[step-1])) -(Convert.ToDouble(sum[step])- Convert.ToInt64(sum[step]))).ToString("n10")}\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
            step++;
        }
    }
}