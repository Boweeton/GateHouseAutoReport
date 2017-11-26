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
        readonly string displayPath;
        ReportManager rp;

        public ManualPathForm(ApplicationManipulator amParam, ReportManager rpParam)
        {
            InitializeComponent();
            amParam.FindPath($"{DateTime.Today:d}");
            displayPath = amParam.FilePath;
            rp = rpParam;

            otherArrivalsReportPathTextBox.Click += (sender, args) => otherArrivalsReportPathTextBox.Select(0, otherArrivalsReportPathTextBox.Text.Length);
        }

        void OnCalculateButtonClick(object sender, EventArgs e)
        {
            // Check if the file exists
            if (File.Exists(displayPath))
            {
                // Read in the report
                rp.CreateEventLists();
                //rp.ReadInArrivalsReport(displayPath);
                rp.CalculateValues();

                Close();
            }
            else
            {
                // If the does not exist, pop up a message to say so
                MessageBox.Show("The file was never created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ManualPathForm_Load(object sender, EventArgs e)
        {
            otherArrivalsReportPathTextBox.Text = displayPath;
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
