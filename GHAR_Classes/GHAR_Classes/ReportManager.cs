﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace GHAR_Classes
{
    public class ReportManager
    {
        #region Data

        // Local Data
        const int NameLength = 16;
        const int EndOfNameIndex = 28;
        const int MaxTourSize = 25;
        List<RosterReservation> lastRun = new List<RosterReservation>();
        List<EventRoster> oldList; // old

        #endregion

        #region Constructors



        #endregion

        #region Properties

        // Reporting Data
        public int DayPasseCount { get; set; }
        public int OvernightPassCount { get; set; }

        // Data
        public string[] FileLines { get; set; }
        public bool ChangedAtLastRun { get; set; }
        public string ToursReportPath { get; set; }
        public string OtherArrivalsReportPath { get; set; }
        public List<RosterReservation> AllReservations { get; set; } = new List<RosterReservation>();
        public List<OvPassRecord> OvPassRecords { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public bool ReadInBothReports(string toursReportPath, string otherArrivalsReportPath)
        {
            // Clear out AllReservations list
            AllReservations.Clear();

            // Read in the tours report
            if (!File.Exists(toursReportPath))
            {
                return false;
            }

            // Read in all the raw lines of the file
            FileLines = File.ReadAllLines(toursReportPath);

            // Loop through each line
            RosterReservation tmp;
            foreach (string line in FileLines)
            {
                // Split up the line by space
                string[] splitLines = line.Split(' ');
                splitLines = splitLines.Where(str => !string.IsNullOrEmpty(str)).ToArray();

                // If the line is a valid entry (and not just the other lines in the report)
                if (IsEntryLine(splitLines, 0))
                {
                    // Clear out tmp
                    tmp = new RosterReservation();

                    // Read out the DepartDate
                    tmp.DepartDate = splitLines[0];

                    // Parse up the "time" element to match expected formatting
                    tmp.DisplayTime = $"{splitLines[1].Substring(0, splitLines[1].Length - 1)} {splitLines[1][splitLines[1].Length - 1]}M";
                    tmp.CalculateTimeValue();

                    // Read out the Evant Code
                    switch (splitLines[2])
                    {
                        case "CONCER":
                            tmp.Type = GuestType.Concert;
                            break;
                        case "DINNER":
                            tmp.Type = GuestType.Dinner;
                            break;
                        case "TOUR":
                            tmp.Type = GuestType.Tour;
                            break;
                        default:
                            tmp.Type = GuestType.Tour;
                            break;
                    }

                    // Read out the name and time
                    int index = 4;
                    int ppl;
                    while (!int.TryParse(splitLines[index], out ppl))
                    {
                        tmp.Name += $" {splitLines[index]}";
                        index++;
                    }

                    // Also store the GuestCount
                    tmp.GuestCount = ppl;

                    // Get back to storing the name
                    tmp.Name = tmp.Name.Trim();
                    if (tmp.Name.Length > NameLength)
                    {
                        tmp.Name = tmp.Name.Substring(0, NameLength);
                    }

                    // Add or increment to the list
                    AddOrIncrement(tmp, AllReservations);
                }
            }

            // Read in the tours report
            if (!File.Exists(otherArrivalsReportPath))
            {
                return false;
            }

            // Read in all the raw lines of the file
            FileLines = File.ReadAllLines(otherArrivalsReportPath);

            // Loop through the lines
            foreach (string line in FileLines)
            {
                if (line.Length >= EndOfNameIndex)
                {
                    string[] part2Split = line.Substring(EndOfNameIndex).Split(' ');
                    part2Split = part2Split.Where(str => !string.IsNullOrEmpty(str)).ToArray();

                    if (IsEntryLine(part2Split, 3))
                    {
                        // Clear out tmp
                        tmp = new RosterReservation();

                        // Read out the name
                        tmp.Name = line.Substring(12, 16).Trim();

                        // Read out the Cres Code to set type and calculate time
                        switch (part2Split[2])
                        {
                            case "TOUR":
                                continue;
                            case "TEAM":
                                tmp.Type = GuestType.Tea;
                                tmp.DisplayTime = "11:00 AM";
                                break;
                            case "TEPM":
                                tmp.Type = GuestType.Tea;
                                tmp.DisplayTime = "2:30 PM";
                                break;
                            default:
                                tmp.Type = GuestType.Overnight;
                                tmp.DisplayTime = "0:00 AM";
                                break;
                        }
                        tmp.CalculateTimeValue();

                        // Read out the date
                        tmp.DepartDate = part2Split[3];

                        // Read out the ppl
                        int index = 4;
                        int ppl;
                        while (!int.TryParse(part2Split[index], out ppl))
                        {
                            index++;
                        }
                        tmp.GuestCount = ppl;

                        // Add or increment to the list
                        AddOrIncrement(tmp, AllReservations);
                    }
                }
            }

            // See if the list has changed since the last run
            ChangedAtLastRun = lastRun.Count != AllReservations.Count;
            lastRun = AllReservations;

            // If everything was parsed and found correctly
            return true;
        }

        //public void ReadInArrivalsReport(string path)
        //{
        //    FileLines = new List<string>();

        //    StreamReader sr = new StreamReader(path);

        //    // Read in the raw lines
        //    while (!sr.EndOfStream)
        //    {
        //        FileLines.Add(sr.ReadLine());
        //    }

        //    // Process the raw lines into ReportEntry objects
        //    // ReSharper disable once LoopCanBePartlyConvertedToQuery
        //    foreach (string line in FileLines)
        //    {
        //        if (line.Length >= 59)
        //        {
        //            string checkString = line.Remove(0, 49);
        //            checkString = checkString.Substring(0, 9);

        //            // If a line is found with a formatted date in index 50
        //            if (IsCorrectDateFormat(checkString))
        //            {
        //                RosterReservation tmpRes = new RosterReservation();
        //                string tmpLine = line;

        //                // Strip off everything before the name
        //                tmpLine = tmpLine.Remove(0, 12);

        //                // Read in the name
        //                tmpRes.Name = tmpLine.Substring(0, 16);

        //                // Strip off everything until "Cres" (Entry.type)
        //                tmpLine = tmpLine.Remove(0, 29);

        //                // Read in the Type
        //                string check = tmpLine.Substring(0, 4);
        //                switch (check)
        //                {
        //                    case "TEAM":
        //                        tmpRes.Type = GuestType.Tea;
        //                        tmpRes.DisplayTime = "11:00 AM";
        //                        tmpRes.CalculateTimeValue();
        //                        break;
        //                    case "TEPM":
        //                        tmpRes.Type = GuestType.Tea;
        //                        tmpRes.DisplayTime = "2:30 PM";
        //                        tmpRes.CalculateTimeValue();
        //                        break;
        //                    case "TOUR":
        //                        tmpRes.Type = GuestType.Tour;
        //                        tmpRes.DisplayTime = "1:00 PM";
        //                        tmpRes.CalculateTimeValue();
        //                        break;
        //                    default:
        //                        tmpRes.Type = GuestType.Overnight;
        //                        tmpRes.DisplayTime = "0:00 AM";
        //                        break;
        //                }

        //                // Strip off everything until date
        //                tmpLine = tmpLine.Remove(0, 9);

        //                // Read in the Date
        //                tmpRes.DepartDate = tmpLine.Substring(0, 8);

        //                // Strip off 41 chars
        //                tmpLine = tmpLine.Remove(0, 41);

        //                // Read in the Date
        //                tmpRes.GuestCount = int.Parse(tmpLine.Substring(0, 2));

        //                // Add in the Entry to the correct roster
        //                foreach (EventRoster eventRoster in ListOfEvents)
        //                {
        //                    // Find the correct roster
        //                    if (tmpRes.Type == eventRoster.Type && tmpRes.DisplayTime == eventRoster.Time)
        //                    {
        //                        bool wasFound = false;
        //                        // Check if the roster yet contains the reservation
        //                        foreach (RosterReservation res in eventRoster.Reservations)
        //                        {
        //                            // If it is already in the roster
        //                            if (tmpRes.Name == res.Name)
        //                            {
        //                                wasFound = true;
        //                                res.EntryCount++;

        //                                if (tmpRes.GuestCount > res.GuestCount)
        //                                {
        //                                    res.GuestCount = tmpRes.GuestCount;
        //                                }
        //                            }
        //                        }
        //                        if (!wasFound)
        //                        {
        //                            tmpRes.EntryCount = 1;
        //                            eventRoster.Reservations.Add(tmpRes);
        //                        }
        //                    }
        //                }

        //                //switch (tmpEntry.Type)
        //                //{
        //                //    case GuestType.Overnight:
        //                //        OvernightGuests.Add(tmpEntry);
        //                //        break;
        //                //    case GuestType.Tour:
        //                //        TourGuests.Add(tmpEntry);
        //                //        break;
        //                //    default:
        //                //        TeaGuests.Add(tmpEntry);
        //                //        break;
        //                //}
        //            }
        //        }
        //    }

        //    // Check to see if the new read in list is differant
        //    if (oldList != null)
        //    {
        //        ChangedAtLastRun = ListsAreDifferent(oldList, ListOfEvents);
        //    }
        //    else
        //    {
        //        ChangedAtLastRun = true;
        //    }

        //    // Store the read in list to the oldList
        //    oldList = ListOfEvents;
        //}

        public void CalculateValues()
        {
            // Insert the O-Event Codes
            foreach (RosterReservation resCurrent in AllReservations)
            {
                foreach (RosterReservation resInner in AllReservations)
                {
                    if (resCurrent != resInner)
                    {
                        if (resCurrent.Name == resInner.Name)
                        {
                            string code = CreateEventCode(resCurrent);
                            resInner.EventCodes.Add(code);
                            resInner.TotalEventsCode += $"({code})";
                        }
                    }
                }
            }

            // Calculate Day Passes
            DayPasseCount = 0;
            foreach (RosterReservation res in AllReservations)
            {
                if (res.Type != GuestType.Overnight)
                {
                    DayPasseCount += res.EntryCount;
                }
            }

            // Calculate Overnight Passes
            OvPassRecords = new List<OvPassRecord>();
            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (RosterReservation res in AllReservations)
            {
                if (res.Type == GuestType.Overnight)
                {
                    OvPassRecord opr = new OvPassRecord { Count = 1, Date = res.DepartDate };
                    AddOrIncrement(opr, OvPassRecords);
                }
            }
        }

        public string ToStringForTeasAndTours()
        {
            // Local declarations
            StringBuilder sb = new StringBuilder();
            const int OffsetAfterName = 18;
            const int OffsetAfterTime = 10;
            const int OffsetAfterCount = 10;
            const int OffsetAfterType = 0;

            int offsetAfterMEventCode = 0;
            foreach (RosterReservation res in AllReservations)
            {
                if (res.TotalEventsCode.Length > offsetAfterMEventCode)
                {
                    offsetAfterMEventCode = res.TotalEventsCode.Length;
                }
            }
            offsetAfterMEventCode += 2;

            // Sort the reservations list
            AllReservations = AllReservations.OrderBy(res => res.TimeValue).ThenBy(res => res.Type).ThenBy(res => res.Name).ToList();

            return sb.ToString();

            // Local Functions
            void BuffInsert(string s, int offset)
            {
                int spacesAfter = offset - s.Length;
                sb.Append(s);
                sb.Append(' ', spacesAfter);
            }
        }

        //public string ToStringForOvernights()
        //{
        //    // Local declarations
        //    StringBuilder sb = new StringBuilder();
        //    const int OffsetAfterName = 18;
        //    const int OffsetAfterMEventCode = 16;
        //    const int OffsetAfterDate = 14;
        //    const int OffsetAfterCount = 10;
        //    const int OffsetAfterType = 0;

        //    // Create header
        //    string title = $"Summed Up Report of Overnights for [{DateTime.Today.Date:M/d/yy}] (Run at: {DateTime.Now:hh:mm tt})";
        //    sb.AppendLine(title);
        //    sb.AppendLine();

        //    string header = $"{"Name",-OffsetAfterName}{"O-Events",-OffsetAfterMEventCode}{"Depart-Date",-OffsetAfterDate}{"Count",-OffsetAfterCount}{"Type",-OffsetAfterType}";
        //    sb.AppendLine(header);
        //    sb.AppendLine();

        //    foreach (RosterReservation res in ListOfEvents[3].Reservations)
        //    {
        //        // Assmble "Multi Event Code"
        //        string mec = res.EventCodes.Count > 0 ? res.EventCodes.Aggregate(string.Empty, (current, code) => $"{current}({code})") : string.Empty;
        //        string followMec = mec != string.Empty ? "\"" : string.Empty;

        //        for (int i = 0; i < res.EntryCount; i++)
        //        {
        //            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
        //            if (i == 0)
        //            {
        //                sb.AppendLine($"{res.Name,-OffsetAfterName}{mec,-OffsetAfterMEventCode}{res.DepartDate,-OffsetAfterDate}{res.GuestCount,-OffsetAfterCount}{res.Type,-OffsetAfterType}");
        //            }
        //            else
        //            {
        //                sb.AppendLine($"{res.Name,-OffsetAfterName}{followMec,-OffsetAfterMEventCode}{res.DepartDate,-OffsetAfterDate}{"\"",-OffsetAfterCount}{res.Type,-OffsetAfterType}");
        //            }
        //        }
        //    }

        //    // Print the passes
        //    sb.AppendLine();
        //    sb.AppendLine($"Overnight Passes: [Total: {OvernightPassCount}]");
        //    for (int i = 0; i < OvernightPassDates.Count; i++)
        //    {
        //        sb.AppendLine($"{OvernightPassDates[i].Substring(0,5)}: {OvernightPassCounts[i]}");
        //    }

        //    return sb.ToString();
        //}

        #endregion

        #region Private Methods

        static bool IsCorrectDateFormat(string input)
            {
                return DateTime.TryParse(input, out DateTime dateValue);
            }

        static bool ListsAreDifferent(List<EventRoster> list1, List<EventRoster> list2)
        {
            if (list1 == null || list2 == null)
            {
                return true;
            }

            // ReSharper disable once LoopCanBeConvertedToQuery
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Reservations.Count != list2[i].Reservations.Count)
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsEntryLine(IReadOnlyList<string> parsedLine, int dateIndex)
        {
            return parsedLine.Count > dateIndex && DateTime.TryParse(parsedLine[dateIndex], out DateTime _);
        }

        static void AddOrIncrement(RosterReservation res, List<RosterReservation> list)
        {
            int otherIndex = list.FindIndex(e => e.Name == res.Name && e.Type == res.Type && e.TimeValue == res.TimeValue);
            if (otherIndex == -1)
            {
                res.EntryCount = 1;
                list.Add(res);
            }
            else
            {
                list[otherIndex].EntryCount++;
                list[otherIndex].GuestCount += res.GuestCount;
            }
        }

        static void AddOrIncrement(OvPassRecord opr, List<OvPassRecord> list)
        {
            int otherIndex = list.FindIndex(e => e.Date == opr.Date);
            if (otherIndex == -1)
            {
                opr.Count = 1;
                list.Add(opr);
            }
            else
            {
                list[otherIndex].Count++;
            }
        }

        static string CreateEventCode(RosterReservation res)
        {
            string returnString = string.Empty;

            switch (res.Type)
            {
                case GuestType.Overnight:
                    returnString = "Ov";
                    break;
                case GuestType.Tour:
                    string[] split = res.DisplayTime.Split(':');
                    string subTime = split[1][0] == '0' ? string.Empty : ":" + split[1][0];
                    returnString = $"Tr.{split[0]}{subTime}{split[1].Substring(split[1].Length - 2, 1)}";
                    break;
                case GuestType.Tea:
                    split = res.DisplayTime.Split(':');
                    subTime = split[1][0] == '0' ? string.Empty : ":" + split[1][0];
                    returnString = $"Te.{split[0]}{subTime}{split[1].Substring(split[1].Length - 2, 1)}";
                    break;
                case GuestType.Madrigal:
                    returnString = "MAD";
                    break;
                case GuestType.Concert:
                    returnString = "CON";
                    break;
                case GuestType.Dinner:
                    returnString = "DIN";
                    break;
            }

            return returnString;
        }

        #endregion

        #endregion
    }
}