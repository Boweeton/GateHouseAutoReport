using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        bool hasRunOnce;
        const string ReportsFolder = "CreatedReports";

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

        void OnLoadTodaysGuestlistButtonClick(object sender, EventArgs e)
        {
            string message = am.RunMagasysArrivalsReport($"{DateTime.Today:d}", $"{DateTime.Today:d}");
            if (message != null)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //StreamWriter sw = new StreamWriter("test.txt");
            masterPath = am.FilePath;

            // Read in the report
            rp.CreateEventLists();
            rp.ReadInArrivalsReport(masterPath);
            rp.CalculateValues();

            // First run initialization
            if (!hasRunOnce)
            {
                oldList = rp.ListOfEvents;
            }

            // Check to see if anyhting has changed
            if (ListsAreDifferent(oldList, rp.ListOfEvents) && hasRunOnce)
            {
                RunTimer();
                nothingChangedMessage.Text = "*Nothing was different";
            }

            // Store the read in list to the oldList
            oldList = rp.ListOfEvents;

            createOvernightsButton.Enabled = true;
            createToursAndTeasButton.Enabled = true;
            hasRunOnce = true;
        }

        void OnCreateToursAndTeasButtonClick(object sender, EventArgs e)
        {
            string text = rp.ToStringForTeasAndTours();
            Directory.CreateDirectory(ReportsFolder);
            string path = Path.Combine(ReportsFolder, $"ToursAndTeas_{DateTime.Today:yy-MM-dd}--{DateTime.Now:hh.mm.ss}");

            File.WriteAllText(path, text);

            Process.Start(path);

            UpdateLastRunText();
        }

        void OnCreateOvernightsButtonClick(object sender, EventArgs e)
        {
            string text = rp.ToStringForOvernights();
            Directory.CreateDirectory(ReportsFolder);
            string path = Path.Combine(ReportsFolder, $"Overnights_{DateTime.Today:yy-MM-dd}--{DateTime.Now:hh.mm.ss}");

            File.WriteAllText(path, text);

            Process.Start(path);

            UpdateLastRunText();
        }

        void UpdateLastRunText()
        {
            lastRunTextBox.Text = $"{DateTime.Now:hh:mm tt}";
        }

        static bool ListsAreDifferent(List<EventRoster> list1, List<EventRoster> list2)
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

        async void RunTimer()
        {
            await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 4000)
                {
                }
            });
            nothingChangedMessage.Text = string.Empty;
        }
    }
}
