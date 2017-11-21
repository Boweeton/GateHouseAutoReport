
using System.Collections.Generic;

namespace GHAR_Classes
{
    public class RosterReservation
    {

        #region Data



        #endregion

        #region Constructors



        #endregion

        #region Properties

        public string Name { get; set; }
        public int GuestCount { get; set; }
        public int EntryCount { get; set; }
        public GuestType Type { get; set; }
        public string DepartDate { get; set; }
        public string DisplayTime { get; set; }
        public int TimeValue { get; set; }
        public List<string> EventCodes { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public void CalculateTimeValue()
        {
            if (DisplayTime != "-")
            {
                // Local declarations
                string tmpDisplayTime = DisplayTime;

                // Split the displayTime by colon
                string[] parsedTime = tmpDisplayTime.Split(':');

                // Store hours
                string hh = parsedTime[0];

                // Split the displayTime by colon
                parsedTime = parsedTime[1].Split(' ');

                // Store minutes
                string mm = parsedTime[0];

                // Store clockZone
                string clockZone = parsedTime[1];

                // Calculate
                TimeValue += int.Parse(hh) * 100;
                if (clockZone == "PM")
                {
                    TimeValue += 1200;
                }
                TimeValue += int.Parse(mm);
            }
            else
            {
                TimeValue = 0;
            }
        }

        public override string ToString()
        {
            return $"{Name} - {EntryCount}";
        }

        #endregion

        #region Private Methods



        #endregion

        #endregion
    }
}
