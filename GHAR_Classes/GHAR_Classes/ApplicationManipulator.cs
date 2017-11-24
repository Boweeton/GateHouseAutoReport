using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GHAR_Classes
{
    public class ApplicationManipulator
    {
        // Data
        public string FilePath { get; set; }

        IntPtr correctHandle;
        Process correctProcess;

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32.dll")]
        static extern IntPtr GetForegroundWindow();

        public void TestOnNotepad()
        {
            Process notePadProcess = Process.Start("Notepad.exe");

            Thread.Sleep(1000);

            if (notePadProcess != null)
            {
                IntPtr notepadHandle = notePadProcess.MainWindowHandle;
                SetForegroundWindow(notepadHandle);

                correctProcess = notePadProcess;
                correctHandle = notepadHandle;

                SendAndWait("test text 1  ");
                Thread.Sleep(3000);
                SendAndWait("test text 2");
            }
        }

        public string RunMagasysArrivalsReport(string startDate, string endDate)
        {
            // Initialize a Process for BackPack
            Process backPackProcess = Process.GetProcessesByName("BackPack").FirstOrDefault();
            correctProcess = backPackProcess;

            // If the Process is running, bring it to the foreground and begin
            if (backPackProcess == null)
            {
                return "Megasys was not open";
            }

            // Set the correctHandle to Backpack
            correctHandle = backPackProcess.MainWindowHandle;

            // Bring Megasys forward
            SetForegroundWindow(correctHandle);
            SetForegroundWindow(correctHandle);
            SetForegroundWindow(correctHandle);

            // [IMPORTANT] (Here, the program expects the highlighted button in Megasys to be "Favorites")

            // Arrow DOWN to the Front Office and TAB over the the selection screen
            Thread.Sleep(200);
            if (!SendAndWait(4, "{DOWN}", 90) || !SendAndWait(2, "{TAB}", 200) ||

                // Arrow DOWN to Arrivals
                !SendAndWait(12, "{DOWN}", 90))
            {
                return "Failed to tab and select \"Print Arrivals\"";
            }

            // Hit ENTER
            Thread.Sleep(400);
            if (!SendAndWait("{ENTER}"))
            {
                return "Failed to tab and select \"Print Arrivals\"";
            }

            // Arrow DOWN to select FILE output for the print selection
            Thread.Sleep(1200);
            //SendWithNoCheck(16, "{DOWN}", 150);
            for (int i = 0; i < 16; i++)
            {
                SendKeys.Send("{DOWN}");
                Thread.Sleep(60);
            }

            //// TAB down to input starting date and delete (BACKSPACE) what's there
            //if (!SendAndWait(2, "{TAB}", 150) || !SendAndWait("{BACKSPACE}", 100) ||

            //    // Type the new date
            //    !SendAndWait(startDate))
            //{
            //    return "Failed to input the start date";
            //}

            //// TAB down to input ending date and delete (BACKSPACE) what's there
            //if (!SendAndWait("{TAB}", 150) || !SendAndWait("{BACKSPACE}", 100) ||

            //    // Type the new date
            //    !SendAndWait(endDate))
            //{
            //    return "Failed to input the end date";
            //}

            // Hit ENTER to request information to print
            Thread.Sleep(500);
            SendWithNoCheck(1, "{ENTER}");

            // Make the correct process Notepad
            Process[] procs;
            Stopwatch sw = Stopwatch.StartNew();
            do
            {
                Thread.Sleep(300);
                procs = Process.GetProcessesByName("Notepad");

                if (sw.ElapsedMilliseconds > 7000)
                {
                    return "Notepad never opened";
                }
            } while (procs.Length == 0);
            correctProcess = procs[0];
            correctHandle = correctProcess.MainWindowHandle;

            // Once inside the displayed Notepad File, hit ALT (%) to access the ribons
            Thread.Sleep(1000);
            if (!SendAndWait("%", 500) ||

                // Arrow DOWN to select the "Save As" action and hit ENTER
                !SendAndWait(4, "{DOWN}", 90) || !SendAndWait("{ENTER}", 900))
            {
                return "Failed to select Notepad print options";
            }

            // Calculate and type the file name (and create the directory)
            FindPath(startDate);
            SendWithNoCheck(1, FilePath, 500);
            SendWithNoCheck(4, "{TAB}", 200);
            SendWithNoCheck(1, "{ENTER}",500);

            //if (!SendAndWait(FilePath, 800) ||

            //    // Hit ENTER to save the file as the calculated path & name
            //    !SendAndWait("{ENTER}"))
            //{
            //    return "Failed to input Auto File path and save";
            //}

            // Once back in the body of the Notepad window, KILL THE PROCESS!!!
            correctProcess.Kill();
            Thread.Sleep(500);

            // Reset corrects to Megasys
            correctProcess = backPackProcess;
            correctHandle = backPackProcess.MainWindowHandle;
            SetForegroundWindow(correctHandle);

            // Once back in Magasys, TAB over to the Folders Panel and arrow UP to get back to "Favorites"
            if (!SendAndWait(8, "{UP}", 30))
            {
                return "Failed to tab back to \"Favorites\" in BackPack";
            }

            // Bring back up GHAR
            Process gharProc = Process.GetProcessesByName("GHAR_WindowsApp").FirstOrDefault();
            if (gharProc != null)
            {
                correctHandle = gharProc.MainWindowHandle;
            }
            SetForegroundWindow(correctHandle);

            return null;
        }

        public void FindPath(string startDate)
        {
            Directory.CreateDirectory(Constants.RawDataReportsFolder);
            FilePath = GeneratePath(startDate);
        }

        public void FocusMaegasys()
        {
            // "C:\Program Files (x86)\Megasys\BackPack\BackPack.exe" -hd Mclient.ini MClient.pvx
        }

        bool SendAndWait(string keys, Process p, int delay = 0)
        {
            if (!CheckWindow())
            {

                return false;
            }

            SendKeys.SendWait(keys);
            while (!p.Responding)
            { }
            Thread.Sleep(delay);

            return true;
        }

        bool SendAndWait(string keys, int delay = 0)
        {
            if (!CheckWindow())
            {
                return false;
            }

            SendKeys.SendWait(keys);
            Thread.Sleep(delay);

            return true;
        }

        bool SendAndWait(int repeats, string keys, int delay = 0)
        {
            if (!CheckWindow())
            {
                return false;
            }

            for (int i = 0; i < repeats; i++)
            {
                SendAndWait(keys, delay);
            }

            return true;
        }

        void SendWithNoCheck(int repeats, string keys, int delay = 0)
        {
            for (int i = 0; i < repeats; i++)
            {
                SendKeys.SendWait(keys);
                Thread.Sleep(delay);
            }
        }

        bool CheckWindow()
        {
            return correctHandle == GetForegroundWindow();
        }

        public string GeneratePath(string date)
        {
            string p = System.Reflection.Assembly.GetEntryAssembly().Location;
            // ReSharper disable once StringLastIndexOfIsCultureSpecific.1
            int index = p.LastIndexOf(@"\");
            p = p.Substring(0, index);
            return Path.Combine(p, Constants.RawDataReportsFolder, $"AutoReport_[RptOf{date.Replace('/', '.')}]_[CrtOn{DateTime.Today.Date:M-d-yy}--{DateTime.Now:h.mm.sstt}].txt");
        }
    }
}