using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GHAR_Classes
{
    public class MegasysReportParser
    {
        #region Data

        const int MaxTourSize = 25;

        #endregion

        #region Constructors



        #endregion

        #region Properties

        // Input Data
        public List<string> FileLines { get; set; }

        // Reporting Data
        public List<EventRoster> ListOfEvents { get; set; }
        public int DayPasseCount { get; set; }
        public int OvernightPassCount { get; set; }
        public List<string> OvernightPassDates { get; set; }
        public List<int> OvernightPassCounts { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public void CreateEventLists()
        {
            ListOfEvents = new List<EventRoster>();

            // Create and add the 11:00 AM Tea
            EventRoster tmp = new EventRoster
            {
                Title = "Tea - 11:00 AM",
                Type = GuestType.Tea,
                Time = "11:00 AM",
                MultiEventCode = "Te-11a",
                Reservations = new List<RosterReservation>()
            };
            ListOfEvents.Add(tmp);

            // Create and add the 1:00 PM Tour
            tmp = new EventRoster
            {
                Title = "Tour - 1:00 PM",
                Type = GuestType.Tour,
                Time = "1:00 PM",
                MultiEventCode = "Tr-1p",
                Reservations = new List<RosterReservation>()
            };
            ListOfEvents.Add(tmp);

            // Create and add the 2:30 PM Tea
            tmp = new EventRoster
            {
                Title = "Tea - 2:30 PM",
                Type = GuestType.Tea,
                Time = "2:30 PM",
                MultiEventCode = "Te-230p",
                Reservations = new List<RosterReservation>()
            };
            ListOfEvents.Add(tmp);

            // Create and add the Overnights "event"
            tmp = new EventRoster
            {
                Title = "Overnights",
                Type = GuestType.Overnight,
                Time = "0:00 AM",
                MultiEventCode = "Ov",
                Reservations = new List<RosterReservation>()
            };
            ListOfEvents.Add(tmp);
        }

        public void ReadInArrivalsReport(string path)
        {
            FileLines = new List<string>();

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
                        RosterReservation tmpRes = new RosterReservation();
                        string tmpLine = line;

                        // Strip off everything before the name
                        tmpLine = tmpLine.Remove(0, 12);

                        // Read in the name
                        tmpRes.Name = tmpLine.Substring(0, 16);

                        // Strip off everything until "Cres" (Entry.type)
                        tmpLine = tmpLine.Remove(0, 29);

                        // Read in the Type
                        string check = tmpLine.Substring(0, 4);
                        switch (check)
                        {
                            case "TEAM":
                                tmpRes.Type = GuestType.Tea;
                                tmpRes.DisplayTime = "11:00 AM";
                                tmpRes.CalculateTimeValue();
                                break;
                            case "TEPM":
                                tmpRes.Type = GuestType.Tea;
                                tmpRes.DisplayTime = "2:30 PM";
                                tmpRes.CalculateTimeValue();
                                break;
                            case "TOUR":
                                tmpRes.Type = GuestType.Tour;
                                tmpRes.DisplayTime = "1:00 PM";
                                tmpRes.CalculateTimeValue();
                                break;
                            default:
                                tmpRes.Type = GuestType.Overnight;
                                tmpRes.DisplayTime = "0:00 AM";
                                break;
                        }

                        // Strip off everything until date
                        tmpLine = tmpLine.Remove(0, 9);

                        // Read in the Date
                        tmpRes.DepartDate = tmpLine.Substring(0, 8);

                        // Strip off 41 chars
                        tmpLine = tmpLine.Remove(0, 41);

                        // Read in the Date
                        tmpRes.GuestCount = int.Parse(tmpLine.Substring(0, 2));

                        // Add in the Entry to the correct roster
                        foreach (EventRoster eventRoster in ListOfEvents)
                        {
                            // Find the correct roster
                            if (tmpRes.Type == eventRoster.Type && tmpRes.DisplayTime == eventRoster.Time)
                            {
                                bool wasFound = false;
                                // Check if the roster yet contains the reservation
                                foreach (RosterReservation res in eventRoster.Reservations)
                                {
                                    // If it is already in the roster
                                    if (tmpRes.Name == res.Name)
                                    {
                                        wasFound = true;
                                        res.EntryCount++;

                                        if (tmpRes.GuestCount > res.GuestCount)
                                        {
                                            res.GuestCount = tmpRes.GuestCount;
                                        }
                                    }
                                }
                                if (!wasFound)
                                {
                                    tmpRes.EntryCount = 1;
                                    eventRoster.Reservations.Add(tmpRes);
                                }
                            }
                        }

                        //switch (tmpEntry.Type)
                        //{
                        //    case GuestType.Overnight:
                        //        OvernightGuests.Add(tmpEntry);
                        //        break;
                        //    case GuestType.Tour:
                        //        TourGuests.Add(tmpEntry);
                        //        break;
                        //    default:
                        //        TeaGuests.Add(tmpEntry);
                        //        break;
                        //}
                    }
                }
            }
        }

        public void CalculateValues()
        {
            // Sort the data
            foreach (EventRoster roster in ListOfEvents)
            {
                roster.Reservations  = roster.Reservations.OrderBy(rosterReservation => rosterReservation.Name).ToList();
            }

            // Calculate the Overnight Passes
            EventRoster overnightRoster = null;

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (EventRoster roster in ListOfEvents)
            {
                if (roster.Type == GuestType.Overnight)
                {
                    overnightRoster = roster;
                }
            }

            OvernightPassDates = new List<string>();
            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (RosterReservation res in overnightRoster.Reservations)
            {
                if (!OvernightPassDates.Contains(res.DepartDate))
                {
                    OvernightPassDates.Add(res.DepartDate);
                }
            }

            OvernightPassDates.Sort();

            OvernightPassCounts = new List<int>();
            foreach (string date in OvernightPassDates)
            {
                OvernightPassCounts.Add(0);
            }

            OvernightPassCount = 0;
            foreach (RosterReservation res in overnightRoster.Reservations)
            {
                OvernightPassCounts[OvernightPassDates.IndexOf(res.DepartDate)] += res.EntryCount;
                OvernightPassCount += res.EntryCount;
            }

            // Calculate Day Passes
            DayPasseCount = 0;

            foreach (EventRoster roster in ListOfEvents)
            {
                if (roster.Type != GuestType.Overnight)
                {
                    foreach (RosterReservation res in roster.Reservations)
                    {
                        DayPasseCount += res.EntryCount;
                    }
                }
            }

            // Calculate reservation's "MultiEvent" codes
            foreach (EventRoster mainRoster in ListOfEvents)
            {
                foreach (RosterReservation mainRes in mainRoster.Reservations)
                {
                    mainRes.EventCodes = new List<string>();

                    foreach (EventRoster roster in ListOfEvents)
                    {
                        if (roster != mainRoster)
                        {
                            foreach (RosterReservation res in roster.Reservations)
                            {
                                if (mainRes.Name == res.Name)
                                {
                                    if (!mainRes.EventCodes.Contains(roster.MultiEventCode))
                                    {
                                        mainRes.EventCodes.Add(roster.MultiEventCode);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string ToStringForTeasAndTours()
        {
            // Local declarations
            StringBuilder sb = new StringBuilder();
            const int OffsetAfterName = 18;
            const int OffsetAfterMEventCode = 16;
            const int OffsetAfterTime = 10;
            const int OffsetAfterCount = 10;
            const int OffsetAfterType = 0;

            // Create header
            string title = $"Summed Up Report of Teas & Tours for [{DateTime.Today.Date:M/d/yy}] (Run at: {DateTime.Now:hh:mm tt})";
            sb.AppendLine(title);
            sb.AppendLine();

            string header = $"{"Name",-OffsetAfterName}{"Count",-OffsetAfterCount}{"O-Events",-OffsetAfterMEventCode}{"Time",-OffsetAfterTime}{"Type",-OffsetAfterType}";
            sb.AppendLine(header);
            sb.AppendLine();

            foreach (RosterReservation res in ListOfEvents[0].Reservations)
            {
                // Assmble "Multi Event Code"
                string mec = res.EventCodes.Count > 0 ? res.EventCodes.Aggregate(string.Empty, (current, code) => $"{current}({code})") : string.Empty;
                string followMec = mec != string.Empty ? "\"" : string.Empty;

                for (int i = 0; i < res.EntryCount; i++)
                {
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (i == 0)
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{res.GuestCount,-OffsetAfterCount}{mec,-OffsetAfterMEventCode}{res.DisplayTime,-OffsetAfterTime}{res.Type,-OffsetAfterType}");
                    }
                    else
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{"\"",-OffsetAfterCount}{followMec,-OffsetAfterMEventCode}{res.DisplayTime,-OffsetAfterTime}{res.Type,-OffsetAfterType}");
                    }
                }
            }
            sb.AppendLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - -");

            int tourTotal = 0;
            foreach (RosterReservation res in ListOfEvents[1].Reservations)
            {
                // Increment the tourTotal
                tourTotal += res.GuestCount;

                // Assmble "Multi Event Code"
                string mec = res.EventCodes.Count > 0 ? res.EventCodes.Aggregate(string.Empty, (current, code) => $"{current}({code})") : string.Empty;
                string followMec = mec != string.Empty ? "\"" : string.Empty;

                for (int i = 0; i < res.EntryCount; i++)
                {
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (i == 0)
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{res.GuestCount,-OffsetAfterCount}{mec,-OffsetAfterMEventCode}{res.DisplayTime,-OffsetAfterTime}{res.Type,-OffsetAfterType}");
                    }
                    else
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{"\"",-OffsetAfterCount}{followMec,-OffsetAfterMEventCode}{res.DisplayTime,-OffsetAfterTime}{res.Type,-OffsetAfterType}");
                    }
                }
            }
            sb.AppendLine();

            sb.AppendLine($"Total: {tourTotal}\tRemaining: {MaxTourSize - tourTotal}");
            sb.AppendLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - -");

            foreach (RosterReservation res in ListOfEvents[2].Reservations)
            {
                // Assmble "Multi Event Code"
                string mec = res.EventCodes.Count > 0 ? res.EventCodes.Aggregate(string.Empty, (current, code) => $"{current}({code})") : string.Empty;
                string followMec = mec != string.Empty ? "\"" : string.Empty;

                for (int i = 0; i < res.EntryCount; i++)
                {
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (i == 0)
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{res.GuestCount,-OffsetAfterCount}{mec,-OffsetAfterMEventCode}{res.DisplayTime,-OffsetAfterTime}{res.Type,-OffsetAfterType}");
                    }
                    else
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{"\"",-OffsetAfterCount}{followMec,-OffsetAfterMEventCode}{res.DisplayTime,-OffsetAfterTime}{res.Type,-OffsetAfterType}");
                    }
                }
            }
            sb.AppendLine();

            // Display pass count
            int extraSheets = DayPasseCount % 3 == 0 ? 0 : 1;
            sb.AppendLine($"Day Passes: {DayPasseCount}\t({DayPasseCount} = {(DayPasseCount/3) + extraSheets} Sheets)");

            return sb.ToString();
        }

        public string ToStringForOvernights()
        {
            // Local declarations
            StringBuilder sb = new StringBuilder();
            const int OffsetAfterName = 18;
            const int OffsetAfterMEventCode = 16;
            const int OffsetAfterDate = 14;
            const int OffsetAfterCount = 10;
            const int OffsetAfterType = 0;

            // Create header
            string title = $"Summed Up Report of Overnights for [{DateTime.Today.Date:M/d/yy}] (Run at: {DateTime.Now:hh:mm tt})";
            sb.AppendLine(title);
            sb.AppendLine();

            string header = $"{"Name",-OffsetAfterName}{"O-Events",-OffsetAfterMEventCode}{"Depart-Date",-OffsetAfterDate}{"Count",-OffsetAfterCount}{"Type",-OffsetAfterType}";
            sb.AppendLine(header);
            sb.AppendLine();

            foreach (RosterReservation res in ListOfEvents[3].Reservations)
            {
                // Assmble "Multi Event Code"
                string mec = res.EventCodes.Count > 0 ? res.EventCodes.Aggregate(string.Empty, (current, code) => $"{current}({code})") : string.Empty;
                string followMec = mec != string.Empty ? "\"" : string.Empty;

                for (int i = 0; i < res.EntryCount; i++)
                {
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (i == 0)
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{mec,-OffsetAfterMEventCode}{res.DepartDate,-OffsetAfterDate}{res.GuestCount,-OffsetAfterCount}{res.Type,-OffsetAfterType}");
                    }
                    else
                    {
                        sb.AppendLine($"{res.Name,-OffsetAfterName}{followMec,-OffsetAfterMEventCode}{res.DepartDate,-OffsetAfterDate}{"\"",-OffsetAfterCount}{res.Type,-OffsetAfterType}");
                    }
                }
            }

            // Print the passes
            sb.AppendLine();
            sb.AppendLine($"Overnight Passes: [Total: {OvernightPassCount}]");
            for (int i = 0; i < OvernightPassDates.Count; i++)
            {
                sb.AppendLine($"{OvernightPassDates[i].Substring(0,5)}: {OvernightPassCounts[i]}");
            }

            return sb.ToString();
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
