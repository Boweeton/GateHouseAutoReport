using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GHAR_WindowsApp
{
    public partial class HelpVideoForm : Form
    {
        public HelpVideoForm()
        {
            InitializeComponent();
        }

        void HelpVideoForm_Load(object sender, EventArgs e)
        {
            helpVideoMediaPlayer.URL = @"C:\Users\Boweeton\Desktop\test1.webm";
        }
    }
}
