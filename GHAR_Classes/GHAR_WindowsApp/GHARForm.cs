using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using GHAR_Classes;

namespace GHAR_WindowsApp
{
    public partial class MainScreenForm : Form
    {
        // Data
        readonly MegasysReportParser rp;
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
            nothingChangedMessage.Text = string.Empty;
            nothingChangedMessage.BackColor = BackColor;
            nothingChangedMessage.ForeColor = Color.White;
        }

        void OnLoadTodaysGuestlistButtonClick(object sender, EventArgs e)
        {
#if DEBUG
            string message = null;
#else
            string message = am.RunMagasysArrivalsReport($"{DateTime.Today:d}", $"{DateTime.Today:d}");
#endif
            if (message != null)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#if DEBUG
            masterPath = @"I:\AutoReport_[RptOf11.26.2017]_[CrtOn11-19-17--10.32.55PM].txt";
#else
            masterPath = am.FilePath;
#endif

            // Read in the report
            rp.CreateEventLists();
            rp.ReadInArrivalsReport(masterPath);
            rp.CalculateValues();

            // Check to see if anyhting has changed
            RunTimer();
            if (ListsAreDifferent(oldList, rp.ListOfEvents))
            {
                nothingChangedMessage.BackColor = Color.ForestGreen;
                nothingChangedMessage.Text = "Sucessfully loaded new report";
            }
            else
            {
                nothingChangedMessage.BackColor = Color.DodgerBlue;
                nothingChangedMessage.Text = "Nothing was different";
            }

            // Store the read in list to the oldList
            oldList = rp.ListOfEvents;

            createOvernightsButton.Enabled = true;
            createToursAndTeasButton.Enabled = true;
        }

        void OnCreateToursAndTeasButtonClick(object sender, EventArgs e)
        {
            string text = rp.ToStringForTeasAndTours();
            Directory.CreateDirectory(Constants.CreatedReportsFolder);
            string path = Path.Combine(Constants.CreatedReportsFolder, $"ToursAndTeas_{DateTime.Today:yy-MM-dd}--{DateTime.Now:tt_hh.mm}.txt");

            File.WriteAllText(path, text);

            Process.Start(path);

            UpdateLastRunText();
        }

        void OnCreateOvernightsButtonClick(object sender, EventArgs e)
        {
            string text = rp.ToStringForOvernights();
            Directory.CreateDirectory(Constants.CreatedReportsFolder);
            string path = Path.Combine(Constants.CreatedReportsFolder, $"Overnights_{DateTime.Today:yy-MM-dd}--{DateTime.Now:tt_hh.mm}.txt");

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
            nothingChangedMessage.BackColor = BackColor;
        }

        void OnManuallyGeneratePathButtonClick(object sender, EventArgs e)
        {
            ManualPathForm m = new ManualPathForm(am, rp);
            m.Show();
            createOvernightsButton.Enabled = true;
            createToursAndTeasButton.Enabled = true;
        }
    }
}
