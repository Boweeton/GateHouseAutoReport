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
        public List<EventRoster> ListOfEvents { get; set; }

        public List<RosterReservation> OvernightGuests { get; set; }
        public List<RosterReservation> TourGuests { get; set; }
        public List<RosterReservation> TeaGuests { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public void SetEventsByFall2017()
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
            OvernightGuests = new List<RosterReservation>();
            TeaGuests = new List<RosterReservation>();
            TourGuests = new List<RosterReservation>();

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
                                // Check if the roster yet contains the reservation
                                foreach (RosterReservation res in eventRoster.Reservations)
                                {
                                    if (eventRoster.Reservations.Contains(tmpRes))
                                    {
                                        int index = eventRoster.Reservations.IndexOf(tmpRes);
                                        eventRoster.Reservations[index].EntryCount++;
                                    }
                                    else
                                    {
                                        tmpRes.EntryCount = 1;
                                        eventRoster.Reservations.Add(tmpRes);
                                    }
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
            Console.WriteLine("lolz");
        }

        public string ToStringForTeasAndTours()
        {
            return "";
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
