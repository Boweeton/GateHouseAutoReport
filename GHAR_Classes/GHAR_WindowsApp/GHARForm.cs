﻿using System;
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

        void OnCreateToursAndTeasButtonClick(object sender, EventArgs e)
        {
            string text = rm.ToStringForTeasAndTours();
            Directory.CreateDirectory(Constants.CreatedReportsFolder);
            string path = Path.Combine(Constants.CreatedReportsFolder, $"ToursAndTeas_{DateTime.Today:yy-MM-dd}--{DateTime.Now:tt_hh.mm}.txt");

            File.WriteAllText(path, text);

            Process.Start(path);
        }

        void OnCreateOvernightsButtonClick(object sender, EventArgs e)
        {
            //string text = rm.ToStringForOvernights();
            Directory.CreateDirectory(Constants.CreatedReportsFolder);
            string path = Path.Combine(Constants.CreatedReportsFolder, $"Overnights_{DateTime.Today:yy-MM-dd}--{DateTime.Now:tt_hh.mm}.txt");

            //File.WriteAllText(path, text);

            Process.Start(path);
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
            using (ManualPathForm m = new ManualPathForm(am, rm))
            {
                //m.Show();
                if (m.ShowDialog() == DialogResult.OK)
                {
                }
            }
            UpdateResultsBanner();

            createOvernightsButton.Enabled = true;
            createDayEventsButton.Enabled = true;

            UpdateLastRunText();
        }

        public void UpdateResultsBanner()
        {
            // Check to see if anyhting has changed
            RunTimer();
            if (rm.ChangedAtLastRun)
            {
                nothingChangedMessage.BackColor = Color.ForestGreen;
                nothingChangedMessage.Text = "Sucessfully loaded new report";
            }
            else
            {
                nothingChangedMessage.BackColor = Color.DodgerBlue;
                nothingChangedMessage.Text = "Nothing was different";
            }
        }

        void testButton_Click(object sender, EventArgs e)
        {
            rm.ReadInBothReports(@"I:\ToursReport7.txt", @"I:\OtherArrivalsReport2.txt");
            rm.CalculateValues();
            rm.ToStringForTeasAndTours();
        }
    }
}
