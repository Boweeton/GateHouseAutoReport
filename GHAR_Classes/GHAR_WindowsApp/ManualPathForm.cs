using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        MegasysReportParser rp;

        public ManualPathForm(ApplicationManipulator amParam, MegasysReportParser rpParam)
        {
            InitializeComponent();
            amParam.FindPath($"{DateTime.Today:d}");
            displayPath = amParam.FilePath;
            rp = rpParam;

            pathTextBox.Click += (sender, args) => pathTextBox.Select(0, pathTextBox.Text.Length);
        }

        void OnCalculateButtonClick(object sender, EventArgs e)
        {
            // Read in the report
            rp.CreateEventLists();
            rp.ReadInArrivalsReport(displayPath);
            rp.CalculateValues();

            Close();
        }

        void ManualPathForm_Load(object sender, EventArgs e)
        {
            pathTextBox.Text = displayPath;
        }

        void pathTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
