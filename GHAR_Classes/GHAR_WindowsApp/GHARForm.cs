using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GHAR_Classes;

namespace GHAR_WindowsApp
{
    public partial class MainScreenForm : Form
    {
        // Data
        MegasysReportParser rp;
        ApplicationManipulator am;
        string masterPath;
        List<EventRoster> oldList;

        public MainScreenForm()
        {
            InitializeComponent();
            rp = new MegasysReportParser();
            am = new ApplicationManipulator();
            createOvernightsButton.Enabled = false;
            createToursAndTeasButton.Enabled = false;
            nothingChangedMessage.Text = string.Empty;
        }

        void OnProgramLoad(object sender, EventArgs e)
        {

        }

        void OnLaodTodaysGuestlistButtonClick(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter("test.txt");
            masterPath = am.GeneratePath($"{DateTime.Today:M/d/yy}");

            createOvernightsButton.Enabled = true;
            createToursAndTeasButton.Enabled = true;
        }

        void OnCreateToursAndTeasButtonClick(object sender, EventArgs e)
        {
            // Read in the report
            rp.ListOfEvents = rp.CreateEventLists();
            rp.ReadInArrivalsReport(@"D:\AutoReport_[RptOf11.26.2017]_[CrtOn11-19-17--10.32.55PM].txt");
            rp.CalculateValues();

            // Check to see if anyhting has changed
            if (oldList.Equals(rp.ListOfEvents))
            {
                nothingChangedMessage.Text = "Nothing was differnt";
            }
            // Store the read in list to the oldList
            oldList = rp.ListOfEvents;

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
    }
}
