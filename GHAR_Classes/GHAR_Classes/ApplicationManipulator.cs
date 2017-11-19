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


        public void OpenNotepad()
        {
            Process p = Process.GetProcessesByName("notepad").FirstOrDefault();
            p = Process.Start("Notepad.exe");
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                SendAndWait("This is a test");
                SendAndWait("^(p)", p, 300);
                SendAndWait(2, "{DOWN}");
                SendAndWait("{ENTER}", p);
                const string FilePath = @"I:\testFile1.pdf";
                SendAndWait($"{FilePath}");
                SendAndWait(4, "{TAB}");
                SendAndWait("{ENTER}", p);
                SendAndWait("{ENTER}", p);
                SendAndWait("%{F4}");
                SendAndWait("{RIGHT}");
                SendAndWait("{ENTER}");
            }

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