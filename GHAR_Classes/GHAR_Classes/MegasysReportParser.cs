using System;
using System.Collections.Generic;
using System.IO;

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
        public List<Event> EventsToday { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public void SetEventsByFall2017()
        {
            EventsToday = new List<Event>();

            Event tmp = new Event { Title = "Overnight", CresCode = string.Empty, MultiEventCode = "(Ov)", Time = "0:00 AM"};
            EventsToday.Add(tmp);

            tmp = new Event { Title = "Tea - 11:00 AM", CresCode = "TEAM", MultiEventCode = "(TE11a)", Time = "11:00 AM" };
            EventsToday.Add(tmp);

            tmp = new Event { Title = "Tour - 1:00 PM", CresCode = "TOUR", MultiEventCode = "(TR1p)", Time = "1:00 PM" };
            EventsToday.Add(tmp);

            tmp = new Event { Title = "Tea - 2:30 PM", CresCode = "TEPM", MultiEventCode = "(TE2.5p)", Time = "2:30 PM" };
            EventsToday.Add(tmp);
        }

        public void ReadInArrivalsReport(string path)
        {
            FileLines = new List<string>();
            OvernightGuests = new List<ReportEntry>();
            TeaGuests = new List<ReportEntry>();
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
                        else if (check == "TOUR")
                        {
                            tmpEntry.Type = GuestType.Tour;
                            tmpEntry.DisplayTime = "1:00 PM";
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
                        switch (tmpEntry.Type)
                        {
                            case GuestType.Overnight:
                                OvernightGuests.Add(tmpEntry);
                                break;
                            case GuestType.Tour:
                                TourGuests.Add(tmpEntry);
                                break;
                            default:
                                TeaGuests.Add(tmpEntry);
                                break;
                        }
                    }
                }
            }
        }

        public string ToStringForTeasAndTours()
        {
            
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
