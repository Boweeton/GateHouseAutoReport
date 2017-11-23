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

        public MainScreenForm()
        {
            InitializeComponent();
            rp = new MegasysReportParser();
            am = new ApplicationManipulator();
        }

        void OnProgramLoad(object sender, EventArgs e)
        {

        }

        void OnLaodTodaysGuestlistButtonClick(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("test.txt");
            File.CreateText(@"C:\Users\Boweeton\Documents\test.txt");
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
    }
}
