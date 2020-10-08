using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

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

        private const int INITIAL_TAPE_LINE = 1;
        private const int INITIAL_POSITION_LINE = 2;
        private const string INITIAL_STATE = "0";
        private const int EXPECTED_ARGUMENT_COUNT = 5;

        private static string initialTape1;
        private static string initialTape2;
        private static string initialTape3;
        private static string initialTape4;

        private static int initialPosition1;
        private static int initialPosition2;
        private static int initialPosition3;
        private static int initialPosition4;

        private static string firstPath = @"C:\Users\sasho\Documents\TextTestLocation\code1.txt";
        private static string secondPath = @"C:\Users\sasho\Documents\TextTestLocation\code2.txt";
        private static string thirdPath = @"C:\Users\sasho\Documents\TextTestLocation\code3.txt";
        private static string fourthPath = @"C:\Users\sasho\Documents\TextTestLocation\code4.txt";

        private static int simulationSpeed = 1;

        private static bool isRunning;
        private static bool menuLoop;
        private static short mode;

        private static List<CodeLine> codeList1 = new List<CodeLine>();
        private static List<CodeLine> codeList2 = new List<CodeLine>();
        private static List<CodeLine> codeList3 = new List<CodeLine>();
        private static List<CodeLine> codeList4 = new List<CodeLine>();

        private static Mutex test = new Mutex();

        static void Main()
        {
            StartupMenu();
        }

        private static void StartupMenu()
        {
            menuLoop = true;

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
                else if (item == '2') StartSimulationMenu();
                else if (item == '3') OptionsMenu();
                else if (item == '4') menuLoop = false;
                else continue;
            }
        }

        private static void StartSimulationMenu()
        {
            bool startLoop = true;

            while (startLoop)
            {

                Console.Clear();

                Console.WriteLine("1 - First file");
                Console.WriteLine("2 - Second file");
                Console.WriteLine("3 - Third file");
                Console.WriteLine("4 - Fourth file");
                Console.WriteLine("5 - All files simultaneously");
                Console.WriteLine("6 - Back");
                Console.WriteLine("Take your pick [1-6]: ");

                char item = Console.ReadKey().KeyChar;

                if (item == '1')
                {
                    InitializeMachine();
                    mode = 1;

                    if (ReadFile(firstPath, 0))
                    {
                        Thread t1 = new Thread(() => Simulation(0));
                        t1.Start();
                        t1.Join();
                    }
                    else
                    {
                        Console.ReadKey();
                        menuLoop = true;
                    }

                    startLoop = false;
                    ClearAll();
                }
                else if (item == '2')
                {
                    InitializeMachine();

                    mode = 1;
                    if (ReadFile(secondPath, 0))
                    {
                        Thread t1 = new Thread(() => Simulation(0));
                        t1.Start();
                        t1.Join();
                    }
                    else
                    {
                        Console.ReadKey();
                        menuLoop = true;
                    }

                    startLoop = false;
                    ClearAll();
                }
                else if (item == '3')
                {
                    InitializeMachine();

                    mode = 1;
                    if (ReadFile(thirdPath, 0))
                    {
                        Thread t1 = new Thread(() => Simulation(0));
                        t1.Start();
                        t1.Join();
                    }
                    else
                    {
                        Console.ReadKey();
                        menuLoop = true;
                    }

                    startLoop = false;
                    ClearAll();
                }
                else if (item == '4')
                {
                    InitializeMachine();

                    mode = 1;
                    if (ReadFile(fourthPath, 0))
                    {
                        Thread t1 = new Thread(() => Simulation(0));
                        t1.Start();
                        t1.Join();
                    }
                    else
                    {
                        Console.ReadKey();
                        menuLoop = true;
                    }

                    startLoop = false;
                    ClearAll();
                }
                else if (item == '5')
                {
                    InitializeMachine();

                    mode = 2;
                    ReadFile(firstPath, 0);
                    ReadFile(secondPath, 1);
                    ReadFile(thirdPath, 2);
                    ReadFile(fourthPath, 3);

                    if(ReadFile(firstPath, 0) && ReadFile(secondPath, 1) && ReadFile(thirdPath, 2) && ReadFile(fourthPath, 3))
                    {

                        Thread t1 = new Thread(() => Simulation(0));
                        Thread t2 = new Thread(() => Simulation(1));
                        Thread t3 = new Thread(() => Simulation(2));
                        Thread t4 = new Thread(() => Simulation(3));

                        t1.Start();
                        t2.Start();
                        t3.Start();
                        t4.Start();

                        t1.Join();
                        t2.Join();
                        t3.Join();
                        t4.Join();
                    }
                    else
                    {
                        Console.ReadKey();
                        menuLoop = true;
                    }

                    startLoop = false;
                    ClearAll();
                }
                else if (item == '6') startLoop = false;
            }
        }

        private static void ClearAll()
        {
            codeList1.Clear();
            codeList2.Clear();
            codeList3.Clear();
            codeList4.Clear();
        }

        private static void InitializeMachine()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 16);
            Console.WriteLine("Press SPACE to abort and return to main menu");
            menuLoop = false;
            isRunning = true;
        }

        private static void OptionsMenu()
        {
            bool optionsLoop = true;

            while (optionsLoop)
            {
                Console.Clear();
                Console.WriteLine("OPTIONS");
                Console.WriteLine("1 - Change simulation speed (" + simulationSpeed.ToString() + ")");
                Console.WriteLine("2 - Back");

                char item = Console.ReadKey().KeyChar;

                if (item == '1') ChangeSimultionSpeed();
                else if (item == '2') optionsLoop = false;
                else continue;
            }
        }
        
        private static void ChooseFile()
        {
            bool changePathLoop = true;
            while (changePathLoop)
            {
                Console.Clear();
                Console.WriteLine("Choose a file number to change path:");
                Console.WriteLine("1 - First file (" + firstPath + ")");
                Console.WriteLine("2 - Second file (" + secondPath + ")");
                Console.WriteLine("3 - Third file (" + thirdPath + ")");
                Console.WriteLine("4 - Fourth file (" + fourthPath + ")");
                Console.WriteLine("5 - Back");

                char item = Console.ReadKey().KeyChar;

                if (item == '1')
                {
                    ChangeFilePath(1);
                    changePathLoop = false;
                }
                else if (item == '2')
                {
                    ChangeFilePath(2);
                    changePathLoop = false;
                }
                else if (item == '3')
                {
                    ChangeFilePath(2);
                    changePathLoop = false;
                }
                else if (item == '4')
                {
                    ChangeFilePath(2);
                    changePathLoop = false;
                }
                else if (item == '5') changePathLoop = false;
                else continue;
            }
        }

        private static void ChangeFilePath(int fileNumber)
        {
            Console.Clear();
            Console.WriteLine("Enter new file path and press ENTER:");
            string path = Console.ReadLine();

            if (fileNumber == 1) firstPath = path;
            else if (fileNumber == 2) secondPath = path;
            else if (fileNumber == 3) thirdPath = path;
            else fourthPath = path;
        }

        private static void ChangeSimultionSpeed()
        {
            string entry;
            bool entryLoop = true;
            Console.Clear();
            Console.WriteLine("Enter the simulation step interval [0-10] and press ENTER.\n0 - run at full speed. Maximum interval (10) stands for 1 second.");
            Console.WriteLine("Current step interval value is " + simulationSpeed.ToString() + ".");

            while (entryLoop)
            {
                entry = Console.ReadLine();
                bool correctInput = int.TryParse(entry, out int interval);
                if (correctInput && interval >= 0 && interval <= 10)
                {
                    simulationSpeed = interval * 10;
                    break;
                }
                else Console.WriteLine("Please enter a whole number 0-10.");
            }
            
        }

        private static void Simulation(int position)
        {
            Console.CursorVisible = false;

            string currentState = INITIAL_STATE;

            char[] currentTape;
            int currentPosition;

            if (position == 0)
            {
                currentTape = initialTape1.ToCharArray();
                currentPosition = initialPosition1 - 1;
            }
            else if (position == 1)
            {
                currentTape = initialTape2.ToCharArray();
                currentPosition = initialPosition2 - 1;
            }
            else if (position == 2)
            {
                currentTape = initialTape3.ToCharArray();
                currentPosition = initialPosition3 - 1;
            }
            else
            {
                currentTape = initialTape4.ToCharArray();
                currentPosition = initialPosition4 - 1;
            }

            bool instructionFoundFlag, headError = false;

            Console.SetCursorPosition(0, 0 + position * 4);

            while (!headError && isRunning)
            {
                test.WaitOne();

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    isRunning = false;
                }

                Console.SetCursorPosition(0, 1 + position * 4);
                for (int i = 0; i < currentPosition; i++) Console.Write(' ');
                Console.WriteLine("^ ");

                instructionFoundFlag = false;

                List<CodeLine> currentList;
                if (position == 0) currentList = codeList1; 
                else if (position == 1) currentList = codeList2;
                else if (position == 2) currentList = codeList3;
                else currentList = codeList4;

                foreach (CodeLine instruction in currentList)
                {
                    if (instruction.state == currentState && instruction.symbol == currentTape[currentPosition])
                    {
                        instructionFoundFlag = true;
                        currentTape[currentPosition] = instruction.newSymbol;
                        if (instruction.direction == 0)
                        {
                            if (currentPosition == 0)
                            {
                                Console.WriteLine("Head position exceeds tape bounds. Simulation halted.");
                                headError = true;
                                break;
                            }
                            else currentPosition--;
                        }
                        else
                        {
                            if (currentPosition == currentTape.Length - 1)
                            {
                                Console.WriteLine("Head position exceeds tape bounds. Simulation halted.");
                                headError = true;
                                break;
                            }
                            else currentPosition++;
                        }
                        currentState = instruction.newState;
                        break;
                    }
                }

                if (!instructionFoundFlag)
                {
                    Console.WriteLine("No instruction found for state " + currentPosition + " and symbol " + currentTape[currentPosition] + ". Simulation halted.");
                    test.ReleaseMutex();
                    if (mode == 1) Console.ReadKey();
                    break;
                }
                else
                {
                    Console.SetCursorPosition(0, 0 + position * 4);
                    Console.WriteLine(currentTape);
                }

                if (simulationSpeed > 0) Thread.Sleep(TimeSpan.FromMilliseconds(simulationSpeed));

                test.ReleaseMutex();

            }
            menuLoop = true;
        }

        private static bool ReadFile(string file, int position)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("Failed to read, file does not exist.");
                return false;
            }

            // Reading all lines as raw string collection
            IEnumerable<string> lines = System.IO.File.ReadLines(file);
            int lineNumber = 0;

            // Processing each line
            foreach (string line in lines)
            {
                // Bypassing the empty lines
                if (String.IsNullOrEmpty(line)) continue;

                lineNumber++;

                // Reading initial tape data and head position on emulation start
                if (lineNumber == INITIAL_TAPE_LINE)
                {
                    if (position == 0) initialTape1 = line;
                    else if (position == 1) initialTape2 = line;
                    else if (position == 2) initialTape3 = line;
                    else initialTape4 = line;
                }
                else if (lineNumber == INITIAL_POSITION_LINE)
                {
                    initialPosition1 = int.Parse(line);
                    if (position == 0) initialPosition1 = int.Parse(line);
                    else if (position == 1) initialPosition2 = int.Parse(line);
                    else if (position == 2) initialPosition3 = int.Parse(line);
                    else initialPosition4 = int.Parse(line);
                }
                else
                {
                    // Splitting the line into arguments and checking if line is valid
                    string[] words = line.Split(" ");

                    if (words.Length < EXPECTED_ARGUMENT_COUNT)
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". Invalid argument number.");
                        return false;
                    }

                    // Trying to parse arguments, simulation is halted immediately if syntax error has occured
                    CodeLine instruction;

                    // State
                    instruction.state = words[0];

                    // Symbol
                    if (char.TryParse(words[1], out char sym)) instruction.symbol = sym;
                    else
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". Symbol is unsupported or provided incorrecty.");
                        return false;
                    }

                    // New symbol
                    if (char.TryParse(words[2], out char newSym)) instruction.newSymbol = newSym;
                    else
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". New symbol is unsupported or provided incorrecty.");
                        return false;
                    }

                    // Direction, either L or R being read from the file, but stored in struct as short
                    if (char.TryParse(words[3], out char dir))
                    {
                        if (dir == 'L') instruction.direction = 0;
                        else if (dir == 'R') instruction.direction = 1;
                        else
                        {
                            Console.WriteLine("Syntax error at line " + lineNumber + ". Direction can be either L or R.");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Syntax error at line " + lineNumber + ". Direction can be either L or R.");
                        return false;
                    }

                    //  New state
                    instruction.newState = words[4];

                    // Putting the struct to the collection
                    if (position == 0) codeList1.Add(instruction);
                    else if (position == 1) codeList2.Add(instruction);
                    else if (position == 2) codeList3.Add(instruction);
                    else codeList4.Add(instruction);
                }
            }
            return true;
        }
    }
}
