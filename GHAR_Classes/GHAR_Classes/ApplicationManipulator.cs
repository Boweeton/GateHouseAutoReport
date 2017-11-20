using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GHAR_Classes
{
    public class ApplicationManipulator
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public void RunMagasysArrivalsReport(string startDate, string endDate)
        {
            // Local Declarations
            bool shallContinue = true;

            // Initialize a Process for BackPack
            Process backPackProcess = Process.GetProcessesByName("BackPack").FirstOrDefault();

            // If the Process is running, bring it to the foreground
            if (backPackProcess != null)
            {
                IntPtr h = backPackProcess.MainWindowHandle;
                SetForegroundWindow(h);
            }

            // [IMPORTANT] (Here, the program expects the highlighted button in Megasys to be "Favorites")

            // Arrow DOWN to the Front Office and TAB over the the selection screen
            Thread.Sleep(100);
            SendAndWait(4, "{DOWN}", 30);
            SendAndWait(2, "{TAB}", 150);

            // Arrow DOWN to Arrivals and hit ENTER
            SendAndWait(12, "{DOWN}", 30);
            Thread.Sleep(500);
            SendAndWait("{ENTER}");

            // Arrow DOWN to select FILE output for the print selection
            Thread.Sleep(1500);
            SendAndWait(16, "{DOWN}", 30);

            // TAB down to input starting date and delete (BACKSPACE) what's there
            SendAndWait(2, "{TAB}", 150);
            SendAndWait("{BACKSPACE}", 100);

            // Type the new date
            SendAndWait(startDate);

            // TAB down to input ending date and delete (BACKSPACE) what's there
            SendAndWait("{TAB}", 150);
            SendAndWait("{BACKSPACE}", 100);

            // Type the new date
            SendAndWait(endDate);

            // Hit ENTER to request information to print
            Thread.Sleep(500);
            SendAndWait("{ENTER}");

            // Once inside the displayed Notepad File, hit ALT (%) to access the ribons
            Thread.Sleep(3500);
            SendAndWait("%");

            // Arrow DOWN to select the "Save As" action and hit ENTER
            Thread.Sleep(1000);
            SendAndWait(4, "{DOWN}", 30);
            Thread.Sleep(1000);
            SendAndWait("{ENTER}");
            Thread.Sleep(900);

            // Calculate and type the file name
            string[] splitList = startDate.Split('/');
            string fileName = $"AutoReport_[RptOf({splitList[0]}.{splitList[1]}.{splitList[2]})]_[CrtOn({DateTime.Today.Date:M-d-yy})--({DateTime.Now:h.mm.sstt})]";
            SendAndWait(fileName);

            // Hit ENTER to save the file as the calculated path & name
            Thread.Sleep(1200);
            SendAndWait("{ENTER}");

            // Once back in the body of the Notepad window, hit F4 to close the window
            SendAndWait("%{F4}");

            // Arrow RIGHT and hit SPACE to confirm close
            Thread.Sleep(500);
            SendAndWait("{RIGHT}", 30);
            SendAndWait(" ");

            // Once back in Magasys, TAB over to the Folders Panel and arrow UP to get back to "Favorites"
            SendAndWait("{TAB}", 150);
            SendAndWait(8, "{UP}", 30);
        }

        public void FocusMaegasys()
        {
            // "C:\Program Files (x86)\Megasys\BackPack\BackPack.exe" -hd Mclient.ini MClient.pvx
        }

        static void SendAndWait(string keys, Process p, int delay = 0)
        {
            SendKeys.SendWait(keys);
            while (!p.Responding) { }
            Thread.Sleep(delay);
        }

        static void SendAndWait(string keys, int delay = 0)
        {
            SendKeys.SendWait(keys);
            Thread.Sleep(delay);
        }

        static void SendAndWait(int repeats, string keys, int delay = 0)
        {
            for (int i = 0; i < repeats; i++)
            {
                SendAndWait(keys, delay);
            }
        }
    }
}