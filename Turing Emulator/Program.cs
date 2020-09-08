using System;

namespace Turing_Emulator
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupMenu();
        }

        private static void StartupMenu()
        {
            bool MenuLoop = true;

            while (MenuLoop) {
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
                else if (item == '4') MenuLoop = false;
                else continue;
            }
        }

        private static void OptionsMenu()
        {
            Console.WriteLine("Options");
            Console.ReadKey();
        }

        private static void Simulation()
        {
            Console.WriteLine("Sumulation");
            Console.ReadKey();
        }

        private static void ChooseFile()
        {
            Console.WriteLine("Choose file");
            Console.ReadKey();
        }
    }
}
