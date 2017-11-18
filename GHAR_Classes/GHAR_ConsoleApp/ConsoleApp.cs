using System;
using GHAR_Classes;

namespace GHAR_ConsoleApp
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            ApplicationManipulator am = new ApplicationManipulator();

            Console.WriteLine("Hit any key to begin.");
            Console.ReadKey();
            Console.Clear();

            am.OpenNotepad();
        }
    }
}
