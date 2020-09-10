using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Turing_Emulator
{
    class Program
    {
        struct CodeLine
        {
            internal string state, newState;
            internal char symbol, newSymbol;
            internal byte direction;
        }

        private const string FILENAME = @"C:\Users\sasho\Documents\TextTestLocation\TextFile1.txt";

        private const int INITIAL_TAPE_LINE = 1;
        private const int INITIAL_POSITION_LINE = 2;
        private const int EXPECTED_ARGUMENT_COUNT = 5;

        private static string _initialTape;
        private static int _initialPosition;

        //private static readonly string _firstPath = "code1.txt";
        //private static readonly string _secondPath = "code2.txt";
        //private static readonly string _thirdPath = "code3.txt";

        private static int _simulationSpeed = 1;

        private static List<CodeLine> _codeList = new List<CodeLine>();

        static void Main()
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
        
        private static void ChooseFile()
        {
            Console.Clear();
            Console.WriteLine("Currently you cannot change the default file locations and names, which are:");
            //Console.WriteLine(_firstPath);
            //Console.WriteLine(_secondPath);
            //Console.WriteLine(_thirdPath);
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
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


        private static void Simulation()
        {
            Console.Clear();
            ReadFile();

            Console.WriteLine(_initialTape);
            Console.WriteLine(_initialPosition);

            foreach (CodeLine item in _codeList)
            {
                Console.WriteLine(item.state + " "  + item.symbol + " " + item.newSymbol + " " + item.direction.ToString() + " " + item.newState);
            }

            Console.ReadKey();
        }

        private static void ReadFile()
        {
            IEnumerable<string> lines = System.IO.File.ReadLines(FILENAME);
            int lineNumber = 0;

            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty(line)) continue;
                lineNumber++;
                if (lineNumber == INITIAL_TAPE_LINE) _initialTape = line;
                else if (lineNumber == INITIAL_POSITION_LINE) _initialPosition = int.Parse(line);
                else
                {

                    string[] words = line.Split(" ");

                    if (words.Length != EXPECTED_ARGUMENT_COUNT)
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". Invalid argument number.");
                        break;
                    }

                    CodeLine myLine;

                    myLine.state = words[0];

                    if (char.TryParse(words[1], out char sym)) myLine.symbol = sym;
                    else
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". Symbol is unsupported or provided incorrecty.");
                        break;
                    }

                    if (char.TryParse(words[2], out char newSym)) myLine.newSymbol = newSym;
                    else
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". New symbol is unsupported or provided incorrecty.");
                        break;
                    }

                    if (char.TryParse(words[3], out char dir))
                    {
                        if (dir == 'L') myLine.direction = 0;
                        else if (dir == 'R') myLine.direction = 1;
                        else
                        {
                            Console.WriteLine("Syntax error at line " + lineNumber + ". Direction can be either L or R.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". Direction can be either L or R.");
                        break;
                    }

                    myLine.newState = words[4];

                    _codeList.Add(myLine);
                }
            }
        }

    }
}
