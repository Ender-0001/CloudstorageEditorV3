using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudstorageEditorV3
{
    class AntiCheat
    {
        bool Initalized = false;
        public void Start()
        {
            var Fortnite = new Process();
            Fortnite.StartInfo.FileName = "FortniteClient-Win64-Shipping.exe";
            Fortnite.StartInfo.Arguments = Globals.LaunchArgs;
            Fortnite.StartInfo.RedirectStandardOutput = true;
            Fortnite.StartInfo.UseShellExecute = false;
            Fortnite.Start();

            while (true)
            {
                var Output = Fortnite.StandardOutput.ReadLine();
                Console.WriteLine(Output);
                if (Output.Contains(Constants.InLobby) && !Initalized)
                {
                    Thread.Sleep(6000);
                    Injector.Inject(Fortnite.Id, "SSL.dll");
                    Injector.Inject(Fortnite.Id, "NoKick.dll");
                    Initalized = true;
                }

                if (Fortnite.HasExited)
                {
                    Process.Start("com.epicgames.launcher://apps/Fortnite?action=verify&silent=true");
                    Environment.Exit(0);
                }
            }
        }
    }
}
