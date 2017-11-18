using System;
using GHAR_Classes;

namespace GHAR_ConsoleApp
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            ApplicationManipulator am = new ApplicationManipulator();
            MegasysReportParser reportParser = new MegasysReportParser();

            Console.WriteLine("Hit any key to begin.");
            Console.ReadKey();
            Console.Clear();

            // am.OpenNotepad();

            reportParser.ReadInArrivalsReport(@"D:\testArrivals.txt");
            Console.WriteLine($"{"Name",-22}{"Type",-10}Departure");
            foreach (ReportEntry entry in reportParser.TeaGuests)
            {
                Console.WriteLine($"{entry.Name, -22}{entry.Type,-10}{entry.DepartDate}");
            }
            foreach (ReportEntry entry in reportParser.OvernightGuests)
            {
                Console.WriteLine($"{entry.Name,-22}{entry.Type,-10}{entry.DepartDate}");
            }

            Console.WriteLine("Hit any key to close.");
            Console.ReadKey();
        }
    }
}
