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
            ReportManager reportParser = new ReportManager();

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


            //Console.WriteLine();
            //Console.WriteLine("Hit any key to close.");
            //Console.ReadKey();
        }
    }
}
