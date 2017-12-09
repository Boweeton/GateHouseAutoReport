using System;
using System.IO;
using System.Windows.Forms;
using GHAR_Classes;
using WMPLib;

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
            string videoPath = Path.GetFullPath(Path.Combine(Constants.MediaFolder, @"gharHelpVideo.mp4"));

            helpVideoMediaPlayer.URL = videoPath;
            helpVideoMediaPlayer.Ctlcontrols.stop();
        }
    }
}
