using System;
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
        const int PageWidth = 68;
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
        public bool IncompleteDataLoad { get; set; }
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
                        case "MADRIG":
                            tmp.Type = GuestType.Madrigal;
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

            // Tally up the total
            foreach (OvPassRecord opr in OvPassRecords)
            {
                OvernightPassCount += opr.Count;
            }
        }

        public string ToStringForTeasAndTours()
        {
            // Local declarations
            StringBuilder sb = new StringBuilder();
            const int OffsetAfterName = 18;
            const int OffsetAfterTime = 10;
            const int OffsetAfterCount = 6;
            const int OffsetAfterType = 0;

            int offsetAfterMEventCode = 0;
            foreach (RosterReservation res in AllReservations)
            {
                if (res.TotalEventsCode.Length > offsetAfterMEventCode)
                {
                    offsetAfterMEventCode = res.TotalEventsCode.Length;
                }
            }
            if (offsetAfterMEventCode > "O-Events".Length)
            {
                offsetAfterMEventCode += 2;
            }
            else
            {
                offsetAfterMEventCode = "O-Events".Length + 2;
            }

            // Sort the reservations list
            AllReservations = AllReservations.OrderBy(res => res.TimeValue).ThenBy(res => res.Type).ThenBy(res => res.Name).ToList();

            // Print report title
            sb.AppendLine($"Summed Report of Day Events for [{DateTime.Today:M/d/yy}] (Run at: {DateTime.Now:hh:mm tt})");

            // Print the partition
            sb.AppendLine();
            for (int j = 0; j < PageWidth; j++)
            {
                sb.Append("_");
            }
            sb.AppendLine();
            sb.AppendLine();

            // Print header
            sb.Append($"{"Name",-OffsetAfterName}{"Count",-OffsetAfterCount}");
            BuffInsert("O-Events", offsetAfterMEventCode);
            sb.AppendLine($"{"Time",-OffsetAfterTime}{"Type", -OffsetAfterType}");
            sb.AppendLine();

            // Print the partition
            for (int j = 0; j < PageWidth / 2; j++)
            {
                sb.Append("- ");
            }
            sb.AppendLine();

            // Master print loop
            int headCount = 0;
            int passCount = 0;
            for (int i = 0; i < AllReservations.Count; i++)
            {
                if (AllReservations[i].Type != GuestType.Overnight)
                {
                    // Print the current data
                    for (int j = 0; j < AllReservations[i].EntryCount; j++)
                    {
                        if (j == 0)
                        {
                            sb.Append($"{AllReservations[i].Name,-OffsetAfterName}{AllReservations[i].GuestCount,-OffsetAfterCount}");
                            BuffInsert(AllReservations[i].TotalEventsCode, offsetAfterMEventCode);
                            sb.AppendLine($"{AllReservations[i].DisplayTime,-OffsetAfterTime}{AllReservations[i].Type,-OffsetAfterType}");
                        }
                        else
                        {
                            string mec2 = AllReservations[i].TotalEventsCode == string.Empty ? string.Empty : "\"";
                            sb.Append($"{AllReservations[i].Name,-OffsetAfterName}{"\"",-OffsetAfterCount}");
                            BuffInsert(mec2, offsetAfterMEventCode);
                            sb.AppendLine($"{AllReservations[i].DisplayTime,-OffsetAfterTime}{AllReservations[i].Type,-OffsetAfterType}");
                        }
                    }
                    passCount += AllReservations[i].EntryCount;
                    headCount += AllReservations[i].GuestCount;

                    // Check for partition information
                    if (i < AllReservations.Count - 1)
                    {
                        // Infor for after tour
                        if (AllReservations[i].Type == GuestType.Tour && AllReservations[i + 1].Type != GuestType.Tour)
                        {
                            sb.AppendLine();
                            sb.AppendLine($"Total: {headCount}\tRemaining: {MaxTourSize-headCount}\t({passCount} Passes)");
                            passCount = 0;
                            headCount = 0;

                            // Print the partition
                            for (int j = 0; j < PageWidth/2; j++)
                            {
                                sb.Append("- ");
                            }
                            sb.AppendLine();
                        }
                        // Info for after not tour
                        else if (AllReservations[i].Type != AllReservations[i + 1].Type)
                        {
                            sb.AppendLine();
                            sb.AppendLine($"({passCount} Passes)");
                            passCount = 0;
                            headCount = 0;
                            
                            // Print the partition
                            for (int j = 0; j < PageWidth/2; j++)
                            {
                                sb.Append("- ");
                            }
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        // Check for tour info and print
                        if (AllReservations[i].Type == GuestType.Tour)
                        {
                            sb.AppendLine();
                            sb.AppendLine($"Total: {headCount}\tRemaining: {MaxTourSize - headCount}\t({passCount} Passes)");
                            passCount = 0;
                            headCount = 0;

                            // Print the partition
                            for (int j = 0; j < PageWidth/2; j++)
                            {
                                sb.Append("- ");
                            }
                            sb.AppendLine();
                        }
                        // Print not tour info
                        else
                        {
                            sb.AppendLine();
                            sb.AppendLine($"({passCount} Passes)");
                            passCount = 0;
                            headCount = 0;

                            // Print the partition
                            for (int j = 0; j < PageWidth/2; j++)
                            {
                                sb.Append("- ");
                            }
                            sb.AppendLine();
                        }
                    }
                }
            }

            // Print the total pass count
            sb.AppendLine();
            sb.AppendLine();
            int extraSheets = DayPasseCount % 3 == 0 ? 0 : 1;
            sb.Append($"Day Passes: {DayPasseCount}\t\t({DayPasseCount} = {(DayPasseCount / 3) + extraSheets} sheets)");

            return sb.ToString();

            // Local Functions
            void BuffInsert(string s, int offset)
            {
                int spacesAfter = offset - s.Length;
                sb.Append(s);
                sb.Append(' ', spacesAfter);
            }
        }

        public string ToStringForOvernights()
        {
            // Local declarations
            StringBuilder sb = new StringBuilder();
            const int OffsetAfterName = 18;
            const int OffsetAfterDate = 12;
            const int OffsetAfterCount = 6;
            const int OffsetAfterType = 0;

            int offsetAfterMEventCode = 0;
            foreach (RosterReservation res in AllReservations)
            {
                if (res.TotalEventsCode.Length > offsetAfterMEventCode)
                {
                    offsetAfterMEventCode = res.TotalEventsCode.Length;
                }
            }
            if (offsetAfterMEventCode > "O-Events".Length)
            {
                offsetAfterMEventCode += 2;
            }
            else
            {
                offsetAfterMEventCode = "O-Events".Length + 2;
            }

            // Sort the reservations list
            AllReservations = AllReservations.OrderBy(res => res.TimeValue).ThenBy(res => res.Type).ThenBy(res => res.Name).ToList();

            // Print report title
            sb.AppendLine($"Summed Report of Overnight Arrivals for [{DateTime.Today:M/d/yy}] (Run at: {DateTime.Now:hh:mm tt})");

            // Print the partition
            sb.AppendLine();
            for (int j = 0; j < PageWidth; j++)
            {
                sb.Append("_");
            }
            sb.AppendLine();
            sb.AppendLine();

            // Print header
            sb.Append($"{"Name",-OffsetAfterName}{"Departure",-OffsetAfterDate}");
            BuffInsert("O-Events", offsetAfterMEventCode);
            sb.AppendLine($"{"Count",-OffsetAfterCount}{"Type",-OffsetAfterType}");
            sb.AppendLine();

            // Loop for printing
            foreach (RosterReservation res in AllReservations)
            {
                if (res.Type == GuestType.Overnight)
                {
                    for (int i = 0; i < res.EntryCount; i++)
                    {
                        if (i == 0)
                        {
                            sb.Append($"{res.Name,-OffsetAfterName}{res.DepartDate,-OffsetAfterDate}");
                            BuffInsert(res.TotalEventsCode, offsetAfterMEventCode);
                            sb.AppendLine($"{res.GuestCount,-OffsetAfterCount}{res.Type,-OffsetAfterType}");
                        }
                        else
                        {
                            string mec2 = res.TotalEventsCode == string.Empty ? string.Empty : "\"";
                            sb.Append($"{res.Name,-OffsetAfterName}{res.DepartDate,-OffsetAfterDate}");
                            BuffInsert(mec2, offsetAfterMEventCode);
                            sb.AppendLine($"{res.GuestCount,-OffsetAfterCount}{res.Type,-OffsetAfterType}");
                        }
                    }
                }
            }

            // Sort the pass tallies
            OvPassRecords = OvPassRecords.OrderBy(opr => opr.Date).ToList();

            // Print the required passes
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine($"Overnight Passes: [Total: {OvernightPassCount}]");
            foreach (OvPassRecord opr in OvPassRecords)
            {
                sb.AppendLine($"{opr.Date.Substring(0, 5)}: {opr.Count}");
            }

            return sb.ToString();

            // Local Functions
            void BuffInsert(string s, int offset)
            {
                int spacesAfter = offset - s.Length;
                sb.Append(s);
                sb.Append(' ', spacesAfter);
            }
        }

        public string GeneratePath(string reportName, string date)
        {
            Directory.CreateDirectory(Constants.RawDataReportsFolder);
            return Path.GetFullPath(Path.Combine(Constants.RawDataReportsFolder, $"AutoReport[{reportName}]_[RptOf{date.Replace('/', '.')}]_[CrtOn{DateTime.Today.Date:M-d-yy}--{DateTime.Now:h.mm.sstt}].txt"));
        }

        #endregion

        #region Private Methods

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
