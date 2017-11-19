using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHAR_Classes
{
    public class MegasysReportParser
    {
        #region Data



        #endregion

        #region Constructors



        #endregion

        #region Properties

        // Input Data
        public List<string> FileLines { get; set; }

        // Reporting Data
        public List<ReportEntry> OvernightGuests { get; set; }
        public List<ReportEntry> TourGuests { get; set; }
        public List<ReportEntry> TeaGuests { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public void ReadInArrivalsReport(string path)
        {
            FileLines = new List<string>();
            OvernightGuests = new List<ReportEntry>();
            TeaGuests = new List<ReportEntry>();

            StreamReader sr = new StreamReader(path);

            // Read in the raw lines
            while (!sr.EndOfStream)
            {
                FileLines.Add(sr.ReadLine());
            }

            // Process the raw lines into ReportEntry objects
            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (string line in FileLines)
            {
                if (line.Length >= 59)
                {
                    string checkString = line.Remove(0, 49);
                    checkString = checkString.Substring(0, 9);

                    // If a line is found with a formatted date in index 50
                    if (IsCorrectDateFormat(checkString))
                    {
                        ReportEntry tmpEntry = new ReportEntry();
                        string tmpLine = line;

                        // Strip off everything before the name
                        tmpLine = tmpLine.Remove(0, 12);

                        // Read in the name
                        tmpEntry.Name = tmpLine.Substring(0, 16);

                        // Strip off everything until "Cres" (Entry.type)
                        tmpLine = tmpLine.Remove(0, 29);

                        // Read in the Type
                        string check = tmpLine.Substring(0, 4);
                        if (check == "TEAM")
                        {
                            tmpEntry.Type = GuestType.Tea;
                            tmpEntry.DisplayTime = "11:00 AM";
                            tmpEntry.CalculateTimeValue();
                        }
                        else if (check == "TEPM")
                        {
                            tmpEntry.Type = GuestType.Tea;
                            tmpEntry.DisplayTime = "2:30 PM";
                            tmpEntry.CalculateTimeValue();
                        }
                        else
                        {
                            tmpEntry.Type = GuestType.Overnight;
                            tmpEntry.DisplayTime = "-";
                        }

                        // Strip off everything until date
                        tmpLine = tmpLine.Remove(0, 9);

                        // Read in the Date
                        tmpEntry.DepartDate = tmpLine.Substring(0, 8);

                        // Strip off 41 chars
                        tmpLine = tmpLine.Remove(0, 41);

                        // Read in the Date
                        tmpEntry.GuestCount = int.Parse(tmpLine.Substring(0, 2));

                        // Add in the Entry
                        if (tmpEntry.Type == GuestType.Overnight)
                        {
                            OvernightGuests.Add(tmpEntry);
                        }
                        else
                        {
                            TeaGuests.Add(tmpEntry);
                        }
                    }
                }
            }
        }

        public void ReadInToursReport(string path)
        {
            FileLines = new List<string>();
            TourGuests = new List<ReportEntry>();

            StreamReader sr = new StreamReader(path);

            // Read in the raw lines
            while (!sr.EndOfStream)
            {
                FileLines.Add(sr.ReadLine());
            }

            // Process the raw lines into ReportEntry objects
            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (string line in FileLines)
            {
                if (line.Length > 50)
                {
                    string checkString = line.Substring(0, 9);

                    // If a line is found with a formatted date in index 0
                    if (IsCorrectDateFormat(checkString))
                    {
                        ReportEntry tmpEntry = new ReportEntry();
                        string tmpLine = line;

                        // Read out the date
                        tmpEntry.DepartDate = tmpLine.Substring(0, 9);

                        // Strip off 11 chars
                        tmpLine = tmpLine.Remove(0, 11);

                        // Read the Time (and add an 'M' onto the end)
                        tmpEntry.DisplayTime = $"{tmpLine.Substring(0, 4)} {tmpLine.Substring(4, 1)}M";
                        tmpEntry.CalculateTimeValue();

                        // Read in the Type
                        tmpEntry.Type = GuestType.Tour;

                        // Strip off 25 chars
                        tmpLine = tmpLine.Remove(0, 25);

                        // Read in the Name
                        tmpEntry.Name = tmpLine.Substring(0, 20);

                        // Strip off 20 chars
                        tmpLine = tmpLine.Remove(0, 20);

                        // Read in the Guest Count
                        tmpEntry.GuestCount = int.Parse(tmpLine.Substring(0, 2));

                        // Store the entry
                        TourGuests.Add(tmpEntry);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

            bool IsCorrectDateFormat(string input)
            {
                return DateTime.TryParse(input, out DateTime dateValue);
            }

        #endregion

        #endregion
    }
}
