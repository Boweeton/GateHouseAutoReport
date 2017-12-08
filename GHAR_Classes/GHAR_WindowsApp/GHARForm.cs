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
        readonly ReportManager rm;
        ApplicationManipulator am;
        string masterPath;

        public MainScreenForm()
        {
            InitializeComponent();
            rm = new ReportManager();
            am = new ApplicationManipulator();
            createOvernightsButton.Enabled = false;
            createDayEventsButton.Enabled = false;
            nothingChangedMessage.Text = string.Empty;
        }

        void OnProgramLoad(object sender, EventArgs e)
        {
            nothingChangedMessage.Text = string.Empty;
            nothingChangedMessage.BackColor = BackColor;
            nothingChangedMessage.ForeColor = Color.White;
        }

        void OnManuallyGeneratePathButtonClick(object sender, EventArgs e)
        {
            using (ManualPathForm f = new ManualPathForm(rm))
            {
                //m.Show();
                if (f.ShowDialog() == DialogResult.OK)
                {
                }
            }
            UpdateResultsBanner();

            if (rm.IncompleteDataLoad)
            {
                createOvernightsButton.Enabled = false;
                createDayEventsButton.Enabled = false;
            }
            else
            {
                createOvernightsButton.Enabled = true;
                createDayEventsButton.Enabled = true;
            }

            UpdateLastRunText();
        }

        void OnCreateToursAndTeasButtonClick(object sender, EventArgs e)
        {
            string text = rm.ToStringForTeasAndTours();
            Directory.CreateDirectory(Constants.CreatedReportsFolder);
            string path = Path.Combine(Constants.CreatedReportsFolder, $"DayEvents_{DateTime.Today:yy-MM-dd}--{DateTime.Now:tt_hh.mm}.txt");

            File.WriteAllText(path, text);

            Process.Start(path);
        }

        void OnCreateOvernightsButtonClick(object sender, EventArgs e)
        {
            string text = rm.ToStringForOvernights();
            Directory.CreateDirectory(Constants.CreatedReportsFolder);
            string path = Path.Combine(Constants.CreatedReportsFolder, $"Overnights_{DateTime.Today:yy-MM-dd}--{DateTime.Now:tt_hh.mm}.txt");

            File.WriteAllText(path, text);

            Process.Start(path);
        }

        void UpdateLastRunText()
        {
            lastRunTextBox.Text = $"{DateTime.Now:hh:mm tt}";
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

        public void UpdateResultsBanner()
        {
            // Check to see if anyhting has changed
            RunTimer();
            if (rm.IncompleteDataLoad)
            {
                nothingChangedMessage.BackColor = Color.DarkOrange;
                nothingChangedMessage.Text = "Incomplete data load";
            }
            else if (rm.ChangedAtLastRun)
            {
                nothingChangedMessage.BackColor = Color.ForestGreen;
                nothingChangedMessage.Text = "New data loaded";
            }
            else
            {
                nothingChangedMessage.BackColor = Color.DodgerBlue;
                nothingChangedMessage.Text = "No data was different";
            }
        }
    }
}
