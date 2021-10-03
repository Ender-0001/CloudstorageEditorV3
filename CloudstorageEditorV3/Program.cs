using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudstorageEditorV3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CloudstorageEditor - Ender#0001";
            Console.WriteLine("CloudstorageEditor - Ender#0001");
            foreach (var arg in args)
            {
                if (arg.Contains("EpicPortal"))
                    Globals.Mode = Mode.AntiCheat;

                Globals.LaunchArgs += arg + " ";
            }

            if (Globals.Mode == Mode.None)
                Globals.Mode = Mode.CloudstorageEditor;

            Launcher.Launch();
        }
    }
}
