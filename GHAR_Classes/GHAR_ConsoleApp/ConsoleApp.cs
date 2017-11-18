using System;
using System.Collections.Generic;
using System.Linq;
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
            //reportParser.ReadInToursReport(@"D:\testTours.txt");

            const int o1 = 22;
            const int o2 = 14;
            const int o3 = 14;

            reportParser.TeaGuests = new List<ReportEntry>(reportParser.TeaGuests.OrderBy(reportEntry => reportEntry.Name));
            reportParser.TeaGuests = new List<ReportEntry>(reportParser.TeaGuests.OrderBy(reportEntry => reportEntry.DisplayTime));
            reportParser.OvernightGuests = new List<ReportEntry>(reportParser.OvernightGuests.OrderBy(reportEntry => reportEntry.Name));


            Console.WriteLine($"{"Name",-o1}{"Time",-o2}{"Type",-o3}Departure");
            Console.WriteLine();
            foreach (ReportEntry entry in reportParser.TeaGuests)
            {
                Console.WriteLine($"{entry.Name, -o1}{entry.DisplayTime,-o2}{entry.Type,-o3}{entry.DepartDate}");
            }
            Console.WriteLine();
            foreach (ReportEntry entry in reportParser.OvernightGuests)
            {
                Console.WriteLine($"{entry.Name,-o1}{entry.DisplayTime,-o2}{entry.Type,-o3}{entry.DepartDate}");
            }

            Console.WriteLine("Hit any key to close.");
            Console.ReadKey();
        }
    }
}
