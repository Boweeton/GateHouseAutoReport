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

            // am.OpenNotepad();

            reportParser.ReadInArrivalsReport(@"I:\testArrivals.txt");
            reportParser.ReadInToursReport(@"I:\testTours.txt");

            List<ReportEntry> toursAndTeas = reportParser.TourGuests.ToList();
            toursAndTeas.AddRange(reportParser.TeaGuests);

            const int O1 = 22;
            const int O2 = 14;
            const int O3 = 14;

            reportParser.TeaGuests = reportParser.TeaGuests.OrderBy(reportEntry => reportEntry.Name).ToList();
            reportParser.TeaGuests = reportParser.TeaGuests.OrderBy(reportEntry => reportEntry.DisplayTime).ToList();
            reportParser.TourGuests = reportParser.TourGuests.OrderBy(reportEntry => reportEntry.Name).ToList();
            reportParser.OvernightGuests = reportParser.OvernightGuests.OrderBy(reportEntry => reportEntry.Name).ToList();


            Console.WriteLine($"{"Name",-O1}{"Time",-O2}{"Type",-O3}Departure");
            Console.WriteLine();

            toursAndTeas = toursAndTeas.OrderBy(reportEntry => reportEntry.Type).ToList();
            toursAndTeas = toursAndTeas.OrderBy(reportEntry => reportEntry.Name).ToList();
            toursAndTeas = toursAndTeas.OrderBy(reportEntry => reportEntry.TimeValue).ToList();

            GuestType? prevType = null;
            string prevTime = null;
            foreach (ReportEntry entry in toursAndTeas)
            {
                // Break a line if needed
                if (entry.Type != prevType || entry.DisplayTime != prevTime)
                {
                    Console.WriteLine();
                }

                Console.WriteLine($"{entry.Name, -O1}{entry.DisplayTime,-O2}{entry.Type,-O3}{entry.DepartDate}");
                prevType = entry.Type;
                prevTime = entry.DisplayTime;
            }
            Console.WriteLine();

            foreach (ReportEntry entry in reportParser.OvernightGuests)
            {
                Console.WriteLine($"{entry.Name,-O1}{entry.DisplayTime,-O2}{entry.Type,-O3}{entry.DepartDate}");
            }
            Console.WriteLine();

            Console.WriteLine("Hit any key to close.");
            Console.ReadKey();
        }
    }
}
