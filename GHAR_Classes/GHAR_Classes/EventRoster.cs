using System.Collections.Generic;

namespace GHAR_Classes
{
    public class EventRoster
    {
        #region Constructors



        #endregion

        #region Properties

        public string Title { get; set; }
        public GuestType Type { get; set; }
        public string Time { get; set; }
        public string MultiEventCode { get; set; }
        public List<RosterReservation> Reservations { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Title;
        }

        #endregion
    }
}