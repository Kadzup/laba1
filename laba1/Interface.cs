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
        public static float xF;
        public static bool isRunning = false;
        public static string title = "Taylor series";
        public static string author = "Dima Stefurak";
        public static string group = "312";

        /*!
            This part of code can be used as
            navigation menu in console mode.
            
            Functions:
                * Name = init, Type = void,
                  Description: function init main statements and console param.
                * Name = menu, Type = void,
                  Description: function call main menu elements
                * Name = input, Type = void,
                  Description: function allows us to read custom console commands.
                  All commands described in this method.
                * Name = runProg, Type = void,
                  Description: function allows us to run our console program.
                  It's calling menu() function and set global var isRunning to True statement.
            
            Variables:
                * Name = isRunning, Type = boolean, Default = faLse;
                * Name = title, Type = string, Default = "";
                * Name = author, Type = string, Default = "";
                
            Console commands:
                * Name = Menu, Commands: m, M, menu, Menu;
                * Name = Clear, Commands: clear, Clear, clr, cls;
                * Name = Exit, Commands: e, E, exit, Exit;
                * Name = Help, Commands: /h, /H, /help, /Help.
            
            Warnings:
                If you want to add your own commands,
                just change them in input() method.
                
                To add your commands, or change iteration in menu list,
                go to menu() method, and find output line with allowed commands.

            Got a questions:
                Instagram: @kadzup
                Email: dimonstefurak@gmail.com
                GitHub: kadzup
        */
        static void init() {
            x = 0;
            xF = 0;
            sum.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = title + ". " + author + " #" + group;
            Console.Clear();
        }
        public static bool isHelp(string arg)
        {
            string[] words = arg.Split();
            if (words[0] == "/help" || words[0] == "/Help" || words[0] == "/H" || words[0] == "/h")
                return true;
            return false;
        }
        //TODO create help dictionary
        public static void callHelp(string arg)
        {
            string[] helpArg;
            if (arg == "/help" || arg == "/Help" || arg == "/H" || arg == "/h")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Only help");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                helpArg = arg.Split();
                if (helpArg[1] == "menu" || helpArg[1] == "Menu" || helpArg[1] == "m" || helpArg[1] == "M")
                {
                    Console.WriteLine("Menu help");
                }
                else if (helpArg[1] == "clear" || helpArg[1] == "Clear" || helpArg[1] == "m" || helpArg[1] == "M")
                {
                    Console.WriteLine("Clear help");
                }
                else if (helpArg[1] == "exit" || helpArg[1] == "Exit" || helpArg[1] == "e" || helpArg[1] == "E")
                {
                    Console.WriteLine("Exit help");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError, can't find {helpArg[1]} in Help dictionary\nMaybe you should try:\n" +
                        "/help Menu\n" +
                        "/help Clear\n" +
                        "/help Exit\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        static void menu() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Main menu:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(  "1) Random\n"+
                                "2) Input\n"+
                                "3) Output\n"+
                                "4) Start\n"+
                                "5) Exit\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nAlso you can type /help, in console mode\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void random()
        {
            x = 0;
            var rand = new Random();
            x = rand.NextDouble() + rand.Next(50);
            xF = (float)x;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"X: {x}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void read() {
            string str = "";
            Console.Write("Enter x: ");
            str = Console.ReadLine();
            x = Convert.ToDouble(str);
            xF = (float)x;
        }
        static void input() {
            string arg = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">>> ");
            arg = Console.ReadLine();
            if (isHelp(arg))
            {
                callHelp(arg);
            }
            else if (arg == "1" || arg == "random" || arg == "rand" || arg == "r" || arg == "R")
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
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("~ An invalid argument\n~ Try: True or False\n~ Now will calculate with No Reversed x");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                calcTaylorSeries(rev);
            }
            else if (arg == "5" || arg == "exit" || arg == "Exit" || arg == "e" || arg == "E")
            {
                isRunning = false;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Thank you, see you again next time!\n\nCreated by {author}, 2019");
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
            else if (arg == "help" || arg == "Help" || arg == "h" || arg == "H" || arg == "/help" || arg == "/Help" || arg == "/H" || arg == "/h") {
                if (arg == "help" || arg == "Help" || arg == "h" || arg == "H") {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"~ Incorrect argument [{arg}]\n~ Try: /h, /H, /help, /Help");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"~ Invalid argument [{arg}]\n~ Try: m, M, menu, Menu");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static void runProg() {
            isRunning = true;
            menu();
            while (isRunning) {
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
            init();
            runProg();
        }
    }
}
