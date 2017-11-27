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
            ReportManager rm = new ReportManager();


            rm.ReadInBothReports(@"D:\ToursReport7.txt", @"D:\OtherArrivalsReport2.txt");
            rm.CalculateValues();
            
            Console.WriteLine(rm.ToStringForTeasAndTours());
            Console.WriteLine("\n\n\n");
            Console.WriteLine();
            Console.WriteLine(rm.ToStringForOvernights());

            //am.TestOnNotepad();
            //Console.WriteLine(am.RunMagasysArrivalsReport($"{DateTime.Today:d}", $"{DateTime.Today:d}"));

            //Console.Write("Enter Date: ");
            //string date = Console.ReadLine();

            //am.RunMagasysArrivalsReport(date, date);

            //reportParser.ListOfEvents = reportParser.CreateEventLists();
            //reportParser.ReadInArrivalsReport(@"I:\AutoReport_[RptOf11.26.2017]_[CrtOn11-19-17--10.32.55PM].txt");
            //reportParser.CalculateValues();

            //Console.WriteLine(reportParser.ToStringForTeasAndTours());
            //Console.WriteLine("____________________________________________________________________");
            //Console.WriteLine();
            //Console.WriteLine(reportParser.ToStringForOvernights());


            Console.WriteLine();
            Console.WriteLine("Hit any key to close.");
            Console.ReadKey();
        }
    }
}
