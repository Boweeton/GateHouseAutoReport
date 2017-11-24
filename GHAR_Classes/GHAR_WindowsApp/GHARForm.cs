using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using GHAR_Classes;
using Timer = System.Timers.Timer;

namespace GHAR_WindowsApp
{
    public partial class MainScreenForm : Form
    {
        // Data
        MegasysReportParser rp;
        ApplicationManipulator am;
        string masterPath;
        List<EventRoster> oldList;
        bool hasRunOnce;

        public MainScreenForm()
        {
            InitializeComponent();
            rp = new MegasysReportParser();
            am = new ApplicationManipulator();
            createOvernightsButton.Enabled = false;
            createToursAndTeasButton.Enabled = false;
            nothingChangedMessage.Text = string.Empty;
            hasRunOnce = false;
        }

        void OnProgramLoad(object sender, EventArgs e)
        {

        }

        void OnLaodTodaysGuestlistButtonClick(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter("test.txt");
            masterPath = am.GeneratePath($"{DateTime.Today:M/d/yy}");

            // Read in the report
            rp.ListOfEvents = rp.CreateEventLists();
            rp.ReadInArrivalsReport(@"D:\AutoReport_[RptOf11.26.2017]_[CrtOn11-19-17--10.32.55PM].txt");
            rp.CalculateValues();

            // First run initialization
            if (!hasRunOnce)
            {
                oldList = rp.ListOfEvents;
            }

            // Check to see if anyhting has changed
            if (ListsAreDifferant(oldList, rp.ListOfEvents) && hasRunOnce)
            {
                // Create a timer
                Timer timer = new System.Timers.Timer(2000);

                // Hook up the Elapsed event for the timer.
                timer.Elapsed += OnTimedEvent;
                timer.Enabled = true;

                nothingChangedMessage.Text = "*Nothing was differnt";
            }

            // Store the read in list to the oldList
            oldList = rp.ListOfEvents;

            createOvernightsButton.Enabled = true;
            createToursAndTeasButton.Enabled = true;
            hasRunOnce = true;
        }

        void OnCreateToursAndTeasButtonClick(object sender, EventArgs e)
        {

            UpdateLastRunText();
        }

        void OnCreateOvernightsButtonClick(object sender, EventArgs e)
        {


            UpdateLastRunText();
        }

        void UpdateLastRunText()
        {
            lastRunTextBox.Text = $"{DateTime.Today:M/d/yy} at {DateTime.Now:hh:mm tt}";
        }

        bool ListsAreDifferant(List<EventRoster> list1, List<EventRoster> list2)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Reservations.Count != list2[i].Reservations.Count)
                {
                    return false;
                }
            }
            return true;
        }

        void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            nothingChangedMessage.Text = string.Empty;
        }
    }
}
