using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudstorageEditorV3
{
    class Launcher
    {
        public static void Launch()
        {
            if (Globals.Mode == Mode.CloudstorageEditor)
            {
                Globals.DiscordRPC = new DiscordRPC();
                Globals.DiscordRPC.Start();
                new CloudstorageEditor().Launch();
            }
            else if (Globals.Mode == Mode.AntiCheat)
            {
                Win32.ShowWindow(Win32.GetConsoleWindow(), 0);
                new AntiCheat().Start();
            }
        }
    }
}
