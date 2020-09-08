using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Turing_Emulator
{
    class Program
    {
        private static readonly string _firstPath = "code1.txt";
        private static readonly string _secondPath = "code2.txt";
        private static readonly string _thirdPath = "code3.txt";

        private static int _simulationSpeed = 1;

        private static List<EntryLine> _lines = new List<EntryLine>();

        static void Main(string[] args)
        {
            StartupMenu();
        }

        private static void StartupMenu()
        {
            bool menuLoop = true;

            while (menuLoop) {
                Console.Clear();

                Console.WriteLine("TURING MACHINE SIMULATOR");
                Console.WriteLine("1 - Choose code file");
                Console.WriteLine("2 - Start simulation");
                Console.WriteLine("3 - Options");
                Console.WriteLine("4 - Exit");
                Console.WriteLine("Take your pick [1-4]: ");

                char item = Console.ReadKey().KeyChar;

                if (item == '1') ChooseFile();
                else if (item == '2') Simulation();
                else if (item == '3') OptionsMenu();
                else if (item == '4') menuLoop = false;
                else continue;
            }
        }

        private static void OptionsMenu()
        {
            bool optionsLoop = true;

            while (optionsLoop)
            {
                Console.Clear();
                Console.WriteLine("OPTIONS");
                Console.WriteLine("1 - Change simulation speed (" + _simulationSpeed.ToString() + ")");
                Console.WriteLine("2 - Back");

                char item = Console.ReadKey().KeyChar;

                if (item == '1') ChangeSimultionSpeed();
                else if (item == '2') optionsLoop = false;
                else continue;
            }
        }

        private static void ChangeSimultionSpeed()
        {
            string entry;
            bool entryLoop = true;
            Console.Clear();
            Console.WriteLine("Enter the simulation step interval [0-10] and press ENTER.\n0 - run at full speed. Maximum interval (10) stands for 1 second.");
            Console.WriteLine("Current step interval value is " + _simulationSpeed.ToString() + ".");

            while (entryLoop)
            {
                entry = Console.ReadLine();
                bool correctInput = int.TryParse(entry, out int interval);
                if (correctInput && interval >= 0 && interval <= 10)
                {
                    _simulationSpeed = interval;
                    break;
                }
                else Console.WriteLine("Please enter a whole number 0-10.");
            }
            
        }

        struct EntryLine
        {
            internal bool direction;
            internal string state, newState;
            internal char symbol, newSymbol;
        }

        private static void Simulation()
        {
            
        }

        private static void ChooseFile()
        {
            Console.Clear();
            Console.WriteLine("Currently you cannot change the default file locations and names, which are:");
            Console.WriteLine(_firstPath);
            Console.WriteLine(_secondPath);
            Console.WriteLine(_thirdPath);
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
