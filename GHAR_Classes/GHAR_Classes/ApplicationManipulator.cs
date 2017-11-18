using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHAR_Classes
{
    public class ApplicationManipulator
    {
        public void OpenNotepad()
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }
    }
}
