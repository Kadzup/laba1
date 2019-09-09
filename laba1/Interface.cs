using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;

/*
  Обчислити x e− , 0 x , розклавши функцію
  у ряд Тейлора і використовуючи формулу x 
  x ee / 1= − , де x e також обчислюється 
  за розкладом у ряд Тейлора. 
  Обчислення здійснити для різних типів даних
  із простою та подвійною точністю, наприклад,
  на мові C – float і double. 
  Для кожного типу даних порівняти одержані 
  різними способами значення x e− 
  для різної кількості доданків, 
  наприклад, 5, 10, 15, 25 із “машинним”
  значенням x e− , безпосередньо обчисленим 
  у програмі. 
  Зробити висновки. 
  У наближеному значенні  8459052.71828182 
  числа e всі цифри правильні.
  Варіанти значень х: 0,5; 1,5. 
*/

namespace laba1
{
    public static partial class Taylor
    {
        public static double x; 
        public static bool running = false;
        public static string title = "Taylor series";
        static void menu() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Main menu:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1) Random\n2) Input\n3) Output\n4) Start\n5) Exit\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void random()
        {
            x = 0;
            var rand = new Random();
            x = rand.NextDouble() + rand.Next(100);
        }
        static void read() {
            string str = "";
            Console.Write("Enter x: ");
            str = Console.ReadLine();
            x = Convert.ToDouble(str);
        }
        static void input() {
            string arg = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">>> ");
            arg = Console.ReadLine();

            if (arg == "1" || arg == "random" || arg == "rand" || arg == "r" || arg == "R")
            {
                random();
            }
            else if (arg == "2" || arg == "input" || arg == "Input" || arg == "i" || arg == "I")
            {
                read();
            }
            else if (arg == "3" || arg == "output" || arg == "Output" || arg == "o" || arg == "O")
            {
                output();
            }
            else if (arg == "4" || arg == "start" || arg == "Start" || arg == "s" || arg == "S")
            {
                bool rev = false;
                string str = "";
                Console.Write("Is function reversed? - ");
                str = Console.ReadLine();
                if (str == "true" || str == "True" || str == "yes" || str == "Yes" || str == "y" || str == "Y" || str == "1" || str == "t" || str == "T")
                {
                    rev = true;
                }
                else if (str == "false" || str == "False" || str == "no" || str == "No" || str == "n" || str == "N" || str == "0" || str == "f" || str == "F")
                {
                    rev = false;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("~ An invalid argument\n~ Try: True or False\n~ Now will calculate with No Reversed x");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                calcTaylorSeries(rev);
            }
            else if (arg == "5" || arg == "exit" || arg == "Exit" || arg == "e" || arg == "E")
            {
                running = false;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Thank you, see you again next time!\n\nCreated by Dima Stefurak, 2019");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (arg == "menu" || arg == "Menu" || arg == "m" || arg == "M")
            {
                Console.Clear();
                menu();
            }
            else if (arg == "clear" || arg == "clr" || arg == "cls" || arg == "Clear")
            {
                Console.Clear();
            }
            else if (arg == "reset" || arg == "res" || arg == "Reset")
            {
                sum.Clear();
                step = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"~ Sum results is reseted");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"~ Invalid argument [{arg}]\n~ Try: m, M, menu, Menu");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void runProg() {
            running = true;
            menu();
            while (running) {
                input();
            }
        }
        static void output() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"X: {x}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            long k = 0;
            foreach (var i in sum) {
                Console.Write("[" + Convert.ToDouble(i).ToString("n10")+"] ");
                k++;
                if (k == 5)
                {
                    Console.WriteLine();
                    k = 0;
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"X: {x}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {
            Console.Title = title;
            runProg();
        }
    }
}
