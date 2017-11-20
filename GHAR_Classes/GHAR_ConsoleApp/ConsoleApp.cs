using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GHAR_Classes;

namespace GHAR_ConsoleApp
{
    internal class ConsoleApp
    {
        static void Main(string[] args)
        {
            const int MaxTourCount = 25;

            ApplicationManipulator am = new ApplicationManipulator();
            MegasysReportParser reportParser = new MegasysReportParser();

            //am.TestOnNotepad();

            //Console.Write("Enter Date: ");
            //string date = Console.ReadLine();

            //am.RunMagasysArrivalsReport(date, date);

            reportParser.ReadInArrivalsReport(@"D:\AutoReport_[RptOf11.25.2017]_[CrtOn11-19-17--10.32.25PM].txt");
            //reportParser.ReadInToursReport(@"I:\testTours.txt");

            List<ReportEntry> toursAndTeas = reportParser.TourGuests.ToList();
            toursAndTeas.AddRange(reportParser.TeaGuests);

            const int O1 = 22;
            const int O2 = 12;
            const int O3 = 6;
            const int O4 = 14;

            reportParser.TeaGuests = reportParser.TeaGuests.OrderBy(reportEntry => reportEntry.Name).ToList();
            reportParser.TeaGuests = reportParser.TeaGuests.OrderBy(reportEntry => reportEntry.DisplayTime).ToList();
            reportParser.TourGuests = reportParser.TourGuests.OrderBy(reportEntry => reportEntry.Name).ToList();
            reportParser.OvernightGuests = reportParser.OvernightGuests.OrderBy(reportEntry => reportEntry.Name).ToList();


            Console.WriteLine($"{"Name",-O1}{"Time",-O2}{"Count",-O3}{"Type",-O4}");
            Console.WriteLine();

            toursAndTeas = toursAndTeas.OrderBy(reportEntry => reportEntry.Type).ToList();
            toursAndTeas = toursAndTeas.OrderBy(reportEntry => reportEntry.Name).ToList();
            toursAndTeas = toursAndTeas.OrderBy(reportEntry => reportEntry.TimeValue).ToList();

            GuestType? prevType = null;
            string prevTime = null;
            bool firstRun = true;
            int count = 0;
            foreach (ReportEntry entry in toursAndTeas)
            {
                // Break a line if needed
                if ((entry.Type != prevType || entry.DisplayTime != prevTime) && firstRun == false)
                {
                    if (prevType == GuestType.Tour && entry.Type != GuestType.Tour)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Total: {count}\t\tRemaining: {MaxTourCount - count}");
                    }

                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - -");
                    count = 0;
                }
                firstRun = false;

                Console.WriteLine($"{entry.Name,-O1}{entry.DisplayTime,-O2}{entry.GuestCount,-O3}{entry.Type,-O4}");
                prevType = entry.Type;
                prevTime = entry.DisplayTime;
                count += entry.GuestCount;
            }

            // Count up Day Passes
            Console.WriteLine();
            int extraSheets = toursAndTeas.Count % 3 == 0 ? 0 : 1;

            Console.WriteLine($"Total Day Passes: {toursAndTeas.Count}\t({toursAndTeas.Count} = {(toursAndTeas.Count / 3) + extraSheets} sheets)");

            Console.WriteLine("______________________________________________________________");
            Console.WriteLine();

            Console.WriteLine($"{"Name",-O1}{"Time",-O2}{"Count",-O3}{"Type",-O4}Departure");
            Console.WriteLine();

            foreach (ReportEntry entry in reportParser.OvernightGuests)
            {
                Console.WriteLine($"{entry.Name,-O1}{entry.DisplayTime,-O2}{entry.GuestCount,-O3}{entry.Type,-O4}{entry.DepartDate}");
            }
            Console.WriteLine();

            Console.WriteLine($"Total Overnight: {reportParser.OvernightGuests.Count}");

            // Find what departure dates are present
            List<string> overnightPasses = new List<string>();
            foreach (ReportEntry entry in reportParser.OvernightGuests)
            {
                if (!overnightPasses.Contains(entry.DepartDate))
                {
                    overnightPasses.Add(entry.DepartDate);
                }
            }

            overnightPasses.Sort();

            foreach (string date in overnightPasses)
            {
                count = 0;
                foreach (ReportEntry entry in reportParser.OvernightGuests)
                {
                    if (entry.DepartDate == date)
                    {
                        count++;
                    }
                }
                Console.WriteLine($"{date.Substring(0, 5)}  =  {count} passes");
            }

            Console.WriteLine();
            Console.WriteLine("Hit any key to close.");
            Console.ReadKey();
        }
    }
}
