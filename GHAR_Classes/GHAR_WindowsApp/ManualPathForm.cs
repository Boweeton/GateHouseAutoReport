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
    public partial class ManualPathForm : Form
    {
        // Data
        readonly string toursReportPath;
        readonly string arrivalsReportPath;
        ReportManager rp;

        public ManualPathForm(ReportManager rpParam)
        {
            InitializeComponent();
            rp = rpParam;
            toursReportPath = rp.GeneratePath("TourReport", $"{DateTime.Today:M/d/yy}");
            arrivalsReportPath = rp.GeneratePath("ArrivalReport", $"{DateTime.Today:M/d/yy}");

            otherArrivalsReportPathTextBox.Click += (sender, args) => otherArrivalsReportPathTextBox.Select(0, otherArrivalsReportPathTextBox.Text.Length);
        }

        void OnCalculateButtonClick(object sender, EventArgs e)
        {
            // Check if the file exists
            if (File.Exists(toursReportPath) && File.Exists(arrivalsReportPath))
            {
                // Read in the report
                rp.ReadInBothReports(toursReportPath, arrivalsReportPath);
                rp.CalculateValues();

                Close();
            }
            else if (File.Exists(toursReportPath))
            {
                // If the does not exist, pop up a message to say so
                MessageBox.Show("The Tours Report was never created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (File.Exists(arrivalsReportPath))
            {
                // If the does not exist, pop up a message to say so
                MessageBox.Show("The Arrivals Report was never created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // If the does not exist, pop up a message to say so
                MessageBox.Show("Neither of the reports were created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ManualPathForm_Load(object sender, EventArgs e)
        {
            toursReportPathTextBox.Text = toursReportPath;
            otherArrivalsReportPathTextBox.Text = arrivalsReportPath;
        }

        void pathTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        void OnToursReportPathCopyButtonClick(object sender, EventArgs e)
        {
            Clipboard.SetText(toursReportPathTextBox.Text);
        }

        void OnOtherArrivalsReportPathCopyButtonClick(object sender, EventArgs e)
        {
            Clipboard.SetText(otherArrivalsReportPathTextBox.Text);
        }
    }
}
